using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player; 
    private GameObject enemy;
    public float speed;
    public bool isGameActive = true;
    public int playernum;
    private int randtarget = 0;

    

    
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
       
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the game to over if the number of player present is 0
        playernum = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playernum == 0 && isGameActive)
        {
            isGameActive = false;
        }
        //Player becomes the target if player is present
        if(isGameActive)
        {
            Vector3 target = player.transform.position;
            

            
           
        //Doesn't work, tried to get some enemies to occasionally target enemies, but I can't get it random per spawn, it's all or nothing
        if (randtarget == 0)
        {
            target = player.transform.position;
        }else if (randtarget == 1)
        {
            target = enemy.transform.position;
        }
        //Makes enemy chase after player (and hopefully other enemies in the future)
        Vector3 lookDirection = (target - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
       }
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
