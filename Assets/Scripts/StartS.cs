using UnityEngine;
using System.Collections;
 
public class StartScene : MonoBehaviour 
{
    private void OnGUI()
    {
        Rect drawRect = new Rect(Screen.width * .05f, (Screen.height * .1f) -20.0f, Screen.width * .5f, Screen.height * .5f);
        GUI.Box(drawRect, "");
 
        drawRect.Set(drawRect.xMin + 50.0f, drawRect.yMin + 20.0f, drawRect.width, drawRect.height);
        GUI.Label(drawRect, "Welcome to Breaking Rat Dude!");
 
        drawRect.Set(drawRect.xMin, drawRect.yMin + 50.0f, drawRect.width, drawRect.height);
        GUI.Label(drawRect, "Use the arrow keys to move. Avoid spiders, smash their webs.");
 
        drawRect.Set(drawRect.xMin, drawRect.yMin + 50.0f, Screen.width * .25f, Screen.height * .25f);
        if (GUI.Button(drawRect, "Play"))
        {
            Application.LoadLevel("Scene1");
        }
    }
}