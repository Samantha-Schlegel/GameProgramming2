using UnityEngine;
using System.Collections;

public class AIChaseScript : MonoBehaviour
{
    [Header("Wandering Settings")]
    public float wanderWidth = 8f;
    public float wanderHeight = 8f;
    public Vector2 startingPosition;
    public float speed = 2f;
    public float pauseDuration = 1f;

    [Header("Chase Settings")]
    public Transform player;
    public float detectionRadius = 5f;
    public float loseSightTime = 2f;
    public LayerMask obstacleMask; // Assign your walls/obstacles layer here

    private Vector2 target;
    private Rigidbody2D rb;
    private bool isPaused = false;
    private bool isChasing = false;
    private float loseSightTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        target = GetRandomTarget();
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        Vector2 moveDir;

        // Check if player is in detection radius AND visible
        if (player != null && Vector2.Distance(rb.position, player.position) <= detectionRadius)
        {
            Vector2 dirToPlayer = ((Vector2)player.position - rb.position).normalized;
            float distance = Vector2.Distance(rb.position, player.position);

            RaycastHit2D hit = Physics2D.Raycast(rb.position, dirToPlayer, distance, obstacleMask);
            if (hit.collider == null) // No obstacle in the way
            {
                isChasing = true;
                loseSightTimer = 0f;
            }
        }

        if (isChasing)
        {
            // Check if player has escaped detection radius or is blocked
            if (player != null)
            {
                Vector2 dirToPlayer = ((Vector2)player.position - rb.position).normalized;
                float distance = Vector2.Distance(rb.position, player.position);
                RaycastHit2D hit = Physics2D.Raycast(rb.position, dirToPlayer, distance, obstacleMask);

                if (distance > detectionRadius || hit.collider != null)
                {
                    loseSightTimer += Time.fixedDeltaTime;
                    if (loseSightTimer >= loseSightTime)
                    {
                        isChasing = false;
                        target = GetRandomTarget();
                    }
                }
            }

            // Chase the player
            if (player != null && isChasing)
            {
                moveDir = ((Vector2)player.position - rb.position).normalized;
                rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
                return;
            }
        }

        // Wandering behavior
        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            StartCoroutine(PauseAndPickNewDestination());
            return;
        }

        moveDir = (target - rb.position).normalized;
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    IEnumerator PauseAndPickNewDestination()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        target = GetRandomTarget();
        isPaused = false;
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2f;
        float halfHeight = wanderHeight / 2f;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), // Left
            1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), // Right
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight), // Bottom
            3 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y + halfHeight), // Top
            _ => startingPosition
        };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));

        if (player != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
