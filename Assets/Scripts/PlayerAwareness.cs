using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{

    // AwareOfPlayer indicates whether enemy is aware or not public so other scripts can access
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    [SerializeField] private float _playerAwarenessDistance;
    private Transform _player;

    private void Awake()
    {
        // searches for gamebjects with scripts of <name>
        _player = Player.instance.transform;
    }

    void Update()
    {
        // vector2 tells how far the player object is
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <- _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
