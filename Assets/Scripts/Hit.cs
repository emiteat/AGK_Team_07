using UnityEngine;

public class hitScript : MonoBehaviour
{
    [SerializeField]
    public Main mainScript; // ���� �Ŵ������� �޾ƿ°�


    public void Hit()
    {
        if (mainScript.isPlayerTurn) 
        {
            mainScript.playerHand.Add(mainScript.DrawCard()); // ī�� �� �� �߰�
            int score = mainScript.CalculateScore(mainScript.playerHand); // ���� ���
            mainScript.Turn++; // �� ����

            Debug.Log("Player Hand: " + string.Join(", ", mainScript.playerHand) + " (Score: " + score + ")");
            //mainScript.DealerTurn();

            if (score > 21) // ������ 21�� �ʰ��ϸ� Bust ó��
            {
                mainScript.Bust();
            }

            mainScript.UpdateUI();
        }
    }

}

