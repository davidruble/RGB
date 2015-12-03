using UnityEngine;
using System.Collections;

public class FinalWall : MonoBehaviour {
    public GameObject chooser;
    public GameObject sceneManager;
    public int blueRoomIndex = 5;

	// Use this for initialization
	void Start () {
	}
    
    void OnTriggerEnter(Collider collider)
    {
        if (!(collider.gameObject.tag == "Equipped Sword"))
        {
            if (!chooser.GetComponent<Chooser>().failed)
                sceneManager.GetComponent<SceneManagerScript>().LoadLevel();
            else
                Application.LoadLevel(blueRoomIndex);
        }
    }	
}
