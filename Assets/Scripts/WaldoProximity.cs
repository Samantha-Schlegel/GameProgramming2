using UnityEngine;
using UnityEngine.SceneManagement;

public class WaldoProximity : MonoBehaviour
{
    public float detectionRadius = 2f;  // How close the player needs to be
    public string playerTag = "Player";

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= detectionRadius)
            {
                // Add 1 point to score
                ScoreManager.instance.AddPoint();

                // Destroy Waldo
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
