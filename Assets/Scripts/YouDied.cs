using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDied : MonoBehaviour
{

    // makes it so we can use instance
    public static YouDied instance;

    void Awake()
    {
        instance = this;
        // instance this means this script
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
        // resets our scene as if were retrying 
    }
}
