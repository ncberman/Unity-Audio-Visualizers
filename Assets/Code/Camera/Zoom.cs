using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public int sensitivity = 10;

    StateController sc = StateController.Instance;
    Camera thisCamera;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sc.IsIslandView())
        {
            float scroll = Input.mouseScrollDelta.y;
            if(scroll < 0 && thisCamera.orthographicSize < 50 || scroll > 0 && thisCamera.orthographicSize > 20)
            {
                thisCamera.orthographicSize -= sensitivity * scroll * Time.deltaTime;
                if (thisCamera.orthographicSize < 20) thisCamera.orthographicSize = 20;
                if (thisCamera.orthographicSize > 50) thisCamera.orthographicSize = 50;
            }
        }
    }
    
}
