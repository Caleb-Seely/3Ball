using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAccelerometer : MonoBehaviour
{
    private Rigidbody rb;
    public float dirX;
    public float moveSpeed = 20f;

    void Start(){
        if(SystemInfo.supportsGyroscope){
            Input.gyro.enabled = true;
            Debug.Log("Gyroscope enabled");
        }else{
            Debug.Log("No Gyroscope found");
        }
    }
    void Update()
    {
        if(SystemInfo.supportsGyroscope){
            //transform.Rotate(-Input.gyro.rotation.x*3,-Input.gyro.rotation.y*3,0);
            transform.rotation = GyroToUnity(Input.gyro.attitude);
            // transform.Rotate (-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, 0);
        }
    }

    private Quaternion GyroToUnity(Quaternion q){
        Debug.Log(q);
        return new Quaternion(q.x, 90*q.y, q.z,-q.w);
    }
}
