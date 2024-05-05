using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public Light light;
    private bool lighton = false;
    Outline highlightvar;
    public LineRenderer line_rend;
    public GameObject game_objj;
    public GameObject move_menu;
    public GameObject rot_menu;
    public GameObject moveorrot_menu;
    public GameObject scale_menu;
    public GameObject delete_menu;
    private bool add_plant_bool = false;

    public GameObject plant;
    public GameObject basket;
    public GameObject cofeetable;
    public GameObject FreestandMirror;
    public GameObject color_change_menu;
    public GameObject add_menu;


    private GameObject first_hit;
    private GameObject hit_ref;
    private GameObject objj;

    private GameObject ctrl_chr;
    private GameObject dum_cu;
    private GameObject dum_cp;

    bool is_cut = false;
    bool is_copy = false;
    bool highlit_enabled = false;
    bool move_menu_bool = false;
    bool rot_menu_bool = false;
    bool moveorrot_menu_bool = false;
    bool dis_col_menu = false;
    bool is_scale_menu = false;
    bool is_glob_active = false;
    bool delete_menu_plant = false;
    bool is_door_open = false;

    public float lenraycast = 500f;
    private GameObject menuObject;
    private getButtonData buttonTextScript;
    private graphicraycaster gpray;
    private string buttonData;


    void Start()
    {
        highlightvar = GetComponent<Outline>();

        move_menu.SetActive(false);
        rot_menu.SetActive(false);
        moveorrot_menu.SetActive(false);
        scale_menu.SetActive(false);
        color_change_menu.SetActive(false);
        menuObject = GameObject.Find("Controller");
        buttonTextScript = menuObject.GetComponent<getButtonData>();
        buttonData = "";
    }

    void Update()
    {
        buttonData = buttonTextScript.pressedButton;
        Vector3 ray_obj = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(ray_obj, Camera.main.transform.forward, out RaycastHit hit, lenraycast))
        {
            first_hit = hit.collider.gameObject;
            line_rend.SetPosition(0, game_objj.transform.position);
            line_rend.SetPosition(1, hit.point);

            if (!first_hit.name.Contains("Base"))
            {
                hit_ref = first_hit;
                highlight_on(first_hit);
                if (first_hit.name.Contains("Curtain"))
                {
                    color_menu_functionality();
                }
                else if (first_hit.name.Contains("Master Bed"))
                {
                    menu_functionality_scale();
                }
                else if (first_hit.name.Contains("Plant Pot") || first_hit.name.Contains("Basket") || first_hit.name.Contains("CoffeeTable") || first_hit.name.Contains("FreestandMirror"))
                {
                    movnrot();
                    delete_menu_function();
                }
                else if (first_hit.name.Contains("Lamp Desk"))
                {
                    toggleLight();
                }
                else if(first_hit.name.Contains("DoorSingle"))
                {
                    toggleDoor();
                }
                else if (first_hit.name.Contains("main_door"))
                {
                    if (Input.GetButtonDown("js1"))
                    {
                        first_hit.SetActive(false);
                    }
                }
                else
                {
                    movnrot();
                }
            }
            else
            {
                if (hit_ref != null)
                {
                    highlight_off(hit_ref);
                }
            }
        }
        else
        {
            if (hit_ref != null)
            {
                highlight_off(hit_ref);
            }
            Vector3 endPos = Camera.main.transform.position + Camera.main.transform.forward * lenraycast;
            line_rend.SetPosition(0, game_objj.transform.position);
            line_rend.SetPosition(1, endPos);

            add_menu_objects(endPos);
        }
    }

    void toggleDoor()
    {
        if (Input.GetButtonDown("js1") && highlit_enabled == true)
        {
            AudioSource source = first_hit.GetComponent<AudioSource>();
            source.enabled = !source.enabled;
            if(is_door_open == false)
            {
            hit_ref.transform.rotation = Quaternion.Euler(
            hit_ref.transform.rotation.eulerAngles.x,
            hit_ref.transform.rotation.eulerAngles.y + 90,
            hit_ref.transform.rotation.eulerAngles.z);
            is_door_open = true;
          }
            else{
            hit_ref.transform.rotation = Quaternion.Euler(
            hit_ref.transform.rotation.eulerAngles.x,
            hit_ref.transform.rotation.eulerAngles.y -90,
            hit_ref.transform.rotation.eulerAngles.z);
            is_door_open = false;
            }        
        }
    }


    void toggleLight()
    {
        if (Input.GetButtonDown("js1"))
        {
            AudioSource source = first_hit.GetComponent<AudioSource>();
            source.enabled = !source.enabled;
            if (lighton)
            {
                light.color = Color.black;
                lighton = !lighton;
            }
            else
            {
                light.color = Color.white;
                lighton = !lighton;
            }
        }
    }

    public void add_object()
    {
        if (buttonData == "plant" && Input.GetButtonDown("js0"))
        {
            GameObject Object_Instance = Instantiate(plant);
            Vector3 endPos = Camera.main.transform.position + Camera.main.transform.forward * lenraycast;
            Object_Instance.transform.position = endPos;
            Object_Instance.SetActive(true);
        }
        if (buttonData == "basket" && Input.GetButtonDown("js0"))
        {
            GameObject Object_Instance = Instantiate(basket);
            Vector3 endPos = Camera.main.transform.position + Camera.main.transform.forward * lenraycast;
            Object_Instance.transform.position = endPos;
            Object_Instance.SetActive(true);    
        }
         if (buttonData == "cofeetable" && Input.GetButtonDown("js0"))
        {
            GameObject Object_Instance = Instantiate(cofeetable);
            Vector3 endPos = Camera.main.transform.position + Camera.main.transform.forward * lenraycast;
            Object_Instance.transform.position = endPos;
            Object_Instance.SetActive(true);
        }
        if (buttonData == "FreestandMirror" && Input.GetButtonDown("js0"))
        {
            GameObject Object_Instance = Instantiate(FreestandMirror);
            Vector3 endPos = Camera.main.transform.position + Camera.main.transform.forward * lenraycast;
            Object_Instance.transform.position = endPos;
            Object_Instance.SetActive(true);
        }
        if(buttonData.StartsWith("exit") && Input.GetButtonDown("js0"))
        {
            if (add_menu.activeSelf)
            {
                add_plant_bool = false;
                add_menu.SetActive(false);
            }
        }
        buttonData ="";
        if (add_menu.activeSelf)
        {
            add_plant_bool = false;
            add_menu.SetActive(false);
        }
    }

    void add_menu_objects(Vector3 endPos)
    {

        if (Input.GetButtonDown("js1"))
        {
            add_plant_bool = true;
            add_menu.SetActive(true);
            add_menu.transform.position = endPos;
            add_menu.transform.rotation = Quaternion.LookRotation(add_menu.transform.position - Camera.main.transform.position);
        }
    }

    void delete_menu_function()
    {
        if (Input.GetButtonDown("js1"))
        {
            delete_menu_plant = true;
            delete_menu.SetActive(true);
            
            Bounds objectBounds = first_hit.GetComponent<Renderer>().bounds;
            float objectHeight = objectBounds.size.y;

            float menuOffset = 0.5f;
            Vector3 menuPosition = new Vector3(first_hit.transform.position.x, 
                                            objectBounds.max.y + menuOffset, 
                                            first_hit.transform.position.z);
            delete_menu.transform.position = menuPosition;

            delete_menu.transform.rotation = Quaternion.LookRotation(delete_menu.transform.position - Camera.main.transform.position);

        }
    }

    // void menu_functionality()
    // {
    //     dis_menu = true;
    //     menu_loc.SetActive(true);
    //     Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
    //     float objectHeight = objectBounds.size.y;

    //     float menuOffset = 2.0f;
    //     Vector3 menuPosition = new Vector3(hit_ref.transform.position.x, 
    //                                        objectBounds.max.y + menuOffset, 
    //                                        hit_ref.transform.position.z);
    //     menu_loc.transform.position = menuPosition;
    //     menu_loc.transform.rotation = Quaternion.LookRotation(menu_loc.transform.position - Camera.main.transform.position);

    // }

    void movnrot()
    {
        if (Input.GetButtonDown("js0") && highlit_enabled == true)
        {
        moveorrot_menu_bool = true;
        moveorrot_menu.SetActive(true);
        Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
        float objectHeight = objectBounds.size.y;

        float menuOffset = 2.0f;
        Vector3 menuPosition = new Vector3(hit_ref.transform.position.x, 
                                           objectBounds.max.y + menuOffset, 
                                           hit_ref.transform.position.z);
        moveorrot_menu.transform.position = menuPosition;
        moveorrot_menu.transform.rotation = Quaternion.LookRotation(moveorrot_menu.transform.position - Camera.main.transform.position);

        }
    }

    void menu_functionality_scale()
    {
        if (Input.GetButtonDown("js0") && highlit_enabled == true)
        {
        is_scale_menu = true;
        scale_menu.SetActive(true);
        
        Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
        float objectHeight = objectBounds.size.y;

        float menuOffset = 1.5f; 
        Vector3 menuPosition = new Vector3(hit_ref.transform.position.x, 
                                           objectBounds.max.y + menuOffset, 
                                           hit_ref.transform.position.z);
        scale_menu.transform.position = menuPosition;

        scale_menu.transform.rotation = Quaternion.LookRotation(scale_menu.transform.position - Camera.main.transform.position);

        }

    }

    void color_menu_functionality()
    {
        if (Input.GetButtonDown("js0") && highlit_enabled == true)
        {
            dis_col_menu = true;
            color_change_menu.SetActive(true);
            
            Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
            float objectHeight = objectBounds.size.y;

            float menuOffset = -1.5f; 
            Vector3 menuPosition = new Vector3(hit_ref.transform.position.x - 1.0f, 
                                            objectBounds.max.y + menuOffset, 
                                            hit_ref.transform.position.z - 1f);
            color_change_menu.transform.position = menuPosition;

            color_change_menu.transform.rotation = Quaternion.LookRotation(color_change_menu.transform.position - Camera.main.transform.position);

        }
    }

    void highlight_on(GameObject oj)
    {
        Outline highlightvar = oj.GetComponent<Outline>();
        if (highlightvar != null)
        {
            highlightvar.enabled = true;
            highlit_enabled = true;
        }
    }

    void highlight_off(GameObject oj)
    {
        Outline highlightvar = oj.GetComponent<Outline>();
        if (highlightvar != null)
        {
            highlightvar.enabled = false;
            highlit_enabled = false;
        }
    }

    public void changeColor()
    {
        Renderer rend = hit_ref.GetComponent<Renderer>();
        if (rend != null)
        {
            if (buttonData == "red")
            {
                rend.material.color = Color.red;
            }
            if (buttonData == "green")
            {
                rend.material.color = Color.green;
            }
            if (buttonData == "blue")
            {
                rend.material.color = Color.blue;
            }
            if (buttonData == "yellow")
            {
                rend.material.color = Color.yellow;
            }
            if (buttonData == "gray")
            {
                rend.material.color = Color.gray;
            }
            if (buttonData == "white")
            {
                rend.material.color = Color.white;
            }
            buttonData = "";
        if (color_change_menu.activeSelf)
        {
            dis_col_menu = false;
            color_change_menu.SetActive(false);
        }        }
    }


    public void moveAndRotate()
    {
        if ( Input.GetButtonDown("js0") && buttonData.StartsWith("rotate"))
        {
            float angle = buttonData == "rotateleft" ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            hit_ref.transform.rotation *= rotation;
            buttonData = "";
        }
        else if (Input.GetButtonDown("js0") && buttonData.StartsWith("move"))
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            Vector3 movementDirection = Vector3.zero;
            if (buttonData == "moveleft")
                movementDirection = -cameraRight;
            else if (buttonData == "moveright")
                movementDirection = cameraRight;
            else if (buttonData == "movefront")
                movementDirection = cameraForward;
            else if (buttonData == "moveback")
                movementDirection = -cameraForward;

            movementDirection.Normalize();
            hit_ref.transform.position += movementDirection;

            if (move_menu_bool)
            {
                move_menu.transform.position += movementDirection;
            }
            buttonData = "";
        }
        else if(Input.GetButtonDown("js0") && buttonData.StartsWith("exit"))
        {
            buttonData = "";    
        if (move_menu.activeSelf)
        {
            move_menu_bool = false;
            move_menu.SetActive(false);
        }
        if (rot_menu.activeSelf)
        {
            rot_menu_bool = false;
            rot_menu.SetActive(false);
        }       
        }
    }

    public void movnrot_menu()
    {
        if ( Input.GetButtonDown("js0") && buttonData.StartsWith("Move"))
        {
            move_menu_bool = true;
            move_menu.SetActive(true);
            Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
            float objectHeight = objectBounds.size.y;

            float menuOffset = 2.0f;
            Vector3 menuPosition = new Vector3(hit_ref.transform.position.x, 
                                            objectBounds.max.y + menuOffset, 
                                            hit_ref.transform.position.z);
            move_menu.transform.position = menuPosition;
            move_menu.transform.rotation = Quaternion.LookRotation(move_menu.transform.position - Camera.main.transform.position);
        }
        else if ( Input.GetButtonDown("js0") && buttonData.StartsWith("Rotate"))
        {
            rot_menu_bool = true;
            rot_menu.SetActive(true);
            Bounds objectBounds = hit_ref.GetComponent<Renderer>().bounds;
            float objectHeight = objectBounds.size.y;

            float menuOffset = 2.0f;
            Vector3 menuPosition = new Vector3(hit_ref.transform.position.x, 
                                            objectBounds.max.y + menuOffset, 
                                            hit_ref.transform.position.z);
            rot_menu.transform.position = menuPosition;
            rot_menu.transform.rotation = Quaternion.LookRotation(rot_menu.transform.position - Camera.main.transform.position);
        }
        buttonData = "";
        if (moveorrot_menu.activeSelf)
        {
            moveorrot_menu_bool = false;
            moveorrot_menu.SetActive(false);
        }    
    }


    public void delete_plant()
    {
        if (buttonData == "delete" && Input.GetButtonDown("js0"))
        {
            hit_ref.SetActive(false);
        if (delete_menu.activeSelf)
                {
                    delete_menu_plant = false;
                    delete_menu.SetActive(false);
                }        
        }
        buttonData = "";
    }

    public void scale_all_dir()
    {
        if (Input.GetButtonDown("js0") && buttonData == "scaleydown")
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.z -= 0.2f;
            hit_ref.transform.localScale = newPosition;
            buttonData = "";
        }

        else if (Input.GetButtonDown("js0") && buttonData == "scaleyup")
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.z += 0.2f;
            hit_ref.transform.localScale = newPosition;
             buttonData = "";
        }
        else if (Input.GetButtonDown("js0") && buttonData == "scalexdown")
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.x -= 0.2f;
            hit_ref.transform.localScale = newPosition;
            buttonData = "";
        }
        else if (Input.GetButtonDown("js0") && buttonData == "scalexup")
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.x += 0.2f;
            hit_ref.transform.localScale = newPosition;
            buttonData = "";
        }
        else if(Input.GetButtonDown("js0") && buttonData.StartsWith("exit"))
        {
            if (scale_menu.activeSelf)
            {
                is_scale_menu = false;
                scale_menu.SetActive(false);
             }     
            buttonData = "";  
        }

    }


    public void quit()
    {
        Application.Quit();
    }

}