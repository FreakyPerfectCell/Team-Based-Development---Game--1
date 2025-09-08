using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour
{

    public static YouDied instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
