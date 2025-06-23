using UnityEngine;

public class stayScript : MonoBehaviour
{
    [SerializeField]
    public Main Stayscript;


    public void Stay()
    {
        if (!Stayscript.isPlayerTurn) return;

        Debug.Log("Player Stays. Dealer's Turn.");
        Stayscript.isPlayerTurn = false; // 플레이어 턴 종료
        Stayscript.DealerTurn();
        Stayscript.Turn++;
    }

}
