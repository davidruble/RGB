using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour {
	public GameObject chooser;

	private bool lightOn = false;
	private Light roomLight;

	public int blueRoom = 5;

	// Use this for initialization
	void Start ()
	{
		roomLight = gameObject.GetComponentInChildren<Light> ();
		roomLight.enabled = false;
	}

	void OnTriggerEnter(Collider collider)
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
