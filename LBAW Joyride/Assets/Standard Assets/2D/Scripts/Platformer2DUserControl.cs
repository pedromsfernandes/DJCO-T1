using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;

        public float stunCooldown = 1f;
        float stunCooldownCounter = 0f;
        bool moving = true;
        bool pause = false;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (moving && !m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            if(pause)
                return;

            if (moving)
            {
                // Read the inputs.
                bool crouch = Input.GetKey(KeyCode.LeftControl);
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
            else
            {
                stunCooldownCounter += Time.deltaTime;
                if (stunCooldownCounter >= stunCooldown)
                {
                    moving = true;
                    this.gameObject.transform.Find("Stun").gameObject.SetActive(false);
                    stunCooldownCounter = 0;
                }
            }
        }

        public void Stun()
        {
            moving = false;
            this.gameObject.transform.Find("Stun").gameObject.SetActive(true);
        }

        public void Start()
        {
            pause = false;
        }

        public void Stop()
        {
            pause = true;
        }
    }
}
