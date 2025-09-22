using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    public Transform target;             // Assign the player in Inspector
    public float tileSize = 1f;          // Size of one tile
    public float moveSpeed = 5f;         // Speed of movement
    public float moveDelay = 0.2f;       // Time between tile moves
    public LayerMask obstacleLayer;      // What counts as a wall/obstacle

    private bool isMoving = false;

    void Update()
    {
        if (isMoving || target == null) return;

        Vector2 delta = target.position - transform.position;
        Vector2Int primaryDir = Vector2Int.zero;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            primaryDir = new Vector2Int((int)Mathf.Sign(delta.x), 0);
        }
        else if (Mathf.Abs(delta.y) > 0)
        {
            primaryDir = new Vector2Int(0, (int)Mathf.Sign(delta.y));
        }

        if (primaryDir != Vector2Int.zero && CanMove(primaryDir))
        {
            StartCoroutine(MoveTo(transform.position + (Vector3)((Vector2)primaryDir * tileSize)));
            return;
        }

        Vector2Int secondaryDir = (primaryDir.x != 0)
            ? new Vector2Int(0, (int)Mathf.Sign(delta.y))
            : new Vector2Int((int)Mathf.Sign(delta.x), 0);

        if (secondaryDir != Vector2Int.zero && CanMove(secondaryDir))
        {
            StartCoroutine(MoveTo(transform.position + (Vector3)((Vector2)secondaryDir * tileSize)));
            return;
        }

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
        yield return new WaitForSeconds(moveDelay);
        isMoving = false;
    }
}