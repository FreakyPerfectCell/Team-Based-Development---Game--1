using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Purify : MonoBehaviour
{

    [Header("References Crap")]
    public PoisonSmoke poisonSmoke;
    public Tilemap poisonTilemap;
    public Animator animator;
    public MonoBehaviour Player;

    [Header("Settings Crap")]
    public float purifyInterval = 1f;
    public string purifyAnimBool = "isPurifying";

    public bool isPurifying = false;
    private Coroutine purifyRoutine;

    void Update()
    {
        // holding doesnt work but spamming button does
        if (Input.GetKey(KeyCode.Return))
        {
            StartPurify();

        }
        else if (Input.GetKeyUp(KeyCode.Return))
        {
            StopPurify();
        }
    }

    void StartPurify()
    {
        if (isPurifying || poisonSmoke == null || poisonTilemap == null) return;

        isPurifying = true;

        // disabling player script to stop movement
        if (Player != null) Player.enabled = false;

        // set the bool true to start the animation
        if (animator != null) animator.SetBool(purifyAnimBool, true);

        purifyRoutine = StartCoroutine(PurifyLoop());
    }

    void StopPurify()
    {
        if (!isPurifying) return;
        isPurifying = false;

        // re-enabling player script to go back to normal
        if (Player != null) Player.enabled = true;

        // set the bool false to stop the animation
        if (animator != null) animator.SetBool(purifyAnimBool, false);

        if (purifyRoutine != null) StopCoroutine(purifyRoutine);
    }

    IEnumerator PurifyLoop()
    {
        while (true)
        {
            PurifyTiles();
            yield return new WaitForSeconds(purifyInterval);
        }
    }

    void PurifyTiles()
    {
        Vector3Int playerCell = poisonTilemap.WorldToCell(transform.position);

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                Vector3Int cell = new Vector3Int(playerCell.x + dx, playerCell.y + dy, playerCell.z);
                int currentLevel = poisonSmoke.GetPoisonLevel(cell);

                if (currentLevel > 0)
                {
                    int newLevel = Mathf.Max(0, currentLevel - 1);
                    poisonSmoke.SetPoisonLevel(cell, newLevel);
                    GameManager.Instance.AddScore(50);
                }
            }
        }       
    }
}
