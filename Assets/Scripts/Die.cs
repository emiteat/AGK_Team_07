using UnityEngine;

public class dieScript : MonoBehaviour
{
    public Main die;

    public void Die()
    {
        if (die.Turn < 1)
        {
            die.Bust();
        }
    }
}
