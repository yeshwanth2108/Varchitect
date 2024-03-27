using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Translation : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Outline outline;
    private bool moving;
    void Start()
    {
        //changeColor=false;
        outline = GetComponent<Outline>();
        outline.enabled=false;
        moving=true;
    }

    // Update is called once per frame
    void Update()
    {
       if((outline.enabled)&&Input.GetButton("js7")&&moving){
           transform.Translate(0f,0f,-10*Time.deltaTime,0f);
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
    private void OnTriggerEnter(Collider coll){
        moving=false;
        
    }
}
