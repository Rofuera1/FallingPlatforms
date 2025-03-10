using System.Collections;
using UnityEngine;

public class GameflowVisualManager : MonoBehaviour
{
    public IEnumerator NewLoop()
    {
        Debug.Log("Нью луп - сто залуп");
        yield return null;
    }
    public IEnumerator ColorChooser(ColorsOnCurrentLoop Colors)
    {
        Debug.Log("Текущий цвет - " + Colors.Colors[0]);
        yield return null;
    }

    public IEnumerator DelayBeforeFalling()
    {
        Debug.Log("АААА ЩА УПАДЕТ ВСЕ ЧЕРЕЗ 5 СЕКУНД");
        yield return new WaitForSeconds(5f); // Move to scriptables
    }
    public IEnumerator Falling()
    {
        Debug.Log("Ну все, оно упало(");
        yield return null;
    }
    public IEnumerator DelayAfterFalling()
    {
        Debug.Log("Можешь передохнуть 5 сек");
        yield return new WaitForSeconds(2f); // Move to scriptables
    }

    public void OnEndGame(bool didWin)
    {
        Debug.Log("Wow! You won? " + didWin);
    }
}
