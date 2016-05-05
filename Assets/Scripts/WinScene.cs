using UnityEngine;
using System.Collections;
 
public class WinScene : MonoBehaviour 
{
    private int destroyedWebs = 0;
 
    // Use this for initialization
    private void Start () 
    {
        destroyedWebs = PlayerPrefs.GetInt("DestroyedWebs");
    }
 
    private void OnGUI()
    {
        Rect drawRect = new Rect(Screen.width * .05f, (Screen.height * .1f) -20.0f, Screen.width * .5f, Screen.height * .5f);
        GUI.Box(drawRect, "");
 
        drawRect.Set(drawRect.xMin + 50.0f, drawRect.yMin + 20.0f, drawRect.width, drawRect.height);
        GUI.Label(drawRect, "You WIN!");
 
        drawRect.Set(drawRect.xMin, drawRect.yMin + 50.0f, drawRect.width, drawRect.height);
        GUI.Label(drawRect, destroyedWebs + " webs destroyed!");
 
        drawRect.Set(drawRect.xMin, drawRect.yMin + 50.0f, Screen.width * .25f, Screen.height * .25f);
        if (GUI.Button(drawRect, "Play Again?"))
        {
            Application.LoadLevel("Scene1");
        }
    }
}