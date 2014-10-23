using UnityEngine;
using System.Collections;

public class Chooser : MonoBehaviour {
	private Ray ray;
	private RaycastHit hit;

	public bool failed = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E))
		{
			ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f));
			
			if (Physics.Raycast(ray, out hit, 1f) && hit.collider.tag == "Choice")
			{
				RiddleChoice choice = hit.collider.gameObject.GetComponent<RiddleChoice>();

				if (choice.correctChoice)
				{
					Debug.Log ("Correct");
				}
				else
				{
					Debug.Log ("Incorrect");
					failed = true;
				}

				choice.door.SetActive(false);
			}
		}
	}
}
