using UnityEngine;
using System.Collections;

public class Singleton : MonoBehaviour {
	public bool initialRoom = true;
    public bool redRoom = false;
	public bool afterRedRoom = false;
	public bool greenRoom = false;
	public bool afterGreenRoom = false;
	public bool blueRoom = false;
	public bool afterBlueRoom = false;

    public static int MAX_LIVES = 5;
    public int playerLives = MAX_LIVES;
    public int numEnemiesKilled = 0;

    private static Singleton instance;

	//gets or creates the singleton instance
	public static Singleton Instance
    {
        get { return instance ?? (instance = new GameObject("Singleton").AddComponent<Singleton>()); }
    }
}