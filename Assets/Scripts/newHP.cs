using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class newHP : MonoBehaviour
{
    [SerializeField] public GameObject HP1;
    [SerializeField] public GameObject HP2;
    [SerializeField] public GameObject HP3;
    [SerializeField] public GameObject HP1_1;
    [SerializeField] public GameObject HP2_1;
    [SerializeField] public GameObject HP3_1;

    [SerializeField]
    public Main mainScript;

    public void Checker()
    {
        if (mainScript.Blood < 3)
        {
            HP1.SetActive(false);
            HP1_1.SetActive(true);
        }
        else if (mainScript.Blood < 2)
        {
            HP2.SetActive(false);
            HP2_1.SetActive(true);
        }
        else if (mainScript.Blood < 1)
        {
            HP3.SetActive(false);
            HP3_1.SetActive(true);
        }
    }
}
