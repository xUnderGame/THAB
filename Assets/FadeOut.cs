using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private int fadeTime = 20;
    private int counter;
    private int transp;
    void Start()
    {
        counter = 0;
        transp = 1;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (counter > fadeTime + 100) {
            counter = 0;
            transp = 1;
            GameManager.Instance.DisablePopUp();
        }
        else if (counter > fadeTime) {
            transp = 100 / (counter - fadeTime);
            if (transp < 0) transp = 0;
            else if (transp > 1) transp = 1;
            //GameManager.Instance.achievementPopUp.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, transp);
        }
    }
}
