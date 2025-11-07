using UnityEngine;
using UnityEngine.SceneManagement; 

public class WaldoManager : MonoBehaviour
{
    void Update()
    {
        GameObject[] remainingWaldos = GameObject.FindGameObjectsWithTag("Waldo");

        if (remainingWaldos.Length == 0)
        {
            Debug.Log("Round Complete!");
            EndRound();
        }
    }

    void EndRound()
    {
        Debug.Log("All Waldos collected! Ending round...");
    }
}
