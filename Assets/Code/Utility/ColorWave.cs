using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWave : MonoBehaviour
{
    public float speed = 10f;
    public float brightest = 0.7f;
    public float darkest = 0.3f;
    Material m;

    char phase = 'R';

    // Start is called before the first frame update
    void Start()
    {
        m = gameObject.GetComponent<MeshRenderer>().materials[0];
        m.SetColor("_BaseColor", new Color(brightest, darkest, darkest));
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = m.GetColor("_BaseColor");
        if(phase == 'R') // turn green
        {
            if(curColor.g < brightest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r,
                        curColor.g + 1 * speed * Time.deltaTime,
                        curColor.b
                        )
                    );
            }
            else if (curColor.r > darkest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r - 1 * speed * Time.deltaTime,
                        curColor.g,
                        curColor.b
                        )
                    );
            }
            else
            {
                phase = 'G';
            }
        }
        else if (phase == 'G') // turn blue
        {
            if (curColor.b < brightest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r,
                        curColor.g,
                        curColor.b + 1 * speed * Time.deltaTime
                        )
                    );
            }
            else if (curColor.g > darkest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r,
                        curColor.g - 1 * speed * Time.deltaTime,
                        curColor.b
                        )
                    );
            }
            else
            {
                phase = 'B';
            }
        }
        else if (phase == 'B') // turn green
        {
            if (curColor.r < brightest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r + 1 * speed * Time.deltaTime,
                        curColor.g,
                        curColor.b
                        )
                    );
            }
            else if (curColor.b > darkest)
            {
                m.SetColor(
                    "_BaseColor",
                    new Color(
                        curColor.r,
                        curColor.g,
                        curColor.b - 1 * speed * Time.deltaTime
                        )
                    );
            }
            else
            {
                phase = 'R';
            }
        }
    }
}
