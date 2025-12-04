using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            var power = other.GetComponent<PlayerInvisibilityPowerup>();
            if (power != null && power.isInvincible)
            {
                return;
            }

            EnemyScoreManager.instance.AddEnemyPoint();
        }
    }
}
