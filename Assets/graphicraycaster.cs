using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class graphicraycaster : MonoBehaviour
{
    GraphicRaycaster menu_inter_raycast;
    PointerEventData menu_Pointer_Data;
    EventSystem event_menu;
    Button clk_btn;
    public GameObject obj_ch;
    Vector3 loc_hit;

    void Start()
    {
        menu_inter_raycast = GetComponent<GraphicRaycaster>();
        event_menu = GetComponent<EventSystem>();
        loc_hit = Camera.main.transform.forward;
    }

    void Update()
    {
            UpdateMenuPointerData();
            List<RaycastResult> list_op = new List<RaycastResult>();
            menu_inter_raycast.Raycast(menu_Pointer_Data, list_op);

            if (list_op.Count > 0)
            {
                obj_ch = GameObject.Find("Character");

                foreach (RaycastResult op in list_op)
                {
                    HandleRaycastResult(op);
                }
            }
            else
            {
                if (clk_btn != null)
                {
                    clk_btn.OnPointerExit(null);
                    clk_btn = null;
                }
            }
    }

    void UpdateMenuPointerData()
    {
        menu_Pointer_Data = new PointerEventData(event_menu);
        menu_Pointer_Data.position = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    void HandleRaycastResult(RaycastResult result)
    {
        GameObject hitObject = result.gameObject;

        if (IsSpecialObject(hitObject))
        {
            loc_hit = hitObject.transform.position;
        }

        if (obj_ch != null)
        {
            SetCharacterPosition();
        }

        CheckButtonClicked(hitObject);
    }

    HashSet<string> specialObjectNames = new HashSet<string> { "Cut", "Copy", "Exit","red" };

    bool IsSpecialObject(GameObject obj)
    {
        return specialObjectNames.Contains(obj.name);
    }

    void SetCharacterPosition()
    {
        setlrpos();
    }

    void CheckButtonClicked(GameObject hitObject)
    {
        clk_btn = hitObject.GetComponentInParent<Button>();
        chech_clk_btn();
    }

    void setlrpos()
    {
        LineRenderer lr = obj_ch.GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.SetPosition(1, loc_hit);
        }
    }

    void chech_clk_btn()
    {
        if (clk_btn != null)
        {
            clk_btn.OnPointerEnter(null);
            if (Input.GetButton("js0"))
            {
                clk_btn.onClick.Invoke();
            }

        }
    }
}