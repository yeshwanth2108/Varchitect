using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Highlight : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Outline outline;    
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Code to execute when the pointer enters the UI element
        outline.enabled=true;
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Code to execute when the pointer exits the UI element
        outline.enabled = false;
    }
}
