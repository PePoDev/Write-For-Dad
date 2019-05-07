using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    /// <summary>
    /// Load scene by next index of current scene.
    /// </summary>
    public void LoadNextScene()
    {
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex + 1, Color.black, 1f);
    }

	public void Quit()
	{
		Application.Quit();
	}

    public void LoadMenuScene(float delay)
    {
        StartCoroutine(LoadSceneByDeleyTime(delay));
    }

    private IEnumerator LoadSceneByDeleyTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex - 3, Color.black, 1f);
    }

    public void RestartScene()
    {
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex, Color.black, 1f);
    }

    public void ExitWithDelay(float sec)
    {
        Initiate.Fade(SceneManager.GetActiveScene().buildIndex, Color.black, sec * 2f);
        StartCoroutine(ExitByDeleyTime());
        IEnumerator ExitByDeleyTime()
        {
            yield return new WaitForSeconds(sec);
            Application.Quit();
        }
    }
}
