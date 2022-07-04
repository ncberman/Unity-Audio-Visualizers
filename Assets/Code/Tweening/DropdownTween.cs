using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTween : MonoBehaviour
{
    public List<GameObject> content = new List<GameObject>();
    public GameObject contentPrefab;
    public float contentSpacing;
    public float speed;

    bool open;

    private void Start()
    {
        LeanTween.scaleY(gameObject, 0.0f, 0.0f);
        gameObject.SetActive(true);
        open = false;
        /*for (int i = 0; i < newContent.Count; i++)
        {
            GameObject temp = Instantiate(
                contentPrefab, 
                new Vector3(
                    transform.position.x, 
                    transform.position.y - (i * contentSpacing), 
                    transform.position.x
                    ),
                Quaternion.identity
                );
            temp.name = newContent[i];
            temp.GetComponent<TextMeshProUGUI>().SetText("  "+newContent[i]);
        }*/
    }

    public void Dropdown()
    {
        
        if(!open)
        {
            //LeanTween.cancel(gameObject);
            Debug.Log(content.Count);
            gameObject.transform.localScale.Set(1.0f, 0.0f, 1.0f);
            LeanTween.scaleY(gameObject, content.Count, speed);

            for(int i = 0; i < content.Count; i++)
            {
                content[i].GetComponent<TMPFadeIn>().FadeIn((speed / (content.Count-i)), 0.3f);
            }
            open = true;
        }
        else
        {
            LeanTween.cancel(gameObject);
            for(int i = 0; i < content.Count; i++)
            {
                content[i].GetComponent<TMPFadeIn>().FadeOut(0.0f, 0.0f);
            }
            LeanTween.scaleY(gameObject, 0.0f, 0.0f);
            //gameObject.SetActive(false);
            open = false;
        }
    }
}
