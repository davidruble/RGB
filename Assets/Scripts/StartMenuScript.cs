using UnityEngine;
using System.Collections;

public class StartMenuScript : MonoBehaviour {
    private int startMenuIndex = 0;
    private int howToMenu = 1;
    private int gameStartIndex = 2;

    public void reloadStartMenu()
    {
        Application.LoadLevel(startMenuIndex);
    }

    public void loadGame()
    {
        Application.LoadLevel(gameStartIndex);
    }

    public void loadHowTo()
    {
        Application.LoadLevel(howToMenu);
    }
}
