using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cards : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> cardImages = new List<Sprite>(14);

    //public List<Sprite> CardImages => CardImages;

    private SpriteRenderer render;

    public Main main;

    [SerializeField]
    private int cardNum;

    public void cardChange()
    {
        string cardStd = main.playerHand[cardNum];
        int value = 0;

        switch (cardStd)
        {
            case "J":
                value = 11;
                break;
            case "Q":
                value = 12;
                break;
            case "K":
                value = 13;
                break;
            case "A":
                value = 1;
                break;
            default:
                int.TryParse(cardStd, out value);
                break;
        }
        render.sprite = cardImages[value - 1];
    }
}