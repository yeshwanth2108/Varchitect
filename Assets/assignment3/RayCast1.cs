using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast1 : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer line;
    public float raycasterLength=100f;
    public Transform pos1;
    //public Transform pos2;
    private Vector3 centerOfScreen;
    private GameObject character;  
    private GameObject woodentableController;
    private woodentable woodenTableScript ;
    private GameObject telescopeController;
    private tele teleScript ;
    private GameObject chairController;
    private chair chairScript ;
    private GameObject pigController;
    private pig pigScript ;
    private GameObject binController;
    private bin binScript ;
    private CharacterMovement characterScript;
    private GameObject MainMenu_object; 
    private menu MainMenu_script;
    
    void Start()
    {
        line.positionCount=2;
        centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        woodentableController=GameObject.Find("WoodenTablecontroller");
        woodenTableScript= woodentableController.GetComponent<woodentable>();
        telescopeController=GameObject.Find("TelescopeController");
        teleScript= telescopeController.GetComponent<tele>();
        chairController=GameObject.Find("RockingController");
        chairScript= chairController.GetComponent<chair>();
        pigController=GameObject.Find("PiggyController");
        pigScript= pigController.GetComponent<pig>();
        binController=GameObject.Find("BinController");
        binScript= binController.GetComponent<bin>();
        character = GameObject.Find("Character");
        characterScript=character.GetComponent<CharacterMovement>();
        MainMenu_object = GameObject.Find("MainMemucontroller");
        MainMenu_script=MainMenu_object.GetComponent<menu>();
    }

    // Update is called once per frame
    void Update()
    {
        centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        //character movement
        if(woodenTableScript.menuDisplay||teleScript.menuDisplay||chairScript.menuDisplay||pigScript.menuDisplay||binScript.menuDisplay||MainMenu_script.menuDisplay){
            characterScript.enabled=false;
        }else{
            characterScript.enabled=true;
        }
        // if(woodenTableScript.menuDisplay||teleScript.menuDisplay||chairScript.menuDisplay||pigScript.menuDisplay||binScript.menuDisplay){
        //     characterScript.enabled=false;
        // }else{
        //     characterScript.enabled=true;
        // }
        // line.SetPosition(0, pos1.position);
        // //line.SetPosition(1, pos2.position);
        // line.SetPosition(1, centerOfScreen.origin + centerOfScreen.direction * 100f);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(centerOfScreen, Camera.main.transform.forward,out hit,raycasterLength))
        {
           // Debug.Log("Hit " + hit.collider.name); 
           //teleportation
            line.SetPosition(0, pos1.position);
            line.SetPosition(1, hit.point); // Draw line to where the ray hits an object
            if(hit.collider.name=="Plane"){
                if(Input.GetButtonDown("js5")){
                    Debug.Log("Hit " + hit.collider.name); 
                    Debug.Log("gggg");
                    Debug.Log(character);
                    character.SetActive(false);
                    character.transform.position=hit.point+new Vector3(0f,2f,0f);
                    character.SetActive(true);
                }
            }
        }
        else
        {
            line.SetPosition(0, pos1.position);
            line.SetPosition(1, new Vector3(0f,1f,0f)+pos1.transform.position + Camera.main.transform.forward * raycasterLength);
        }

        if (woodenTableScript.copiedObject != null && Input.GetButtonDown("js10"))
        {
            // Cast a ray from the center of the screen
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            // Check if the ray hits a collider
            if (Physics.Raycast(centerOfScreen, Camera.main.transform.forward, out hit))
            {
            // Check if the hit collider's name is "plane"
            //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Plane")
                {
                // Instantiate the copied object at the hit point
                // Debug.Log(woodenTableScript.copiedObject+" juju");
                Vector3 adjustedHitPoint = hit.point +new Vector3(-4f,2.2f,0f);
                // Debug.Log(hit.point);
                GameObject newObject = Instantiate(woodenTableScript.copiedObject, adjustedHitPoint, Quaternion.Euler(0, 180, 0));
                newObject.SetActive(true);

                }
            }
        }
        if (teleScript.copiedObject != null && Input.GetButtonDown("js10"))
        {
            // Cast a ray from the center of the screen
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            // Check if the ray hits a collider
            if (Physics.Raycast(centerOfScreen,  Camera.main.transform.forward,out hit))
            {
            // Check if the hit collider's name is "plane"
            //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Plane")
                {
                // Instantiate the copied object at the hit point
                // Debug.Log(woodenTableScript.copiedObject+" juju");
                Vector3 adjustedHitPoint = hit.point +new Vector3(-8f,0f,0f);
                // Debug.Log(hit.point);
                GameObject newObject=Instantiate(teleScript.copiedObject,adjustedHitPoint,Quaternion.Euler(0, 180, 0));
                newObject.SetActive(true);
                }
            }
        }
        if (chairScript.copiedObject != null && Input.GetButtonDown("js10"))
        {
            // Cast a ray from the center of the screen
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            // Check if the ray hits a collider
            if (Physics.Raycast(centerOfScreen, Camera.main.transform.forward, out hit))
            {
            // Check if the hit collider's name is "plane"
            //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Plane")
                {
                // Instantiate the copied object at the hit point
                // Debug.Log(woodenTableScript.copiedObject+" juju");
                Vector3 adjustedHitPoint = hit.point +new Vector3(0f,1f,-6f);
                // Debug.Log(hit.point);
                GameObject newObject = Instantiate(chairScript.copiedObject, adjustedHitPoint, Quaternion.Euler(0, 180, 0));
                newObject.SetActive(true);

                }
            }
        }
        if (pigScript.copiedObject != null && Input.GetButtonDown("js10"))
        {
            // Cast a ray from the center of the screen
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            

            // Check if the ray hits a collider
            if (Physics.Raycast(centerOfScreen, Camera.main.transform.forward, out hit))
            {
            // Check if the hit collider's name is "plane"
            //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Plane")
                {
                // Instantiate the copied object at the hit point
                // Debug.Log(woodenTableScript.copiedObject+" juju");
                Vector3 adjustedHitPoint = hit.point +new Vector3(3f,2.6f,12f);
                // Debug.Log(hit.point);
                GameObject newObject = Instantiate(pigScript.copiedObject, adjustedHitPoint, Quaternion.Euler(0, 180, 0));
                newObject.SetActive(true);

                }
            }
        }
        if (binScript.copiedObject != null && Input.GetButtonDown("js10"))
        {
            // Cast a ray from the center of the screen
            Vector3 centerOfScreen = Camera.main.ViewportToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            

            // Check if the ray hits a collider
            if (Physics.Raycast(centerOfScreen, Camera.main.transform.forward, out hit))
            {
            // Check if the hit collider's name is "plane"
            //Debug.Log(hit.collider.name);
                if (hit.collider.name == "Plane")
                {
                // Instantiate the copied object at the hit point
                // Debug.Log(woodenTableScript.copiedObject+" juju");
                Vector3 adjustedHitPoint = hit.point +new Vector3(3f,2.34f,-12f);
                // Debug.Log(hit.point);
                GameObject newObject = Instantiate(binScript.copiedObject, adjustedHitPoint, Quaternion.Euler(0, 180, 0));
                newObject.SetActive(true);

                }
            }
        }
        
    }
}
