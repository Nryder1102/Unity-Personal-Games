using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //The most annoying enemy deserves it's own script
    private void OnCollisionEnter(Collision collision)
    {
        //Bounces player back, unaffected by powerup mostly
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayFromPlayer * 2, ForceMode.Impulse);
        
        }
        //Just because they're terrifying, I made it so they can accidentally (hopefully on purpose in the future) attack other enemies, except better than they do the player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayFromPlayer * 5, ForceMode.Impulse);
        
        }
    }
}
