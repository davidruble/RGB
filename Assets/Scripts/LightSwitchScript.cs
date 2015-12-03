using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour {
	public GameObject chooser;
	public int blueRoom = 7;

	private bool lightOn = false;
	private Light roomLight;

	// Use this for initialization
	void Start ()
	{
        roomLight = gameObject.GetComponentInChildren<Light> ();
		roomLight.enabled = false;
	}

	void OnTriggerEnter(Collider collider)
	{
        //don't turn on the lights for the sword, only the character
        if (!(collider.gameObject.tag == "Equipped Sword"))
        {
            if (!chooser.GetComponent<Chooser>().failed)
            {
                if (!lightOn)
                {
                    roomLight.enabled = true;
                }
            }
            else
            {
                Debug.Log("Failed");
                Application.LoadLevel(blueRoom);
            }
        }
	}
}
