using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollTextHandler : MonoBehaviour
{
public TextMeshProUGUI text;
    public float scrollSpeed = 10.0f;
    public float cycleLength = 0;       //Spacing between the text repeating

    private TextMeshProUGUI cloneText;
    private RectTransform textRectTransform;
    private string sourceText;
    private string tempText;

 // Use this for initialization
    void Awake () {

        float textBoxWidth = -1080-text.preferredWidth-cycleLength;
        textRectTransform = text.GetComponent<RectTransform>();
                
        cloneText = Instantiate(text) as TextMeshProUGUI;
        RectTransform cloneRectTransform = cloneText.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(textRectTransform);
        cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
        // cloneRectTransform.localPosition = new Vector3(-1080, 0, cloneRectTransform.position.z);
        cloneRectTransform.localPosition = new Vector3(textBoxWidth, 0, cloneRectTransform.position.z);
        cloneRectTransform.localScale = new Vector3(1, 1, 1);
        cloneText.text = text.text;  

    }

    private IEnumerator Start()
    {
    
        float width = 1080 + text.preferredWidth + cycleLength;      
        Vector3 startPosition = textRectTransform.localPosition;

        float scrollPosition = -1080 - text.preferredWidth;

        while (true)
        {
            textRectTransform.localPosition = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);
            scrollPosition += scrollSpeed * 20 * Time.deltaTime;         
            yield return null;
        }      
    }

}
