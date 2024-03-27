using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class woodentable : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private GameObject charcterMovment;
    [SerializeField] public Button copyButton;
    [SerializeField] public Button cutButton;
    [SerializeField] public Button exitButton;
    private string selectedButton="";
    private Outline outline; 
     public GameObject Menu;
     public GameObject selectedObject;
    public bool menuDisplay; 
    [SerializeField] public GameObject copiedObject;  
    private Vector3 centerOfScreen;
    public bool pressedExit=false;
    private GameObject telescopeController;
    private tele teleScript ;
    private GameObject chairController;
    private chair chairScript ;
    private GameObject pigController;
    private pig pigScript ;
    private GameObject binController;
    private bin binScript ;
    private CharacterController char_control;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled=false;
        menuDisplay=false;
        Menu.SetActive(false);
        centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        telescopeController=GameObject.Find("TelescopeController");
        teleScript= telescopeController.GetComponent<tele>();
        chairController=GameObject.Find("RockingController");
        chairScript= chairController.GetComponent<chair>();
        pigController=GameObject.Find("PiggyController");
        pigScript= pigController.GetComponent<pig>();
        binController=GameObject.Find("BinController");
        binScript= binController.GetComponent<bin>();
    }

    // Update is called once per frame
    void Update()
    {
        charcterMovment = GameObject.Find("Character");
        char_control = charcterMovment.GetComponent<CharacterController>();
        centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if(Input.GetButtonDown("js2")&&outline.enabled){
            menuDisplay=!menuDisplay;
            Menu.SetActive(menuDisplay);
            selectedObject=gameObject;
            //copiedObject=gameObject;
            pigScript.menuDisplay=false;
            pigScript.Menu.SetActive(false);
            teleScript.menuDisplay=false;
            teleScript.Menu.SetActive(false);
            binScript.menuDisplay=false;
            binScript.Menu.SetActive(false);
            chairScript.menuDisplay=false;
            chairScript.Menu.SetActive(false);
            //char_control.enabled = false;
        }
        
        if(selectedButton=="copy"&&Input.GetButtonDown("js5")){
            teleScript.copiedObject=null;
            chairScript.copiedObject=null;
            pigScript.copiedObject=null;
            binScript.copiedObject=null;
            copy();
        }
        if(selectedButton=="cut"&&Input.GetButtonDown("js5")){
            teleScript.copiedObject=null;
            chairScript.copiedObject=null;
            pigScript.copiedObject=null;
            binScript.copiedObject=null;
            cut();
        }
        if(selectedButton=="exit"&&Input.GetButtonDown("js5")){
            exit();
        }
        if(menuDisplay){
            char_control.enabled = false;
        }else{
            char_control.enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Code to execute when the pointer enters the UI element
        outline.enabled=true;
        // Debug.Log(eventData);
        Debug.Log("UI Element Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        if(eventData.pointerCurrentRaycast.gameObject.name=="copy"){
            selectedButton="copy";
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="cut"){
            selectedButton="cut";
        }
        if(eventData.pointerCurrentRaycast.gameObject.name=="exit"){
            selectedButton="exit";
        }
        
        
    } 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Code to execute when the pointer exits the UI element
        outline.enabled = false;
        selectedButton="";
    }
    public void exit(){
        pressedExit=true;
        copiedObject=null;
        menuDisplay = false;
        Menu.SetActive(false);
        //char_control.enabled = true;
    }
    public void copy(){
        //Debug.Log(selectedObject);
        menuDisplay = false;
        Menu.SetActive(false);
        copiedObject=selectedObject;
        //char_control.enabled = true;
    }
    public void cut(){
        menuDisplay = false;
        Menu.SetActive(false);
        copiedObject = selectedObject;
        selectedObject.SetActive(false);
        //char_control.enabled = true;
    }
    
}
