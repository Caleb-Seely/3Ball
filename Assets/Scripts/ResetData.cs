using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetData : MonoBehaviour
{
    public Button resetBtn;

    public void ResetDataOnClick()
    {
        resetBtn.interactable = false;
        Debug.Log("Reset");
        PlayerPrefs.DeleteKey("GameIsSet");
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetFloat("Money", 0);
        PlayerPrefs.SetFloat("BallPrice",2);
        PlayerPrefs.SetFloat("BallPricePrev", 1);
        PlayerPrefs.SetFloat("BallScalePrice",2);
        PlayerPrefs.SetFloat("BallScale", 1);
        PlayerPrefs.SetInt("StartBallCount", 5);
    }

}
