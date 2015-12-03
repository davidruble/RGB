using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneManagerScript : MonoBehaviour {
    public AudioSource initialRoomMusic;
    public AudioSource redRoomMusic;
    public AudioSource afterRedRoomMusic;
    public AudioSource greenRoomMusic;
    public AudioSource afterGreenRoomMusic;
    public AudioSource blueRoomMusic;
    public AudioSource afterBlueRoomMusic;

    public int maxEnemies = 15;
    public float textDisplayTimer = 5.0f;

    public Text canvasText;
    public Text healthText;
    public Text enemiesText;

    private int initialRoomIndex = 0;
    private int redRoomIndex = 1;
    private int afterRedRoomIndex = 2;
    private int greenRoomIndex = 3;
    private int afterGreenRoomIndex = 4;
    private int blueRoomIndex = 5;
    private int afterBlueRoomIndex = 6;

    private GameObject player;
    private Terrain terrain;
    
    private bool textDisplay = false;
    private float textDisplayStep = 0.0f;

    private const string INTERACT_STR = "Press 'E' to interact\nLeft mouse to attack with weapon";
    private const string HEALTH_STR = "Health: ";
    private const string ENEMIES_KO_STR = "Enemies killed: ";
    private const string EMPTY_STR = "";

    void Awake()
    {
        terrain = Terrain.activeTerrain;
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        if (Singleton.Instance.initialRoom == true)
            textDisplay = true;

        textDisplayStep = 0.0f;
        canvasText.text = EMPTY_STR;
    }

    void Update()
    {
        healthText.text = HEALTH_STR + Singleton.Instance.playerLives;
        enemiesText.text = ENEMIES_KO_STR + Singleton.Instance.numEnemiesKilled;

        //display the gui text for a designated period of time at the start of a level
        if (textDisplay)
        {
            canvasText.text = INTERACT_STR;
            textDisplayStep += Time.deltaTime;
        }
        if (textDisplayStep > textDisplayTimer)
        {
            textDisplay = false;
            canvasText.text = EMPTY_STR;
            textDisplayStep = 0.0f;
        }

        //check if the player went through the bottom of the map
        if (player.transform.position.y < -10)
        {
            player.transform.position = new Vector3(transform.position.x, 20.0f, transform.position.z);
        }

        //handles initial room stuff
        if (Singleton.Instance.initialRoom == true)
        {
            if (terrain != null)
                terrain.drawTreesAndFoliage = false;

            if (!initialRoomMusic.isPlaying)
            {
                initialRoomMusic.Play();
            }
        }

        //handles red room stuff
        else if (Singleton.Instance.redRoom == true)
        {
            if (!redRoomMusic.isPlaying)
            {
                redRoomMusic.Play();
            }

            //restart the level
            if (Singleton.Instance.playerLives <= 0)
            {
                Singleton.Instance.playerLives = Singleton.MAX_LIVES;
                Application.LoadLevel(redRoomIndex);
            }

            if (Singleton.Instance.numEnemiesKilled >= 10)
            {
                //Debug.Log("Run! The door is unlocked!");
            }
        }

        //handles after red room stuff
        else if (Singleton.Instance.afterRedRoom == true)
        {
            if (terrain != null)
                terrain.drawTreesAndFoliage = false;

            if (!afterRedRoomMusic.isPlaying)
            {
                afterRedRoomMusic.Play();
            }
            //print ("completed red room");
        }

        //handles green room stuff
        else if (Singleton.Instance.greenRoom == true)
        {
            if (!greenRoomMusic.isPlaying)
            {
                greenRoomMusic.Play();
            }
        }

        //handles after green room stuff
        else if (Singleton.Instance.afterGreenRoom == true)
        {
            if (terrain != null)
                terrain.drawTreesAndFoliage = true;

            if (!afterGreenRoomMusic.isPlaying)
            {
                afterGreenRoomMusic.Play();
            }
        }

        //handles blue room stuff
        else if (Singleton.Instance.blueRoom == true)
        {
            if (!blueRoomMusic.isPlaying)
            {
                blueRoomMusic.Play();
            }
        }

        //handles after blue room stuff
        else if (Singleton.Instance.afterBlueRoom == true)
        {
            if (terrain != null)
                terrain.drawTreesAndFoliage = true;

            if (!afterBlueRoomMusic.isPlaying)
            {
                afterBlueRoomMusic.Play();
            }
        }
    }

    //sets conditions for the next level as the level is being loaded
    public void LoadLevel()
    {
        if (Singleton.Instance.initialRoom == true)
        {
            Singleton.Instance.redRoom = true;
            Singleton.Instance.initialRoom = false;
            Application.LoadLevel(redRoomIndex);        //load the next level
        }
        else if (Singleton.Instance.redRoom == true)
        {
            Singleton.Instance.afterRedRoom = true;
            Singleton.Instance.redRoom = false;
            Application.LoadLevel(afterRedRoomIndex);       //load the next level
        }
        else if (Singleton.Instance.afterRedRoom == true)
        {
            Singleton.Instance.greenRoom = true;
            Singleton.Instance.afterRedRoom = false;
            Application.LoadLevel(greenRoomIndex);
        }
        else if (Singleton.Instance.greenRoom == true)
        {
            Singleton.Instance.afterGreenRoom = true;
            Singleton.Instance.greenRoom = false;
            Application.LoadLevel(afterGreenRoomIndex);
        }
        else if (Singleton.Instance.afterGreenRoom == true)
        {
            Singleton.Instance.blueRoom = true;
            Singleton.Instance.afterGreenRoom = false;
            Application.LoadLevel(blueRoomIndex);
        }
        else if (Singleton.Instance.blueRoom == true)
        {
            Singleton.Instance.afterBlueRoom = true;
            Singleton.Instance.blueRoom = false;
            Application.LoadLevel(afterBlueRoomIndex);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == redRoomIndex)
        {
            //print("red room loaded");

            Singleton.Instance.redRoom = true;
            Singleton.Instance.initialRoom = false;
        }
        if (level == afterRedRoomIndex)
        {
            //print("after red room loaded");

            Singleton.Instance.afterRedRoom = true;
            Singleton.Instance.redRoom = false;
        }
        if (level == greenRoomIndex)
        {
            //print("green room loaded");

            Singleton.Instance.greenRoom = true;
            Singleton.Instance.afterRedRoom = false;
        }
        if (level == afterGreenRoomIndex)
        {
            //print("after green room loaded");

            Singleton.Instance.afterGreenRoom = true;
            Singleton.Instance.greenRoom = false;
        }
        if (level == blueRoomIndex)
        {
            //print("blue room loaded");

            Singleton.Instance.blueRoom = true;
            Singleton.Instance.afterGreenRoom = false;
        }
        if (level == afterBlueRoomIndex)
        {
            //print("after blue room loaded");

            Singleton.Instance.afterBlueRoom = true;
            Singleton.Instance.blueRoom = false;
        }
    }
}

