using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadSceneAsync("Level 1");
	}
}

// click play we change scene
