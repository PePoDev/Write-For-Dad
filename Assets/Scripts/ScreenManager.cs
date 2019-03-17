using UnityEngine.SceneManagement;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

	/// <summary>
	/// Load scene by next index of current scene.
	/// </summary>
	public void LoadNextScene()
	{
		Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, Color.black, 1f);
	}

	public void RestartScene()
	{
		Initiate.Fade(SceneManager.GetActiveScene().buildIndex, Color.black, 1f);
	}
}
