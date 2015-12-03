using UnityEngine;
using System.Collections;

public class FullScreenScript : MonoBehaviour {

    void Update()
    {
        //turn on lock screen when F is pushed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!Screen.fullScreen)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
                Screen.fullScreen = true;

                Debug.Log("Going fullscreen");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           if (Screen.fullScreen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Screen.fullScreen = false;

                Debug.Log("Exiting fullscreen");
            }
        }
    }

}
