using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public GameObject Blackjack;

    void Start()
    {
        Main.GetComponent<Main>.Hit();    
    }


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

