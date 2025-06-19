using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dieScript : MonoBehaviour
{
    [SerializeField]
    private Main die;

    public void Die()
    {
        if(die.Turn < 1)
        {
            die.Bust();
        }
    }
}
