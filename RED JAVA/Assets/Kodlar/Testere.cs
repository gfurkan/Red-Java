using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
# endif


public class Testere : MonoBehaviour
{
    GameObject []gidilecekNoktalar;
    bool aradakiMesafeyiAl = true;
    bool ileriGeri = true;

    Vector3 aradakiMesafe;
    int sayac = 0;
    void Start()
    {
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
            aradakiMesafe=(gidilecekNoktalar[sayac].transform.position-transform.position).normalized;
            aradakiMesafeyiAl = false;
        }
        float mesafe = Vector3.Distance(transform.position, gidilecekNoktalar[sayac].transform.position);
        transform.position += aradakiMesafe * Time.deltaTime * 10;
        if(mesafe < 0.5f)
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
        for(int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);

        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, 35));
        noktalaraGit();
    }

}

[CustomEditor(typeof(Testere))]
[System.Serializable]
#if UNITY_EDITOR
class TestereEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Testere script = (Testere)target;
        if (GUILayout.Button("ÜRET", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject yeniObje = new GameObject();
            yeniObje.transform.parent = script.transform;
            yeniObje.transform.position = script.transform.position;
            yeniObje.name = script.transform.childCount.ToString();
        }
    }
}
#endif