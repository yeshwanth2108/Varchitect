using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class menu : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject mainmenu;
    public GameObject pos1;
    private Outline outline;
    private string selectedButton="";
    public bool menuDisplay=false;
    private GameObject character; 
    private CharacterMovement characterScript;
    public GameObject resume;
    public GameObject length;
    public GameObject speed;
    public GameObject exit;
    public int[] speeds = {5,10,20};
    public int i=1;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled=false;
        menuDisplay=false;
        mainmenu.SetActive(false);
        character = GameObject.Find("Character");
        characterScript=character.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(characterScript);
        if(Input.GetButtonDown("js7")){
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f+2f, 0f));
            if(menuDisplay){
                menuDisplay=!menuDisplay;
                mainmenu.SetActive(menuDisplay);
                //characterScript.enabled=false;
                Debug.Log("move");
            }else{
                menuDisplay=!menuDisplay;
                mainmenu.SetActive(menuDisplay);
                mainmenu.transform.position=pos1.transform.position + Camera.main.transform.forward *5f+new Vector3(0f,3f,0f);
                //characterScript.enabled=false;
                Debug.Log("no move");
            }
        }
        if(Input.GetButtonDown("js2")&&selectedButton=="resume"){
            resumeSce();
        }
        if(Input.GetButtonDown("js2")&&selectedButton=="length"){
            changeLength();
        }
        if(Input.GetButtonDown("js2")&&selectedButton=="speed"){
            speedCharacter();
        }
        if(Input.GetButtonDown("js2")&&selectedButton=="exit"){
            quitApp();
        }
        //Debug.Log(selectedButton);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Code to execute when the pointer enters the UI element
        Debug.Log("UI Element Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        if(eventData.pointerCurrentRaycast.gameObject.name=="resume"){
            selectedButton="resume";
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="length"){
            selectedButton="length";
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="speed"){
            selectedButton="speed";
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="exit"){
            selectedButton="exit";
        }
        
        
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        selectedButton="";
    }
    public void quitApp(){
        Debug.Log("Quit");
        Application.Quit();
        
    }
    public void resumeSce(){
        Debug.Log("resume");
        menuDisplay=!menuDisplay;
        mainmenu.SetActive(menuDisplay);
    }
    public void changeLength(){
        Debug.Log("length");
    }
    public void speedCharacter(){
        Debug.Log("speed");
        characterScript.speed=speeds[i];
        i=(i+1)%3;
        menuDisplay=!menuDisplay;
        mainmenu.SetActive(menuDisplay);
    }
}
