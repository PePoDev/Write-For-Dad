using UnityEngine;

public class Level2_Manager : MonoBehaviour
{
    #region Variables
    public ObjectsReference objectsReference;

    private bool showedTutorial = false;
    #endregion

    #region Main Methods
    private void Start()
    {

    }
    #endregion

    #region Helper Method
    public void ShowTutorial()
    {
        if (showedTutorial == false)
        {
            objectsReference.objectsInScene.TryGetValue("DialogOverlay", out GameObject dialog);
            dialog.SetActive(true);
            showedTutorial = true;
        }
        else
        {
            objectsReference.objectsInScene.TryGetValue("TutorialButton", out GameObject tutorialButton);
            tutorialButton.SetActive(false);
            objectsReference.objectsInScene.TryGetValue("DialogOverlay", out GameObject dialog);
            dialog.SetActive(false);
        }
    }
    public void GameStart()
    {

    }
    public void GameReset()
    {

    }
    #endregion
}