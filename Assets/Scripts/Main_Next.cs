using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Next : MonoBehaviour
{
    public void toMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void toChoose()
    {
        SceneManager.LoadScene("Choose");
    }
    public void toGame()
    {
        SceneManager.LoadScene("Blackjack");
    }
}
