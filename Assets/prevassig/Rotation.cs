using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Rotation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Outline outline;
    void Start()
    {
        //changeColor=false;
        outline = GetComponent<Outline>();
        outline.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
       if((outline.enabled)&&Input.GetButton("js7")){
           transform.Rotate(0f,100*Time.deltaTime,0f,Space.Self);
        }
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
