using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class PoisonSmoke : MonoBehaviour
{
    [Header("Tilemap Setup")]
    public Tilemap poisonTilemap;
    public TileBase[] poisonTiles = new TileBase[6]; // tiles for poison levels 0–5

    [Header("Poison Settings")]
    public int maxLevel = 5;
    public float spreadDelay = 0.2f;
    public int spreadRadius = 1;
    public int[] damagePerLevel = new int[6];
    // damage cooldown
    public float damaCooldown = 2f;
    
    // damage timer
    private float damaTimer = 0f;

    private Dictionary<Vector3Int, int> poisonLevels = new Dictionary<Vector3Int, int>();
    private HashSet<Vector3Int> spreadingTiles = new HashSet<Vector3Int>();

    private void Awake()
    {
        // gets ground tilemap ready for the picking
        if (poisonTilemap == null)
            poisonTilemap = GetComponent<Tilemap>();
    }

    // called by external trigger, like an enemy stepping on a tile
    public void TriggerPoisonSpread(Vector3 worldPos)
    {
        Vector3Int cell = poisonTilemap.WorldToCell(worldPos);
        int currentLevel = GetPoisonLevel(cell);

        // wont go above max level
        if (currentLevel >= maxLevel)
            return;

        int newLevel = Mathf.Clamp(currentLevel + 1, 1, maxLevel);
        SetPoisonLevel(cell, newLevel);
        Debug.Log($"Poison level set to {newLevel} at tile {cell}");
    }

    private IEnumerator SpreadFrom(Vector3Int origin)
    {
        int originLevel = GetPoisonLevel(origin);

        yield return new WaitForSeconds(spreadDelay);

        for (int dx = -spreadRadius; dx <= spreadRadius; dx++)
        {
            for (int dy = -spreadRadius; dy <= spreadRadius; dy++)
            {
                Vector3Int pos = new Vector3Int(origin.x + dx, origin.y + dy, origin.z);
                if (pos == origin) continue;

                // only spread to valid tiles (matching tag)
                if (CanSpreadTo(pos))
                {
                    int newLevel = Mathf.Clamp(originLevel - 1, 1, maxLevel);
                    SetPoisonLevel(pos, newLevel);
                }
            }
        }
    }

    private bool CanSpreadTo(Vector3Int pos)
    {
        // only spread to positions within the tilemap with no poison or lower level
        int currentLevel = GetPoisonLevel(pos);
        return currentLevel < 1;
    }

    public void SetPoisonLevel(Vector3Int pos, int level)
    {
        // sets the poison level and calls the function to update the tile
        poisonLevels[pos] = level;
        UpdateTileVisual(pos, level);
    }

    public int GetPoisonLevel(Vector3Int pos)
    {
        // returns the poison level for total sum function 
        return poisonLevels.TryGetValue(pos, out int level) ? level : 0;
    }

    public int GetTotalPoisonLevel(GameObject player)
    {
        int total = 0;

        foreach (int level in poisonLevels.Values)
        {
            total += level;
        }

        if (total >= 20)
        {
            damaTimer += Time.deltaTime;
            if (damaTimer >= damaCooldown)
            {
                player.GetComponent<Player>()?.TakeDamage(1);
                damaTimer = 0f;
            }
        }
        else
        {
            damaTimer = 0f;
        }
        return total;
    }

    private void UpdateTileVisual(Vector3Int pos, int level)
    {
        if (level <= 0)
        {
            poisonTilemap.SetTile(pos, null); // clear tile
        }
        else if (level >= 0 && level < poisonTiles.Length)
        {
            poisonTilemap.SetTile(pos, poisonTiles[level]); // checks tile level
        }
    }

    public void OnPlayerStepped(Vector3 worldPos, GameObject player, GameObject dam)
    {
        GetTotalPoisonLevel(player);
        Vector3Int cell = poisonTilemap.WorldToCell(worldPos);
        int level = GetPoisonLevel(cell);

        if (level > 0 && level < damagePerLevel.Length)
        {
            int damage = damagePerLevel[level];
            Debug.Log($"Player took {damage} poison damage at tile {cell}");
            damaTimer += Time.deltaTime;

            // creates a cooldown with damage times, prevents damage every 0.01 second basically instant death
            if (damaTimer >= damaCooldown)
            {
                dam.GetComponent<DamManager>()?.TakeDamage(damage);
                player.GetComponent<Player>()?.TakeDamage(damage);
                damaTimer = 0f;
            }                
        }

        // after checking we call these funstions in player script

        if (level == 1)
        {
            player.GetComponent<Player>()?.SlowMove1();
        }
        else if (level == 2)
        {
            player.GetComponent<Player>()?.SlowMove2();
        }
        else if (level == 3)
        {
            player.GetComponent<Player>()?.SlowMove3();
        }
        else if (level == 4)
        {
            player.GetComponent<Player>()?.SlowMove4();
        }
        else if (level == 5)
        {
            player.GetComponent<Player>()?.SlowMove5();
        }
        else if (level == 0)
        {
            player.GetComponent<Player>()?.SlowMove0();
        }
    }
}