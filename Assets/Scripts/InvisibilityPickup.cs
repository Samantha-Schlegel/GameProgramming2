using UnityEngine;
using System;

public class InvisibilityPickup : MonoBehaviour
{
    public event Action onCollected; // Event to notify spawner

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInvisibilityPowerup power = other.GetComponent<PlayerInvisibilityPowerup>();
            if (power != null)
            {
                power.ActivatePowerup();
            }

            // Notify spawner
            onCollected?.Invoke();

            Destroy(gameObject);
        }
    }
}
