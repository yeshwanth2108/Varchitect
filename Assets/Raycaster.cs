using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public AudioSource source1;
    public AudioClip clip_audio;
    Outline highlightvar;
    public LineRenderer line_rend;
    public GameObject game_objj;
    public GameObject menu_loc;
    public GameObject scale_menu;
    public GameObject delete_menu;
    private bool add_plant_bool = false;

    public GameObject plant;
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
    bool dis_menu = false;
    bool dis_col_menu = false;
    bool is_scale_menu = false;
    bool is_glob_active = false;
    bool delete_menu_plant = false;

    public float lenraycast = 500f;
    private GameObject menuObject;
    private getButtonData buttonTextScript;
    private string buttonData;


    void Start()
    {
        highlightvar = GetComponent<Outline>();

        menu_loc.SetActive(false);
        scale_menu.SetActive(false);
        color_change_menu.SetActive(false);
        //source1.clip = clip_audio;
        menuObject = GameObject.Find("Controller");
        buttonTextScript = menuObject.GetComponent<getButtonData>();
        buttonData = "";
    }

    void Update()
    {

        buttonData = buttonTextScript.pressedButton;
        Vector3 ray_obj = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray_obj, Camera.main.transform.forward, out RaycastHit hit, lenraycast))
        {
            //source1.PlayOneShot(clip);
            //source1.Play();
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
                else if (first_hit.name.Contains("Plant Pot"))
                {
                    Debug.Log("..............................");
                    delete_menu_function();
                }
                else if (first_hit.name.Contains("door"))
                {
                    if (Input.GetButtonDown("js1"))
                    {
                        first_hit.SetActive(false);
                    }
                }
                else
                {
                    menu_functionality();
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
            //Debug.Log("chavakka");
            add_menu_objects(endPos);
            add_object(endPos);
            Debug.Log("whu");
        }
        rotate_Left();
        rotate_Right();
        move_Back();
        move_Front();
        move_Left();
        move_Right();
        scale_x_down();
        scale_x_up();
        scale_y_down();
        scale_y_up();
        changeColor();
        delete_plant();
    }
    void add_object(Vector3 end)
    {
        if (buttonData == "plant" && Input.GetButtonDown("js1"))
        {
            plant.SetActive(false);
            plant.transform.position = end;
            plant.SetActive(true);
            exit_func();
        }
    }
    void disable_menu()
    {
        if (dis_menu == false)
        {
            menu_loc.SetActive(false);
        }
    }
    void add_menu_objects(Vector3 endPos)
    {

        if (Input.GetButtonDown("js0"))
        {
            add_plant_bool = true;
            add_menu.SetActive(true);
            //Debug.Log("vamooooo");
            add_menu.transform.position = endPos;
            add_menu.transform.rotation = Quaternion.LookRotation(add_menu.transform.position - Camera.main.transform.position);
            disable_chr_ctrl();
        }
    }
    void delete_menu_function()
    {
        Debug.Log("deletee.......");
        if (Input.GetButtonDown("js0"))
        {
            delete_menu_plant = true;
            delete_menu.SetActive(true);
            Debug.Log("deletee.......");
            delete_menu.transform.position = new Vector3(first_hit.transform.position.x, first_hit.transform.position.y + 1.0f, first_hit.transform.position.z);
            delete_menu.transform.rotation = Quaternion.LookRotation(delete_menu.transform.position - Camera.main.transform.position);
            disable_chr_ctrl();
        }
    }
    void menu_functionality()
    {
        if (Input.GetButtonDown("js0") && highlit_enabled == true)
        {
            dis_menu = true;
            menu_loc.SetActive(true);
            menu_loc.transform.position = new Vector3(first_hit.transform.position.x, first_hit.transform.position.y + 5.0f, first_hit.transform.position.z);
            menu_loc.transform.rotation = Quaternion.LookRotation(menu_loc.transform.position - Camera.main.transform.position);
            disable_chr_ctrl();
        }

    }

    void menu_functionality_scale()
    {
        if (Input.GetButtonDown("js0") && highlit_enabled == true)
        {
            is_scale_menu = true;
            scale_menu.SetActive(true);
            scale_menu.transform.position = new Vector3(first_hit.transform.position.x, first_hit.transform.position.y + 1.0f, first_hit.transform.position.z);
            scale_menu.transform.rotation = Quaternion.LookRotation(scale_menu.transform.position - Camera.main.transform.position);
            disable_chr_ctrl();
        }

    }

    void color_menu_functionality()
    {
        if (Input.GetButtonDown("js0"))
        {
            dis_col_menu = true;
            color_change_menu.SetActive(true);
            color_change_menu.transform.position = new Vector3(first_hit.transform.position.x - 1.0f, first_hit.transform.position.y, first_hit.transform.position.z + 0.5f);
            color_change_menu.transform.rotation = Quaternion.LookRotation(color_change_menu.transform.position - Camera.main.transform.position);
            disable_chr_ctrl();
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

        if (Input.GetButtonDown("js1"))
        {

            Renderer rend = hit_ref.GetComponent<Renderer>();
            //Debug.Log(hit_ref.name);
            //Debug.Log(buttonData);
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
            }
            exit_func();
        }
    }

    public void disable_chr_ctrl()
    {
        ctrl_chr = GameObject.Find("Character");
        CharacterController char_control = ctrl_chr.GetComponent<CharacterController>();
        char_control.enabled = false;
    }

    public void enable_chr_ctrl()
    {
        ctrl_chr = GameObject.Find("Character");
        CharacterController char_control = ctrl_chr.GetComponent<CharacterController>();
        char_control.enabled = true;
    }

    public void rotate_Left()
    {
        if (buttonData == "rotateleft" && Input.GetButtonDown("js1"))
        {
            hit_ref.transform.rotation = Quaternion.Euler(
            hit_ref.transform.rotation.eulerAngles.x,
            hit_ref.transform.rotation.eulerAngles.y + 90,
            hit_ref.transform.rotation.eulerAngles.z);
            exit_func();
        }
    }
    public void delete_plant()
    {
        if (buttonData == "delete" && Input.GetButtonDown("js1"))
        {
            hit_ref.SetActive(false);
            exit_func();
        }
    }

    public void rotate_Right()
    {
        if (buttonData == "rotateright" && Input.GetButtonDown("js1"))
        {
            hit_ref.transform.rotation = Quaternion.Euler(
            hit_ref.transform.rotation.eulerAngles.x,
            hit_ref.transform.rotation.eulerAngles.y - 90,
            hit_ref.transform.rotation.eulerAngles.z);
            exit_func();
        }
    }

    public void move_Left()
    {
        if (buttonData == "moveleft" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.position;
            newPosition.z += 1;
            hit_ref.transform.position = newPosition;
            exit_func();
        }
    }

    public void move_Right()
    {
        if (buttonData == "moveright" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.position;
            newPosition.z -= 1;
            hit_ref.transform.position = newPosition;
            exit_func();
        }
    }

    public void move_Front()
    {
        if (buttonData == "movefront" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.position;
            newPosition.x -= 1;
            hit_ref.transform.position = newPosition;
            exit_func();
        }
    }

    public void move_Back()
    {
        if (buttonData == "moveback" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.position;
            newPosition.x += 1;
            hit_ref.transform.position = newPosition;
            exit_func();
        }
    }

    public void scale_y_down()
    {
        if (buttonData == "scaleydown" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.z -= 0.2f;
            hit_ref.transform.localScale = newPosition;
            exit_func();
        }
    }
    public void scale_y_up()
    {
        if (buttonData == "scaleyup" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.z += 0.2f;
            hit_ref.transform.localScale = newPosition;
            exit_func();
        }
    }
    public void scale_x_down()
    {
        if (buttonData == "scalexdown" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.x -= 0.2f;
            hit_ref.transform.localScale = newPosition;
            exit_func();
        }
    }
    public void scale_x_up()
    {
        if (buttonData == "scalexup" && Input.GetButtonDown("js1"))
        {
            Vector3 newPosition = hit_ref.transform.localScale;
            newPosition.x += 0.2f;
            hit_ref.transform.localScale = newPosition;
            exit_func();
        }
    }


    public void exit_func()
    {
        if (menu_loc.activeSelf)
        {
            dis_menu = false;
            menu_loc.SetActive(false);
            enable_chr_ctrl();
        }
        if (scale_menu.activeSelf)
        {
            is_scale_menu = false;
            scale_menu.SetActive(false);
            enable_chr_ctrl();
        }
        if (color_change_menu.activeSelf)
        {
            dis_col_menu = false;
            color_change_menu.SetActive(false);
            enable_chr_ctrl();
        }
        if (add_menu.activeSelf)
        {
            add_plant_bool = false;
            add_menu.SetActive(false);
            enable_chr_ctrl();
        }
        if (delete_menu.activeSelf)
        {
            delete_menu_plant = false;
            delete_menu.SetActive(false);
            enable_chr_ctrl();
        }
    }

    public void quit()
    {
        Application.Quit();
    }

}