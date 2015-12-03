using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class MusicPlayerScript : MonoBehaviour {
    public GameObject musicPlayer;

    public AudioSource initialRoomMusic;
    public AudioSource redRoomMusic;
    public AudioSource afterRedRoomMusic;
    public AudioSource greenRoomMusic;
    public AudioSource afterGreenRoomMusic;
    public AudioSource blueRoomMusic;
    public AudioSource afterBlueRoomMusic;

	void Awake ()
    {
        musicPlayer = GameObject.Find("MUSIC");
        
        //no music playing in scene yet
        if (musicPlayer == null)
        {
            //set the music player and make sure it doesn't die between scenes
            musicPlayer = this.gameObject;
            musicPlayer.name = "MUSIC";
            DontDestroyOnLoad(musicPlayer);
        }
        else
        {
            //coming back to a scene with music already playing
            if (this.gameObject.name != "MUSIC")
            {
                //destroy the extra music player
                Destroy(this.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //handles initial room stuff
        if (Singleton.Instance.initialRoom == true)
        {
            if (!initialRoomMusic.isPlaying)
            {
                initialRoomMusic.Play();
            }
        }

        //handles red room stuff
        else if (Singleton.Instance.redRoom == true)
        {
            //stop the initial room music
            if (initialRoomMusic.isPlaying)
            {
                initialRoomMusic.Stop();
            }

            if (!redRoomMusic.isPlaying)
            {
                redRoomMusic.Play();
            }
        }

        //handles after red room stuff
        else if (Singleton.Instance.afterRedRoom == true)
        {
            //stop the red room music
            if (redRoomMusic.isPlaying)
            {
                redRoomMusic.Stop();
            }

            if (!afterRedRoomMusic.isPlaying)
            {
                afterRedRoomMusic.Play();
            }
            //print ("completed red room");
        }

        //handles green room stuff
        else if (Singleton.Instance.greenRoom == true)
        {
            //stop the afterRedRoom music
            if (afterRedRoomMusic.isPlaying)
            {
                afterRedRoomMusic.Stop();
            }

            if (!greenRoomMusic.isPlaying)
            {
                greenRoomMusic.Play();
            }
        }

        //handles after green room stuff
        else if (Singleton.Instance.afterGreenRoom == true)
        {
            //stop the green room music
            if (greenRoomMusic.isPlaying)
            {
                greenRoomMusic.Stop();
            }

            if (!afterGreenRoomMusic.isPlaying)
            {
                afterGreenRoomMusic.Play();
            }
        }

        //handles blue room stuff
        else if (Singleton.Instance.blueRoom == true)
        {
            //stops the afterGreenRoom music
            if (afterGreenRoomMusic.isPlaying)
            {
                afterGreenRoomMusic.Stop();
            }

            if (!blueRoomMusic.isPlaying)
            {
                blueRoomMusic.Play();
            }
        }

        //handles after blue room stuff
        else if (Singleton.Instance.afterBlueRoom == true)
        {
            //stop the blue room music
            if (blueRoomMusic.isPlaying)
            {
                blueRoomMusic.Stop();
            }

            if (!afterBlueRoomMusic.isPlaying)
            {
                afterBlueRoomMusic.Play();
            }
        }
	}
}
