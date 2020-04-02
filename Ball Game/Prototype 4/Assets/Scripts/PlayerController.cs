﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    private float powerUpStrength = 6.7f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forward = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forward);
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
        if (transform.position.y < -10)
       {
            Destroy(gameObject);

       } 
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        
        }
    }

     IEnumerator PowerupCountdownRoutine() 
     {
         yield return new WaitForSeconds(8);
         hasPowerup = false;
         powerupIndicator.gameObject.SetActive(false);
     }
}