using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRotate : MonoBehaviour
{
    public float speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //First ring of animated powerup indicator
        transform.Rotate(Vector3.down, speed * Time.deltaTime);
    }
}
