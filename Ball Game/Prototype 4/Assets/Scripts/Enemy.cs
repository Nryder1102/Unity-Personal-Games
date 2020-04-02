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
        playernum = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playernum == 0 && isGameActive)
        {
            isGameActive = false;
        }
        if(isGameActive)
        {
            Vector3 target = player.transform.position;
            

            
           
        
        if (randtarget == 0)
        {
            target = player.transform.position;
        }else if (randtarget == 1)
        {
            target = enemy.transform.position;
        }

        Vector3 lookDirection = (target - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);
       }
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
