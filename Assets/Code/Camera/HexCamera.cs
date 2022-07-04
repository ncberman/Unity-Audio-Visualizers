using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexCamera : MonoBehaviour
{
    GameObject mainCamera;
    GameObject hexCam;
    StateController sc = StateController.Instance;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        hexCam = GameObject.Find("Hex Camera");
    }

    private void Update()
    {
        if(sc.IsHexView())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hexCam.GetComponent<Camera>().enabled = false;
                mainCamera.GetComponent<Camera>().enabled = true;
                sc.SetIslandView();
            }
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                hexCam.transform.position = new Vector3(gameObject.transform.position.x, (gameObject.GetComponent<HexRenderer>().height * gameObject.transform.localScale.y) + 10, gameObject.transform.position.z);
                hexCam.transform.eulerAngles = new Vector3(
                    90,
                    hexCam.transform.eulerAngles.y,
                    hexCam.transform.eulerAngles.z
                    );

                hexCam.GetComponent<Camera>().enabled = true;
                hexCam.GetComponent<HexCameraFollow>().SetHex(gameObject);
                mainCamera.GetComponent<Camera>().enabled = false;
                sc.SetHexView();
            }
        }
    }
}
