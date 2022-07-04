using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnSliderChanged : MonoBehaviour, IPointerUpHandler
{
    public GameObject textBox;

    public void OnPointerUp(PointerEventData eventData)
    {
        textBox.GetComponent<TextMeshProUGUI>().SetText(gameObject.GetComponent<Slider>().value.ToString());
    }
    
}
