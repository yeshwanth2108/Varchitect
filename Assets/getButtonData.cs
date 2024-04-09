using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class getButtonData : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    public string pressedButton;
    void Start()
    {
        pressedButton = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        //Debug.Log("Button Highlighted: " + eventData.pointerCurrentRaycast.gameObject.name);
        pressedButton = eventData.pointerCurrentRaycast.gameObject.name;
    }
}
