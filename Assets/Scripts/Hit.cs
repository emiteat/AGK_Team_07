using UnityEngine;

public class hitScript : MonoBehaviour
{
    [SerializeField]
    public Main mainScript; // 게임 매니저에서 받아온거


    public void Hit()
    {
        if (mainScript.isPlayerTurn) 
        {
            mainScript.playerHand.Add(mainScript.DrawCard()); // 카드 한 장 추가
            int score = mainScript.CalculateScore(mainScript.playerHand); // 숫자 계산
            mainScript.Turn++; // 턴 증가

            Debug.Log("Player Hand: " + string.Join(", ", mainScript.playerHand) + " (Score: " + score + ")");
            //mainScript.DealerTurn();

            if (score > 21) // 점수가 21을 초과하면 Bust 처리
            {
                mainScript.Bust();
            }

            mainScript.UpdateUI();
        }
    }

}

