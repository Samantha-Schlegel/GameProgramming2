using UnityEngine;
using System.Collections;

public class PowerupSpawner : MonoBehaviour
{
    [Header("Powerup Settings")]
    public GameObject powerupPrefab;        
    public Transform[] spawnPoints;         
    public float minSpawnTime = 5f;        
    public float maxSpawnTime = 15f;        
    public int maxPowerups = 3;             

    private int currentPowerups = 0;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentPowerups < maxPowerups)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject p = Instantiate(powerupPrefab, spawnPoint.position, Quaternion.identity);

                Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.gravityScale = 0;
                }

                currentPowerups++;

                InvisibilityPickup pickup = p.GetComponent<InvisibilityPickup>();
                if (pickup != null)
                {
                    pickup.onCollected += () => currentPowerups--;
                }
            }

            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
