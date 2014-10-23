using UnityEngine;
using System.Collections;

public class LightSwitchScript : MonoBehaviour {
	public GameObject chooser;

	private bool lightOn = false;
	private Light light;

	public int blueRoom = 3;

	// Use this for initialization
	void Start ()
	{
		light = gameObject.GetComponentInChildren<Light> ();
		light.enabled = false;
	}

	void OnTriggerEnter(Collider collider)
	{
		if (!chooser.GetComponent<Chooser>().failed)
		{
			if (!lightOn)
			{
				light.enabled = true;
			}
		} 
		else 
		{
			Application.LoadLevel(blueRoom);
		}
	}
}
