using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Main Blackjack;

    public 


    public void Hit()
    {
        if (!isPlayerTurn) return;

        playerHand.Add(DrawCard()); // 카드 한 장 추가
        int score = CalculateScore(playerHand); // 점수 계산
        Turn++; // 턴 증가

        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + score + ")");

        if (score > 21) // 점수가 21을 초과하면 Bust 처리
        {
            Bust();
        }

        UpdateUI();
    }

}

