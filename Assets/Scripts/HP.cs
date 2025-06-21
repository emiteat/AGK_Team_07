using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private Main HPScript;

    [SerializeField]
    public int Blood = 2;

    public void HPf()
    {
        if (HPScript.Dead == true)
        {
            Blood--;
            HPScript.Dead = false;
            HPScript.InitializeGame();
        }

    }
}
