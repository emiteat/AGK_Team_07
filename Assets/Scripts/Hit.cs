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

        playerHand.Add(DrawCard()); // ī�� �� �� �߰�
        int score = CalculateScore(playerHand); // ���� ���
        Turn++; // �� ����

        Debug.Log("Player Hand: " + string.Join(", ", playerHand) + " (Score: " + score + ")");

        if (score > 21) // ������ 21�� �ʰ��ϸ� Bust ó��
        {
            Bust();
        }

        UpdateUI();
    }

}

