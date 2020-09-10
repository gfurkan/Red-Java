using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class karakterKontrol : MonoBehaviour
{
    public Sprite[] yurumeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] beklemeAnim;


    public Text canText;

    SpriteRenderer spriteRenderer;
    Rigidbody2D fizik;
    GameObject kamera;
    public Image SiyahArkaPlan;

    Vector3 vec;
    Vector3 kameraIlkPos;
    Vector3 kameraSonPos;

    int can = 100;
    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;

    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    float horizontal = 0;
    float siyahArkaPlanSayac = 0;
    float anaMenuyeDonZaman = 0;

    bool ziplamakontrol = true;
    void Start()
    {
        Time.timeScale = 1;
        if(SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("kacincilevel"))
        {
            PlayerPrefs.SetInt("kacincilevel", SceneManager.GetActiveScene().buildIndex);
        }
       
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik=GetComponent<Rigidbody2D>();
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPos = kamera.transform.position - transform.position;
        canText.text = "Can : " + can;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ziplamakontrol)
            {
                fizik.AddForce(new Vector2(0, 700));
                ziplamakontrol = false;
            }
        }
        
    }
    void LateUpdate()
    {
        KameraKontrol();
    }
    void FixedUpdate()
    {
        KarakterHareket();
        Animasyon();
        if(can <= 0)
        {
            Time.timeScale = 0.4f;
            canText.enabled = false;
            siyahArkaPlanSayac += 0.03f;
            SiyahArkaPlan.color = new Color(0, 0, 0, siyahArkaPlanSayac);
            anaMenuyeDonZaman += Time.deltaTime;

            if(anaMenuyeDonZaman > 1)
            {
                SceneManager.LoadScene("AnaMenu");
            }
        }
    }
    
        
    
    void OnCollisionEnter2D(Collision2D col)
    {
        ziplamakontrol = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "kursun")
        {
            can -= 10;
            canText.text = "Can : " + can;
        }
        else if (col.gameObject.tag == "dusman")
        {
            can -= 25;
            canText.text = "Can : " + can;
        }
        else if (col.gameObject.tag == "testere")
        {
            can -= 15;
            canText.text = "Can : " + can;
        }
        else if (col.gameObject.tag == "hazine")
        {
            can += 1;
            canText.text = "Can : " + can;
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<Hazine>().enabled = true;
            Destroy(col.gameObject, 1);
        }
        else if (col.gameObject.tag == "karadelik")
        {
           
            if (SceneManager.GetActiveScene().buildIndex != 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            SceneManager.LoadScene("AnaMenu");
        }
    }
    void Animasyon()
    {
        if (ziplamakontrol)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.04f)
                {
                    spriteRenderer.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0.0f;
                }


            }
            if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.04f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (horizontal < 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.04f)
                {
                    spriteRenderer.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
                if (fizik.velocity.y > 0)
                {
                    spriteRenderer.sprite = ziplamaAnim[0];
                
                }
                if (fizik.velocity.y < 0)
                {
                    spriteRenderer.sprite = ziplamaAnim[1];
                  
                }

            }
        
    }
    void KarakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    void KameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPos, 0.04f);
    }
}
