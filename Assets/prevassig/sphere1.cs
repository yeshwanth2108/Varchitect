using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class sphere1 : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Outline outline;
     private GameObject[] characterarray;
    private GameObject character;  
    private Vector3 shere1;
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
            shere1=gameObject.transform.position;
            Debug.Log(shere1);
            gameObject.SetActive(false);
            character.SetActive(false);
            character.transform.position=shere1;
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
