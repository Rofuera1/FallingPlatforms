using System.Collections;
using UnityEngine;

public class GameflowManager : MonoBehaviour
{
    [Zenject.Inject] private GameflowVisualManager VisualManager;
    [Zenject.Inject] private MapManager MapGenerator;
    [Zenject.Inject] private ColorChooser ColorChooser;

    [Zenject.Inject] private Zenject.SignalBus Signaller;

    [SerializeField] private int MaxIterations; // Move to scriptables
    private int IterationID = 0;

    private void Awake()
    {
        SignalNewState(GameStates.NotStarted);

        LoadMap();
    }

    private void SignalNewState(GameStates newState)
    {
        Signaller.Fire(new GameflowEvent(newState));
    }

    private void LoadMap()
    {
        MapGenerator.CreateMap();

        StartGame();
    }

    private void StartGame()
    {
        SignalNewState(GameStates.Started);
        StartCoroutine(MainLoop());
    }

    private IEnumerator MainLoop()
    {
        if(IterationID >= MaxIterations)
        {
            OnEndGameWon();
            yield break;
        }

        IterationID++;

        yield return VisualManager.NewLoop();

        IterationChoosingColors();
    }
    
    private IEnumerator IterationChoosingColors()
    {
        ColorChooser.OnChooseColors(1);

        yield return VisualManager.ColorChooser(ColorChooser.CurrentColors);

        IterationWaitingForFalling();
    }

    private IEnumerator IterationWaitingForFalling()
    {
        yield return VisualManager.DelayBeforeFalling();

        IterationFalling();
    }

    private IEnumerator IterationFalling()
    {
        yield return VisualManager.Falling();

        IterationPauseAfterFalling();
    }

    private IEnumerator IterationPauseAfterFalling()
    {
        yield return VisualManager.DelayAfterFalling();

        MainLoop();
    }

    private void OnEndGameWon()
    {
        Signaller.Fire(new GameflowEvent(GameStates.Ended));

        VisualManager.OnEndGame(true);
    }

    private void OnEndGameLost()
    {
        Signaller.Fire(new GameflowEvent(GameStates.Ended));

        VisualManager.OnEndGame(false);
    }

    public class GameflowEvent
    {
        public GameStates NewState;

        public GameflowEvent(GameStates newState)
        {
            NewState = newState;
        }
    }

    public enum GameStates
    {
        NotStarted,
        Started,
        Ended
    }
}
