using UnityEngine;
using System.Collections;

public class MeleeScript : MonoBehaviour {
    public float attackTimer = 0.458f;		//length of attack animation
    public float attackTimerStep = 0.0f;	//timer up to length of attack animation

    public AudioSource zombieDeathSound;	//sound effect for zombie death
    public AudioSource hitSound;			//sound when you hit azombie
    public Transform blood;					//blood effect
    public float thrust = 2.0f;            //for collision with interactable objects

    private bool attacking = false;		    //are you currently swinging the sword?
    private int leftMouse = 0;				//variable for attack controls -- left mouse
    private GameObject equippedSword;

    void Start()
    {
        equippedSword = GameObject.FindWithTag("Equipped Sword");
    }

    void Update()
    {
        //Debug.Log(attackTimerStep);
        if (Input.GetMouseButton(leftMouse) && equippedSword != null)
        {
            GetComponent<Rigidbody>().detectCollisions = true; 

            //play the attack animation and sound effect
            equippedSword.GetComponent<Animation>().Play("Take 001");
            GetComponent<AudioSource>().Play();
            attacking = true;
        }

        //timer for attack duration -- prevents from killing zobies when not attacking
        if (attacking == true)
        {
            attackTimerStep += Time.deltaTime;
        }

        if (attackTimerStep >= attackTimer)
        {
            ResetTimer();
        }
    }


    //collision with an enemy
    void OnCollisionEnter(Collision collision)
    {
        if (attacking == true)
        {
            //hit an enemy
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Singleton.Instance.numEnemiesKilled++;

                //play blood sound and instantiate one and destroy enemy
                hitSound.Play();
                zombieDeathSound.Play();
                Transform bloodSplatter = (Transform) Instantiate(blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                Destroy(collision.gameObject);
                Destroy(bloodSplatter.gameObject, 0.5f);
            }
            //hit any other interactable object
            else
            {
                // Debug.Log("Hit something else");
                collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
                attacking = false;
            }
        }
        //make sure nothing is interacted with unless explicitly attacking
        else
        {
            GetComponent<Rigidbody>().detectCollisions = false; 
        }
    }

    //reverts variables -- stops sword from doing damage until next attack
    private void ResetTimer()
    {
        attacking = false;
        attackTimerStep = 0.0f;
    }
}
