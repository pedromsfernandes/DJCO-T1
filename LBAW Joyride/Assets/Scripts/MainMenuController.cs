using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    static bool first = true;

    public GameObject canvas;
    public Image logo;
    public GameObject[] btns;

    Vector2 screenSize;

    void Start()
    {
        //Generate world space point information for position and scale calculations
        screenSize.x = canvas.GetComponent<RectTransform>().sizeDelta.x;
        screenSize.y = canvas.GetComponent<RectTransform>().sizeDelta.y;

        logo.GetComponent<RectTransform>().sizeDelta = new Vector2(10000, screenSize.y / 3f);
        logo.GetComponent<RectTransform>().localPosition = new Vector3(0, screenSize.y / 3f, 0);

        if(first)
            StartCoroutine("FirstMenuAnim");
    }

    IEnumerator FirstMenuAnim()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < 20; i++)
        {
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a + 0.05f);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
