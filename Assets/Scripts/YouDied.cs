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
        if (Input.GetKeyDown(KeyCode.R)) // placeholder R (this is reused code, you can change it to whatever you want just make sure you update the UI if you do so)
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
