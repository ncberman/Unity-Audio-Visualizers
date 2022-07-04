using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject centerObject;
    public float dragSpeed = 120;
    private Vector3 dragOrigin;

    StateController sc = StateController.Instance;

    // Update is called once per frame
    void Update()
    {
        if(sc.IsIslandView())
        {
            if (cam.enabled)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    dragOrigin = cam.ScreenToViewportPoint(Input.mousePosition);
                }
                if (Input.GetMouseButton(1))
                {
                    Vector3 dragPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                    Vector3 direction = dragOrigin - dragPosition;

                    float rotationAroundYAxis = -direction.x * dragSpeed;

                    transform.RotateAround(centerObject.transform.position, Vector3.up, rotationAroundYAxis);
                    dragOrigin = cam.ScreenToViewportPoint(Input.mousePosition);
                }
            }
        }
    }
}
