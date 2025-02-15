﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Game independent Power Up logic supporting 2D and 3D modes.
/// When collected, a Power Up has visuals switched off, but the Power Up gameobject exists until it is time for it to expire
/// Subclasses of this must:
/// 1. Implement PowerUpPayload()
/// 2. Optionally Implement PowerUpHasExpired() to remove what was given in the payload
/// 3. Call PowerUpHasExpired() when the power up has expired or tick ExpiresImmediately in inspector
/// </summary>
public class PowerUp : MonoBehaviour
{
    public string powerUpName;
    public string powerUpExplanation;
    public string powerUpQuote;
    [Tooltip("Tick true for power ups that are instant use, eg a health addition that has no delay before expiring")]
    public bool expiresImmediately;
    public GameObject specialEffect;
    public AudioClip soundEffect;

    public bool powerUpCollected;

    public float timer = 5f;

    protected AudioClip sound;

    /// <summary>
    /// It is handy to keep a reference to the player that collected us
    /// </summary>

    protected SpriteRenderer spriteRenderer;

    protected enum PowerUpState
    {
        InAttractMode,
        IsCollected,
        IsExpiring
    }

    protected PowerUpState powerUpState;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        powerUpState = PowerUpState.InAttractMode;
        powerUpCollected = false;
    }

    protected void Update()
    {
        if (powerUpCollected)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                PowerUpHasExpired();
                timer = 5f;
            }
        }

    }

    /// <summary>
    /// 2D support
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (powerUpState != PowerUpState.IsCollected && powerUpState != PowerUpState.IsExpiring)
            SingleAudioSource.PlayMusic(sound);
        PowerUpCollected(other.gameObject);
    }

    /// <summary>
    /// 3D support
    /// </summary>
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (powerUpState != PowerUpState.IsCollected && powerUpState != PowerUpState.IsExpiring)
            SingleAudioSource.PlayMusic(sound);
        PowerUpCollected(other.gameObject);
    }

    protected virtual void PowerUpCollected(GameObject gameObjectCollectingPowerUp)
    {
        // We only care if we've been collected by the player
        if (gameObjectCollectingPowerUp.tag != "Player")
        {
            return;
        }

        // We only care if we've not been collected before
        if (powerUpState == PowerUpState.IsCollected || powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsCollected;

        // We must have been collected by a player, store handle to player for later use      
        // playerBrain = gameObjectCollectingPowerUp.GetComponent<PlayerBrain> ();

        // We move the power up game object to be under the player that collect it, this isn't essential for functionality 
        // presented so far, but it is neater in the gameObject hierarchy
        // gameObject.transform.parent = playerBrain.gameObject.transform;
        // gameObject.transform.position = playerBrain.gameObject.transform.position;

        // Collection effects
        PowerUpEffects();

        // Payload      
        PowerUpPayload();

        // // Send message to any listeners
        foreach (GameObject go in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvents>(go, null, (x, y) => x.OnPowerUpCollected(this));
        }

        // Now the power up visuals can go away
        spriteRenderer.enabled = false;
    }

    protected virtual void PowerUpEffects()
    {
        if (specialEffect != null)
        {
            Instantiate(specialEffect, transform.position, transform.rotation, transform);
        }

        if (soundEffect != null)
        {
            // MainGameController.main.PlaySound (soundEffect);
        }
    }

    protected virtual void PowerUpPayload()
    {
        Debug.Log("Power Up collected, issuing payload for: " + gameObject.name);

        // If we're instant use we also expire self immediately
        if (expiresImmediately)
        {
            PowerUpHasExpired();
        }
        else
            powerUpCollected = true;
    }

    protected virtual void PowerUpHasExpired()
    {
        if (powerUpState == PowerUpState.IsExpiring)
        {
            return;
        }
        powerUpState = PowerUpState.IsExpiring;
        powerUpCollected = true;

        // Send message to any listeners
        foreach (GameObject go in EventSystemListeners.main.listeners)
        {
            ExecuteEvents.Execute<IPowerUpEvents>(go, null, (x, y) => x.OnPowerUpExpired(this));
        }
        Debug.Log("Power Up has expired, removing after a delay for: " + gameObject.name);
        DestroySelfAfterDelay();
    }

    protected virtual void DestroySelfAfterDelay()
    {
        // Arbitrary delay of some seconds to allow particle, audio is all done
        // TODO could tighten this and inspect the sfx? Hard to know how many, as subclasses could have spawned their own
        Destroy(gameObject, 0);
    }
}

