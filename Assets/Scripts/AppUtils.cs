using UnityEngine;

public class AppUtils : MonoBehaviour
{
	public void ToggleObjectVisible(GameObject obj)
	{
		obj.SetActive(!obj.activeSelf);
	}

	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}
}
