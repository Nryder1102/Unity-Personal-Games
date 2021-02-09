using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActions : MonoBehaviour
{
    public Transform player;
    public int speed = 1;
    public int charge = 0;
    public int chargeMax = 3;
    public int cooldown = 0;
    public int cooldownTime = 2;
    public bool isCharging;
    public bool onCooldown;
    public GameObject projectileOne;
    public int flag = 0;
    public int flag2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(player != null && isCharging == false && onCooldown == false)

        {
        //Rotates cannon object on a singular axis
        transform.LookAt(player);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(0, eulerAngles.y, 0);
        transform.rotation = Quaternion.Euler(eulerAngles); 
        
        //My attempt at doing the same as above but slower, but I'm adding in a stop and charge up so it won't matter
        /*
        transform.LookAt(player);
        Vector3 playerPos = player.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(transform.forward, playerPos);
        player.rotation = Quaternion.RotateTowards(player.rotation,lookRot,Time.deltaTime);
        */

        
        }

        if(isCharging == false && onCooldown == false && flag == 0)
        {
            StartCoroutine(InBetweenShots());
            flag = 1;
        }

        if(charge == chargeMax)
        {
            //Shoot projectile and reset charge, activate cooldown
            isCharging = false;
            onCooldown = true;
            
            //Debug.Log(isCharging);
        }

        if(charge != chargeMax && isCharging == true && flag2 == 0) 
        {
            StartCoroutine(ChargeLoop());
            flag2 = 1;
        }

        if(onCooldown == true)
        {
            //Loop/Wait Coroutine again, find out how to not have cannon snap back to player
            
            StartCoroutine(Cooldown());
        }

        if(cooldown == cooldownTime)
        {
            onCooldown = false;
            Debug.Log(cooldown);
            cooldown = 0;
            StartCoroutine(InBetweenShots());
        }


    }

    //Coroutine to loop and wait 1 second and add one until charge = chargeMax
    IEnumerator ChargeLoop()
    {
        yield return new WaitForSeconds(1);
        charge++;
        flag2 = 0;
        //Debug.Log("ChargeLoop");
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        cooldown++;
        //Debug.Log(cooldown);
    }

    IEnumerator InBetweenShots()
    {
        yield return new WaitForSeconds(3);
        isCharging = true;
        //Debug.Log("Between");
    }



}