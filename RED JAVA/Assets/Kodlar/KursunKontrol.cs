using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KursunKontrol : MonoBehaviour
{
    DusmanKontrol dusman;
    Rigidbody2D fizik;
    void Start()
    {
        fizik= GetComponent<Rigidbody2D>();
        dusman = GameObject.FindGameObjectWithTag("dusman").GetComponent<DusmanKontrol>();
        fizik.AddForce(dusman.getYon() * 1000);
    }

}
