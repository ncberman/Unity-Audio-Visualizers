using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMPFadeIn : MonoBehaviour
{
    TextMeshProUGUI tmp;

    public void FadeIn(float delay, float speed)
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        tmp.alpha = 0.0f;
        LeanTween.value(
            gameObject, ChangeTmpColor,
            new Color(255, 255, 255, 0),
            new Color(255, 255, 255, 1),
            speed
            )
            .setDelay(delay)
            ;
        gameObject.SetActive(true);
    }

    public void FadeOut(float delay, float speed)
    {
        tmp = gameObject.GetComponent<TextMeshProUGUI>();
        tmp.alpha = 0.0f;
        LeanTween.value(
            gameObject, ChangeTmpColor,
            new Color(255, 255, 255, 1),
            new Color(255, 255, 255, 0),
            speed
            )
            .setDelay(delay)
            ;
        gameObject.SetActive(false);
    }

    private void ChangeTmpColor(Color c)
    {
        if (tmp == null) return;
        tmp.color = c;
    }
}
