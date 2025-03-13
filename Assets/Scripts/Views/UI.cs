using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI CurrentCoinsCollected;
    [SerializeField] private GameObject StartObject;
    [SerializeField] private GameObject WinObject;
    [SerializeField] private GameObject LostObject;
    [SerializeField] private GameObject HowToPlayObject;

    public void StartGame()
    {
        StartObject.SetActive(false);
    }

    public void EndGameWin()
    {
        WinObject.SetActive(true);
    }

    public void EndGameLost()
    {
        LostObject.SetActive(true);
    }

    public void HowToPlaySetActive(bool active)
    {
        HowToPlayObject.SetActive(active);
    }

    public void UpdateText(int CollectedCoins, int OutOf)
    {
        CurrentCoinsCollected.text = "Монеток собрано: " + CollectedCoins + "/" + OutOf;
    }
}
