using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
 
    [RequireComponent(typeof(TMP_Text))]
    public class OpenHyperlink : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text m_textMeshPro;
        void Start()
        {
            m_textMeshPro = GetComponent<TMP_Text>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            
            string webLink = "https://calebseely.com/";

            if(Random.Range(0,100) % 2 == 0){
                webLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
            }

            Application.OpenURL(webLink);
            
            // int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_textMeshPro, new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), 0), null);
            // if (linkIndex != -1)
            // {
            //     TMP_LinkInfo linkInfo = m_textMeshPro.textInfo.linkInfo[linkIndex];
            //     Application.OpenURL("https://calebseely.com/");
            // }
        }
    }