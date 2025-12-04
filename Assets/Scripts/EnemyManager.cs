using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemyManager : MonoBehaviour
{
    void Update()
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (remainingEnemies.Length == 0)
        {
            Debug.Log("Round Complete!");
            EndRound();
        }
    }

    void EndRound()
    {
        Debug.Log("Oh no!");
    }
}
