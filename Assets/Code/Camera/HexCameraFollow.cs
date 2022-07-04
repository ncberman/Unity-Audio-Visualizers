using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCameraFollow : MonoBehaviour
{
    GameObject currentHex;

    public void SetHex(GameObject go)
    {
        currentHex = go;
    }

    private void Update()
    {
        if(currentHex != null)
        {
            transform.position = new Vector3(currentHex.transform.position.x, (currentHex.GetComponent<HexRenderer>().height * currentHex.transform.localScale.y) + 10, currentHex.transform.position.z);
        }
    }
}
