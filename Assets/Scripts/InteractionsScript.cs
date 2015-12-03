using UnityEngine;
using System.Collections;

public class InteractionsScript : MonoBehaviour {
    public float reachDistance = 2.5f;	//how far the raycast goes -- for opening doors
    public float doorTimer = 0.5f;		//how long is allowed for the door sound effect to play
    public float doorTimerCount = 0.0f;	//counter up to doorTimer
    public bool doorHit = false;		//has the player interacted with the door?

    private GameObject sceneManager;
    private GameObject heldSword;
    //private CharacterController charCtrl;

    //private bool ceilingCol = false;   //true when there hasbeen a ceiling collision

    void Start()
    {
        heldSword = GameObject.FindWithTag("Equipped Sword");
        sceneManager = GameObject.FindWithTag("Scene Manager");
        //charCtrl = this.GetComponent<CharacterController>();
        if (Singleton.Instance.beforeRedRoom || Singleton.Instance.redRoom)
            heldSword.SetActive(false);
    }

    void Update()
    {
        //Vector3 p1 = transform.position + charCtrl.center;

        //timer for when player interacts with door -- alotted time for sound to play
        if (doorHit)
        {
            doorTimerCount += Time.deltaTime;
            //Debug.Log(doorTimerCount);
        }

        //once the timer has run out
        if (doorTimerCount >= doorTimer)
        {
            GetComponent<AudioSource>().Stop();

            sceneManager.GetComponent<SceneManagerScript>().LoadLevel();
        }

        //interaction
        if (Input.GetKeyDown(KeyCode.E))
        {
            //if something has been hit within the raycast's limits
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4.0f);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                //if this something was the door
                if (hitColliders[i].GetComponent<Collider>().tag == "Door")
                {
                    //Debug.Log("Door is hit.");
                    GetComponent<AudioSource>().Play();
                    doorHit = true;
                }
                //if this was the sword
                else if (hitColliders[i].GetComponent<Collider>().tag == "Sword")
                {
                    //"pick up" the sword and destroy the one on the ground
                    //Debug.Log("Picked up sword");
                    heldSword.SetActive(true);
                    Destroy(hitColliders[i].GetComponent<Collider>().gameObject);
                }
                else if (hitColliders[i].GetComponent<Collider>().tag == "Puzzle")
                {
                    //Debug.Log("Picked up a puzzle piece");
                    hitColliders[i].transform.parent = Camera.main.transform;
                    hitColliders[i].transform.position = transform.position + new Vector3(0.0f, 0.0f, -3.0f);
                }
            }
        }

        //prevent character from getting stuck when jumping
        /*	if ((charCtrl.collisionFlags & CollisionFlags.Above) != 0)
            {
                Vector3 hitVelocity = charCtrl.velocity;
                ceilingCol = true;
                HandleCeilingCollisions(hitVelocity);
                //Debug.Log("Hit ceiling");
            }*/
    }
    /*void HandleCeilingCollisions(hitVelocity : Vector3)
    {
        if (!charCtrl.isGrounded)
            charCtrl.Move(Vector3.Reflect(hitVelocity, Vector3.up) * Time.deltaTime);
        else
            ceilingCol = false;
    }*/
}
