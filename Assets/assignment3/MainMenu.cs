using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject mainmenu;
    public GameObject pos1;
    private Outline outline;
    // private GameObject woodentableController;
    // private woodentable woodenTableScript ;
    // private GameObject telescopeController;
    // private tele teleScript ;
    // private GameObject chairController;
    // private chair chairScript ;
    public bool menuDisplay=false;
    // private GameObject pigController;
    // private pig pigScript ;
    // private GameObject binController;
    // private bin binScript ;
    private GameObject linerender;
    private RayCast1 raycasterScript ;
    private string selectedButton="";
    public int test=0;
    
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled=false;
        menuDisplay=false;
        mainmenu.SetActive(false);
        // woodentableController=GameObject.Find("WoodenTablecontroller");
        // woodenTableScript= woodentableController.GetComponent<woodentable>();
        // telescopeController=GameObject.Find("TelescopeController");
        // teleScript= telescopeController.GetComponent<tele>();
        // chairController=GameObject.Find("RockingController");
        // chairScript= chairController.GetComponent<chair>();
        // pigController=GameObject.Find("PiggyController");
        // pigScript= pigController.GetComponent<pig>();
        // binController=GameObject.Find("BinController");
        // binScript= binController.GetComponent<bin>();
        linerender=GameObject.Find("Linerender");
        raycasterScript= linerender.GetComponent<RayCast1>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(test==0){
            menuDisplay=false;
        mainmenu.SetActive(false);
        test+=1;
        }
        if(Input.GetButtonDown("js7")){
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f+2f, 0f));
            if(menuDisplay){
                //mainmenu.transform.position=pos1.transform.position + Camera.main.transform.forward *5f;
                menuDisplay=!menuDisplay;
                mainmenu.SetActive(menuDisplay);
                
                // Vector3 direction = (centerOfScreen - mainmenu.transform.position).normalized;
                // mainmenu.transform.position = centerOfScreen + direction * 100f;
                //mainmenu.transform.rotate=
                //Instantiate(mainmenu, centerOfScreen, Quaternion.identity);
            }else{
                 menuDisplay=!menuDisplay;
                mainmenu.SetActive(menuDisplay);
                mainmenu.transform.position=pos1.transform.position + Camera.main.transform.forward *5f;
            }
            //Debug.Log(menuDisplay);
        }
        
        Debug.Log(selectedButton);
        if(selectedButton=="length"&&Input.GetButtonDown("js2")){
                raycasterScript.raycasterLength=100f;
        }
        
    }
    public void OnPointerEnter(PointerEventData eventData)
        {
        // Code to execute when the pointer enters the UI element
        outline.enabled=true;
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
        // Code to execute when the pointer exits the UI element
        outline.enabled = false;
        }
    public void quitApp(){
        Application.Quit();
        Debug.Log("Quit");
    }
   
}