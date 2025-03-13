using System.Collections;
using UnityEngine;

public class GameflowVisualManager : MonoBehaviour
{
    [Zenject.Inject] private LightsOperator LightsOperator;
    [Zenject.Inject] private MapManager MapManager;

    private float DelayTimeAfterColorChose = 1.5f;
    private float DelayTimeBeforeFalling = 3f;
    private float LightsOutBeforeFallingTime = 0.6f;

    public IEnumerator NewLoop()
    {
        Debug.Log("Ќью луп - сто залуп");
        yield return null;
    }
    public IEnumerator ColorChooser(ColorsOnCurrentLoop Colors)
    {
        LightsOperator.OnChangeColorLightbulb(Colors.Colors[0]);
        yield return new WaitForSeconds(DelayTimeAfterColorChose);
    }

    public IEnumerator DelayBeforeFalling()
    {
        yield return LightsOutBeforeFalling();

        yield return new WaitForSeconds(DelayTimeBeforeFalling);
    }

    private IEnumerator LightsOutBeforeFalling()
    {
        LightsOperator.SetMainLight(false);

        yield return new WaitForSeconds(LightsOutBeforeFallingTime);

        LightsOperator.ActivateSpotlight(true);
    }

    public IEnumerator Falling(ColorsOnCurrentLoop Current)
    {
        yield return MapManager.FallObjects(Current, 0.1f);
    }
    public IEnumerator DelayAfterFalling()
    {
        LightsOperator.ActivateSpotlight(false);
        LightsOperator.SetMainLight(true);

        yield return new WaitForSeconds(2f);    
    }

    public void OnEndGame(bool didWin)
    {
        Debug.Log("Wow! You won? " + didWin);
    }
}
