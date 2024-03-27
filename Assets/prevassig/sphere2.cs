using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sphere2 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Outline outline;
     private GameObject[] characterarray;
    private GameObject character;  
    private Vector3 shere2;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled=false;
        characterarray = GameObject.FindGameObjectsWithTag("Character");
        character=characterarray[0];
        
    }

    // Update is called once per frame
    void Update()
    {
         if((outline.enabled)&&Input.GetButton("js7")){
            shere2=gameObject.transform.position;
            Debug.Log(shere2);
            gameObject.SetActive(false);
            character.SetActive(false);
            character.transform.position=shere2;
            character.SetActive(true);
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
