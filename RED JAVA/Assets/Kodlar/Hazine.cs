using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazine : MonoBehaviour
{
    public Sprite[] hazineAnim;
    SpriteRenderer spriteRenderer;
    int animasyonSayac = 0;
    float zaman = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
     
    }

    void Update()
    {
        zaman += Time.deltaTime;
        if (zaman >0.05f)
        {

            spriteRenderer.sprite = hazineAnim[animasyonSayac++];
            if (animasyonSayac == hazineAnim.Length)
            {
                animasyonSayac = hazineAnim.Length - 1;

            }
            zaman = 0;
        }
    }
  
       
    
}
