using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer color;

    public bool isFlat = true;
    public int x, y = 0;
    public int z = 90;
    public int speed = 100;
    public float sphereRadius = 0f;
   
    private void Start() {
        rb = GetComponent<Rigidbody>();
        color = GetComponent<Renderer>();

        // Create a new RGBA color 
        Color customColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 0f,1f);
        // Call SetColor using the shader property name "_Color" and setting the color to red
        color.material.SetColor("_Color", customColor);
        //Set the mass of each sphere
        rb.mass = Random.Range(1,20);
        //Debug.Log(rb.mass);
    }
    
    private void Update() {
        // Never add vertical force, b/c we assume the screen is flat, the z val is height
        Vector3 force = new Vector3 (Input.acceleration.x*speed, Input.acceleration.y*speed, 0f);
        Vector3 tilt = force;

        //Only add force when the ball is on the ground
        float yVal = rb.position.y;
        if(isFlat){
            tilt = Quaternion.Euler(z,x,y) * tilt;
        }
        if(yVal < 2){
            rb.AddForce(tilt); 
        }
        else{
        }
        
        
        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.red);

        //See DeathScript for Destroy()
    }

}
