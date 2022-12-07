using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class Handler : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject how2PlayPanel;
    public Toggle Rotate_Toggle;


    public void OpenSettingsPanel(){
        if (settingsPanel != null) {  
            settingsPanel.SetActive(true);  

            int isRotation = PlayerPrefs.GetInt("Rotation");
            if(isRotation == 0){
                Rotate_Toggle.isOn = false;
            }
            else{
                Rotate_Toggle.isOn = true;
            }
        } 
    }

    public void OpenHow2PlayPanel(){
        if(how2PlayPanel != null){
            how2PlayPanel.SetActive(true);
        }
    }

    public void CloseSettingsPanel(){
        settingsPanel.SetActive(false);
    }

    public void CloseHow2PlayPanel(){
        how2PlayPanel.SetActive(false);
    }
}
