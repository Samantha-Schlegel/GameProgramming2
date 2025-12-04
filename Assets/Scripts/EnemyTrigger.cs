using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            EnemyScoreManager.instance.AddEnemyPoint();
        }
    }
}
