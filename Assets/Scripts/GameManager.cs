using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer Crap")]
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;

    [Header("Score Crap")]
    public static GameManager Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    public int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}

// simple timer code, counts up, formats as minutes:seconds / 00:00
// score code instance so it can be accessed by other scripts
