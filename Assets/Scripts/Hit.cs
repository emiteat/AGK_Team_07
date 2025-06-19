using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class hitScript : MonoBehaviour
{
    [SerializeField]
    private Main mainScript;

    public void Hit()
    {
        if (!mainScript.isPlayerTurn) return;

        mainScript.playerHand.Add(mainScript.DrawCard()); // 카드 한 장 추가
        int score = mainScript.CalculateScore(mainScript.playerHand); // 점수 계산
        mainScript.Turn++; // 턴 증가

        Debug.Log("Player Hand: " + string.Join(", ", mainScript.playerHand) + " (Score: " + score + ")");

        if (score > 21) // 점수가 21을 초과하면 Bust 처리
        {
            mainScript.Bust();
        }

        mainScript.UpdateUI();
    }

}

