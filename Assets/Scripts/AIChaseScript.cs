using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public float loseSightTime = 1f;
    public LayerMask obstacleMask;

    [Header("Trail Settings")]
    public float recordInterval = 0.1f;
    public int maxTrailLength = 20;

    private Rigidbody2D rb;
    private bool isPaused = false;
    private bool isChasing = false;
    private float loseSightTimer = 0f;

    private Queue<Vector2> playerTrail = new Queue<Vector2>();
    private float trailTimer = 0f;

    private Vector2 wanderTarget;
    private Coroutine wanderCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void OnEnable()
    {
        StartWandering();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        var playerPower = player.GetComponent<PlayerInvisibilityPowerup>();
        if (playerPower != null && playerPower.isInvisibleToAI)
        {
            isChasing = false;
            loseSightTimer = loseSightTime;
            StartWandering();
            return;
        }


        trailTimer += Time.fixedDeltaTime;
        if (trailTimer >= recordInterval)
        {
            playerTrail.Enqueue(player.position);
            if (playerTrail.Count > maxTrailLength)
                playerTrail.Dequeue();
            trailTimer = 0f;
        }

        float distanceToPlayer = Vector2.Distance(rb.position, player.position);
        bool canSeePlayer = distanceToPlayer <= detectionRadius &&
                            Physics2D.Raycast(rb.position, (player.position - (Vector3)rb.position).normalized,
                                distanceToPlayer, obstacleMask).collider == null;

        if (canSeePlayer)
        {
            if (!isChasing)
            {
                isChasing = true;
                StopWandering();
                isPaused = false;
            }

            loseSightTimer = 0f;
            playerTrail.Clear();
            MoveTowards(player.position, true);
            return;
        }

        if (isChasing)
        {
            loseSightTimer += Time.fixedDeltaTime;
            if (loseSightTimer >= loseSightTime)
            {
                isChasing = false;
                StartWandering();
                return;
            }

            FollowPlayerTrail();
            return;
        }

        MoveTowards(wanderTarget, true);
    }

    private void FollowPlayerTrail()
    {
        if (playerTrail.Count == 0) return;

        Vector2 targetPos = playerTrail.Peek();
        Vector2 moveDir = targetPos - rb.position;

        if (moveDir.magnitude < 0.05f)
        {
            playerTrail.Dequeue();
            return;
        }

        MoveTowards(targetPos, true);
    }

    private void MoveTowards(Vector2 target, bool allowSliding)
    {
        Vector2 moveDir = target - rb.position;
        if (moveDir.magnitude < 0.05f) return;

        moveDir.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(rb.position, moveDir, 0.5f, obstacleMask);

        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        }
        else if (allowSliding)
        {
            Vector2 tangent = Vector2.Perpendicular(moveDir).normalized;

            RaycastHit2D hit1 = Physics2D.Raycast(rb.position, tangent, 0.5f, obstacleMask);
            RaycastHit2D hit2 = Physics2D.Raycast(rb.position, -tangent, 0.5f, obstacleMask);

            Vector2 slideDir = hit1.collider == null ? tangent :
                               hit2.collider == null ? -tangent : Vector2.zero;

            if (slideDir != Vector2.zero)
                rb.MovePosition(rb.position + slideDir * speed * Time.fixedDeltaTime);
        }
    }

    private void StartWandering()
    {
        if (!isActiveAndEnabled) return;

        if (wanderCoroutine != null)
            StopCoroutine(wanderCoroutine);

        wanderCoroutine = StartCoroutine(WanderRoutine());
    }

    private void StopWandering()
    {
        if (wanderCoroutine != null)
        {
            StopCoroutine(wanderCoroutine);
            wanderCoroutine = null;
        }
        isPaused = false;
    }

    private IEnumerator WanderRoutine()
    {
        while (!isChasing)
        {
            isPaused = true;
            yield return new WaitForSeconds(pauseDuration);

            float x = Random.Range(startingPosition.x - wanderWidth / 2, startingPosition.x + wanderWidth / 2);
            float y = Random.Range(startingPosition.y - wanderHeight / 2, startingPosition.y + wanderHeight / 2);
            wanderTarget = new Vector2(x, y);

            isPaused = false;

            while (!isChasing && Vector2.Distance(rb.position, wanderTarget) > 0.1f)
            {
                yield return null;
            }
        }
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
