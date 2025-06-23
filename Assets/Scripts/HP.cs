using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    private Main MainScript;

    [SerializeField]
    public SpriteRenderer HP_Renderer;
    [SerializeField]
    public Sprite Sprite01;

    [SerializeField]
    public int Num = 0;

    public void HPf()
    {
        if (MainScript.Dead == true)
        {
            MainScript.Dead = false;
            MainScript.InitializeGame();
        }
    }

    public void cardHP()
    {
        if (MainScript.Blood <= Num)
        {
            HP_Renderer.sprite = Sprite01;
        }

    }
}
