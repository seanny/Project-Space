using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ButtonHoverOver");
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.instance.PlaySound("ButtonClick");
    }

}
