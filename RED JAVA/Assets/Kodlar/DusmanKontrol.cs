using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
# endif


public class DusmanKontrol : MonoBehaviour
{
    public Sprite Gordu;
    public Sprite Gormedi;
    public GameObject kursun;

    SpriteRenderer spriteRenderer;
    GameObject[] gidilecekNoktalar;
    GameObject karakter;
    RaycastHit2D ray;


    public LayerMask layermask;

    float atesZamani = 0;
    int hiz = 2;
    bool aradakiMesafeyiAl = true;
    bool ileriGeri = true;

    Vector3 aradakiMesafe;
    int sayac = 0;
    void Start()
    {


        spriteRenderer = GetComponent<SpriteRenderer>();
        karakter = GameObject.FindGameObjectWithTag("karakter");
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }
    void noktalaraGit()
    {
        if (aradakiMesafeyiAl)
        {
            aradakiMesafe = (gidilecekNoktalar[sayac].transform.position - transform.position).normalized;
            aradakiMesafeyiAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[sayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * hiz;
        if (mesafe < 0.5f)
        {

            aradakiMesafeyiAl = true;
            if (sayac == gidilecekNoktalar.Length - 1)
            {
                ileriGeri = false;

            }
            else if (sayac == 0)
            {
                ileriGeri = true;
            }
            if (ileriGeri)
            {
                sayac++;
            }
            else
            {
                sayac--;
            }
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);

        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif

    void FixedUpdate()
    {
        DusmanGordumu();
        if (ray.collider.tag == "karakter")
        {

            hiz = 8;
            spriteRenderer.sprite = Gordu;
            AtesEt();
        }
        else
        {
            hiz = 2;
            spriteRenderer.sprite = Gormedi;
        }
        noktalaraGit();
       
    }
    void AtesEt()
    {
        atesZamani += Time.deltaTime;
        if(atesZamani > Random.Range(0.2f, 1))
            {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }
    public Vector3 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }
    void DusmanGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000,layermask);
        Debug.DrawLine(transform.position, ray.point, Color.gray);
    }
  
    
}

[CustomEditor(typeof(DusmanKontrol))]
[System.Serializable]
#if UNITY_EDITOR
class DusmanKontrolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DusmanKontrol script = (DusmanKontrol)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layermask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Gordu"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Gormedi"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif