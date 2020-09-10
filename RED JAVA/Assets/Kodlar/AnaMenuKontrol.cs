using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnaMenuKontrol : MonoBehaviour
{

    GameObject level1, level2, level3;
    GameObject kilit1, kilit2, kilit3;
    GameObject leveller,kilitler;

    void Start()
    {
        Time.timeScale = 1;
        leveller = GameObject.Find("leveller");
        kilitler = GameObject.Find("kilitler");

        for(int i=0;i < leveller.transform.childCount; i++)
        {
            leveller.transform.GetChild(i).gameObject.SetActive(false);
        }
        

        for (int i = 0; i < kilitler.transform.childCount; i++)
        {
            kilitler.transform.GetChild(i).gameObject.SetActive(false);
        }
            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
        {
            leveller.transform.GetChild(i).GetComponent<Button>().interactable = true;
           
        }
    }

    void Update()
    {
        
    }
    public void ButonSec(int butonNo)
    {
        if (butonNo == 1)
        {
            SceneManager.LoadScene("level1");
        }
        if (butonNo == 2)
        {
            for (int i = 0; i < leveller.transform.childCount; i++)
            {
                leveller.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < kilitler.transform.childCount; i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(true);
            }


            for (int i = 0; i < PlayerPrefs.GetInt("kacincilevel"); i++)
            {
                kilitler.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if (butonNo == 3)
        {
            Application.Quit();
        }
    }
    public void LevelButonlari(int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }
}
