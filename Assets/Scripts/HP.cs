using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private Main HPScript;

    [SerializeField]
    public SpriteRenderer HP_Renderer;
    [SerializeField]
    public Sprite Sprite01;


    [SerializeField]
    public int Blood = 2;
    [SerializeField]
    public int Num;

    public void HPf()
    {
        if(HPScript.Dead == true)
        {
            Blood--;
            HPScript.Dead = false;
            HPScript.InitializeGame();
        }
    }

    public void cardHP()
    {
        if (Blood <= Num)
        {
            HP_Renderer.sprite = Sprite01;
        }

    }
}
