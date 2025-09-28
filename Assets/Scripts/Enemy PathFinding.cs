using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    public Transform pointA;               // First patrol point
    public Transform pointB;               // Second patrol point
    public float tileSize = 1f;            // Size of one tile
    public float moveSpeed = 5f;           // Speed of movement
    public float moveDelay = 0.2f;         // Time between tile moves
    public LayerMask obstacleLayer;        // Obstacles to avoid

    private bool isMoving = false;
    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB; // Start by moving toward pointB
    }

    void Update()
    {
        if (isMoving || currentTarget == null) return;

        Vector2 delta = currentTarget.position - transform.position;
        Vector2Int primaryDir = Vector2Int.zero;

        // Determine primary direction (horizontal or vertical)
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            primaryDir = new Vector2Int((int)Mathf.Sign(delta.x), 0);
        }
        else if (Mathf.Abs(delta.y) > 0)
        {
            primaryDir = new Vector2Int(0, (int)Mathf.Sign(delta.y));
        }

        // Try moving in primary direction
        if (primaryDir != Vector2Int.zero && CanMove(primaryDir))
        {
            StartCoroutine(MoveTo(transform.position + (Vector3)((Vector2)primaryDir * tileSize)));
            return;
        }

        // Try moving in secondary direction
        Vector2Int secondaryDir = (primaryDir.x != 0)
            ? new Vector2Int(0, (int)Mathf.Sign(delta.y))
            : new Vector2Int((int)Mathf.Sign(delta.x), 0);

        if (secondaryDir != Vector2Int.zero && CanMove(secondaryDir))
        {
            StartCoroutine(MoveTo(transform.position + (Vector3)((Vector2)secondaryDir * tileSize)));
            return;
        }

        // Fallback: move in a random valid direction
        Vector2Int randomDir = GetRandomValidDirection();
        if (randomDir != Vector2Int.zero)
        {
            StartCoroutine(MoveTo(transform.position + (Vector3)((Vector2)randomDir * tileSize)));
        }
    }

    bool CanMove(Vector2 dir)
    {
        Vector2 checkPos = (Vector2)transform.position + dir * tileSize;
        return !Physics2D.OverlapCircle(checkPos, 0.1f, obstacleLayer);
    }

    Vector2Int GetRandomValidDirection()
    {
        Vector2Int[] directions = {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        List<Vector2Int> validDirs = new List<Vector2Int>();
        foreach (Vector2Int dir in directions)
        {
            if (CanMove(dir))
                validDirs.Add(dir);
        }

        if (validDirs.Count == 0)
            return Vector2Int.zero;

        return validDirs[Random.Range(0, validDirs.Count)];
    }

    IEnumerator MoveTo(Vector3 destination)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;
        float duration = 1f / moveSpeed;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, destination, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;

        // Switch to the other point if we've arrived at the current one
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }

        yield return new WaitForSeconds(moveDelay);
        isMoving = false;
    }
}