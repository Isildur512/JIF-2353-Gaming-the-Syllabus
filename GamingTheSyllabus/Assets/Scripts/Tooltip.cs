using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string message;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.SetAndShowTooltip(message);
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        TooltipManager.HideToolTip();
    }

    public void setMessage(string message)
    {
        this.message = message;
    }
}
