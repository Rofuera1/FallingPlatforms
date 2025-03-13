using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    [Zenject.Inject] private GameflowManager Manager;
    [Zenject.Inject] private UI UI;

    public void ShowHowToPlay()
    {
        UI.HowToPlaySetActive(true);
    }

    public void HideHowToPlay()
    {
        UI.HowToPlaySetActive(false);
    }

    public void StartGame()
    {
        Manager.StartGame();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
