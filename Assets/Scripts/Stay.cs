using UnityEngine;

public class stayScript : MonoBehaviour
{
    [SerializeField]
    public Main Stayscript;


    public void Stay()
    {
        if (!Stayscript.isPlayerTurn) return;

        Debug.Log("Player Stays. Dealer's Turn.");
        Stayscript.isPlayerTurn = false; // �÷��̾� �� ����
        Stayscript.DealerTurn();
        Stayscript.Turn++;
    }

}
