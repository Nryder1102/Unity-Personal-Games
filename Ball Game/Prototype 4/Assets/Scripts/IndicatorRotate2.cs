using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRotate2 : MonoBehaviour
{
    public float speed = 125.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Second ring of animated powerup indicator
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
