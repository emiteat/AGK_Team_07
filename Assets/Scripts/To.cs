using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class To : MonoBehaviour
{
    [SerializeField]
    public Main mainScript;

    [SerializeField]
    public GameObject Settings;

    public void toChoose()
    {
        SceneManager.LoadScene("Choose");
    }
    public void toMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void toRetry()
    {
        mainScript.InitializeGame();
    }
    public void toContinue()
    {
        Settings.SetActive(false);
    }
}
