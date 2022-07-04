using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTrackName : MonoBehaviour
{
    public AudioClips script;
    private void Update()
    {
        gameObject.GetComponent<TMPro.TMP_Text>().SetText(script.currentClip.name);
    }
}
