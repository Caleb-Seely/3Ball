using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotator : MonoBehaviour
{
    [SerializeField] private  Vector3 _rotation;
    private float timePassed;
    public Events _events;
    float smooth = 1.0f;
    public Quaternion target;
    public bool spin = true;

    void Update()
    {   
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int rotateFloorPref = PlayerPrefs.GetInt("Rotation");
        if( (sceneIndex == 0 && rotateFloorPref == 1)|| (rotateFloorPref == 1 && !_events.NewGame )){
            rotateFloor();
        }
        else{
            //Rotate to specified target
            timePassed = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        }
    }

    public void toggleRotation(){
        Debug.Log("Toggle rotation");
        if(spin == true){
            spin = false;
            PlayerPrefs.SetInt("Rotation", 0);
        }else{
            PlayerPrefs.SetInt("Rotation", 1);
            spin = true;
        }
    }

    private void rotateFloor(){
        timePassed += Time.deltaTime;
        _rotation.y = timePassed+10;
        transform.Rotate(_rotation * Time.deltaTime);   
    }
}
