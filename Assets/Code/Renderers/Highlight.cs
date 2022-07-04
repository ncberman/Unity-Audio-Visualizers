using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour
{
    [SerializeField] Material color;
    [SerializeField] Material highlightColor;

    void OnMouseEnter()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            color = GetComponent<MeshRenderer>().material;
            GetComponent<MeshRenderer>().material = highlightColor;
        }
    }
    void OnMouseExit()
    {
            GetComponent<MeshRenderer>().material = color;
    }
}
