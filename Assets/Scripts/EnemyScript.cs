using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public int movementSpeed = 2;					//speed of zombie movement
    public bool playerHit = false;					//has the zombie hit the player?
    public float playerHitForceDuration = 0.2f;		//duration for being flown backwards
    public float forceAmount = 12.0f;				//amount of force to be applied

    private NavMeshAgent agent;                     //agent for AI 
    private GameObject player;					    //the player object being manipulated

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       //accessing the AI nav mesh
        player = GameObject.FindWithTag("Player");  //accessing the player
    }

    void Update()
    {
        agent.destination = player.transform.position;  //AI moving towards player
        agent.speed = movementSpeed;                    //how fast the AI moves

        if (playerHit)
        {
            //force the player back relative to himself when hit
            player.transform.Translate(Vector3.back * Time.deltaTime * forceAmount, Space.Self);
        }
        if (!playerHit)
        {
            //after certain time, stop flying backwards
            player.transform.Translate(Vector3.zero, Space.Self);
        }
    }

    //player "death" -- restarting the scene
    void OnCollisionEnter(Collision collision)
    {
        //colliding with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            //subtract lives from the player
            Debug.Log("Hit the player");
            Singleton.Instance.playerLives--;

            playerHit = true;
        }
    }

    //after collision with player has stopped
    void OnCollisionExit(Collision collision)
    {
        StartCoroutine(HandleCollisionExit(collision));
    }

    private IEnumerator HandleCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //this allows force to be applied for certain amount of time
            yield return new WaitForSeconds(playerHitForceDuration);

            playerHit = false;
        }
    }
}
