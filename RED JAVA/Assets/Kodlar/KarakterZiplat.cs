using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterZiplat : MonoBehaviour
{
    public Sprite[] ziplamaAnim;
    float beklemesuresi = 0;
    bool ziplamakontrol = true;
    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
       
            ZiplamaAnimasyon();
        
    }
    void Update()
    {
        if (ziplamakontrol)
        {
            spriteRenderer.sprite = ziplamaAnim[2];
            beklemesuresi += Time.deltaTime;
            if (beklemesuresi > 0.2f)
            {
                fizik.AddForce(new Vector2(0, 200));
                ziplamakontrol = false;
                beklemesuresi = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        ziplamakontrol = true;
    }
    void ZiplamaAnimasyon()
    {

        if (fizik.velocity.y > 0)
        {
            spriteRenderer.sprite = ziplamaAnim[1];
        }
        if (fizik.velocity.y < 0)
        {
            spriteRenderer.sprite = ziplamaAnim[0];
        }
      

    }
  
        
   
}
