using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> cardImages;

    [SerializeField]
    public Image render;

    public Main main;

    [SerializeField]
    public int DCNum;

    [SerializeField]
    int value = 0;


    private void Awake()
    {
        render = GetComponent<Image>();

    }
    public void DCChange()
    {
        string cardStd = main.dealerHand[DCNum];

        switch (cardStd)
        {
            case "J": 
                value = 11;
                render.sprite = cardImages[value];
                break;
            case "Q":
                value = 12;
                render.sprite = cardImages[value];
                break;
            case "K":
                value = 13;
                render.sprite = cardImages[value];  
                break;
            case "A":
                value = 1;
                render.sprite = cardImages[value];
                break;
            default:
                value = int.Parse(cardStd);
                render.sprite = cardImages[value];
                break;
        }
    }
}