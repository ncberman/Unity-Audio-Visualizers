using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartFadeIn : MonoBehaviour
{
    public float delay = 1f;
    public float speed = 1f;
    public float scale = 1f;
    public bool onStart = false;
    // Start is called before the first frame update
    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0);
        LeanTween.scale(gameObject, new Vector3(scale, scale, scale), speed).setDelay(delay);
    }
}
