using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofFade : MonoBehaviour
{
    float fade = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fade -= 1.0f * Time.deltaTime;
        transform.GetComponent<SpriteRenderer>().color = new Color(255f, 248f, 0f, fade);
    }
}
