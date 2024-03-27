using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorChange : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    
    Renderer ren;
    private Outline outline;
    void Start()
    {
        //changeColor=false;
        ren=GetComponent<Renderer>();
        ren.material.color=Color.black;
        outline = GetComponent<Outline>();
        outline.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
       if((outline.enabled)&&Input.GetButtonDown("js7")){
           if(ren.material.color==Color.black){
            ren.material.color=Color.yellow;
           }else{
            ren.material.color=Color.black;
           }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Code to execute when the pointer enters the UI element
        outline.enabled=true;
        // if((outline.enabled)&&Input.GetButton("js7")){
        //    if(ren.material.color==Color.black){
        //     ren.material.color=Color.yellow;
        //    }else{
        //     ren.material.color=Color.black;
        //    }
        // }
        
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Code to execute when the pointer exits the UI element
        outline.enabled = false;
    }
    
}
