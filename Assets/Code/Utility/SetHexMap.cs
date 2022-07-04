using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetHexMap : MonoBehaviour
{
    public HexMap hexMap;
    public void SetNumHexes()
    {
        int h = int.Parse(
            gameObject.GetComponent<TMP_InputField>().text
            );
        if (h >= 7 && h <= 1260) hexMap.mapSize = h;
        else if(h > 1260)
        {
            hexMap.mapSize = 1260;
            gameObject.GetComponent<TMP_InputField>().text = "1260";
        }
        else if (h < 7)
        {
            hexMap.mapSize = 7;
            gameObject.GetComponent<TMP_InputField>().text = "7";
        }

        hexMap.UpdateSettings();
    }

    public void SetHexHeight()
    {
        int h = int.Parse(
            gameObject.GetComponent<TMP_InputField>().text
            );
        if (h > hexMap.minHeight) hexMap.maxHeight = h;
        else
        {
            hexMap.maxHeight = hexMap.minHeight + 1;
            gameObject.GetComponent<TMP_InputField>().text = hexMap.maxHeight.ToString();
        }

        hexMap.UpdateSettings();
    }

    public void SetHexSize()
    {
        float h = float.Parse(
            gameObject.GetComponent<TMP_InputField>().text
            );
        if (h >= 0.1) hexMap.hexSize = h;
        else
        {
            hexMap.hexSize = 0.1f;
            gameObject.GetComponent<TMP_InputField>().text = "0.1";
        }
        hexMap.UpdateSettings();
    }

    public void SetHexOffset()
    {
        float h = float.Parse(
            gameObject.GetComponent<TMP_InputField>().text
            );
        if (h >= 0.0) hexMap.hexOffset = h;
        else
        {
            hexMap.hexOffset = 0.0f;
            gameObject.GetComponent<TMP_InputField>().text = "0.0";
        }
        hexMap.UpdateSettings();
    }
}
