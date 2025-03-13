using System.Collections;
using UnityEngine;

public class GameflowManager : MonoBehaviour
{
    [Zenject.Inject] private GameflowVisualManager VisualManager;
    [Zenject.Inject] private MapManager MapGenerator;
    [Zenject.Inject] private ColorChooser ColorChooser;
    [Zenject.Inject] private Player Player;
    [Zenject.Inject] private UI UI;

    [Zenject.Inject] private Zenject.SignalBus Signaller;

    [SerializeField] private int MaxIterations; // Move to scriptables
    [SerializeField] private int WinCoinsAmount = 5; // Move to scriptables

    private GameStates CurrentState;

    private int IterationID = 0;

    private void Awake()
    {
        SignalNewState(GameStates.NotStarted);

        LoadMap();

        Player.OnCoinCollected += OnCoinCollectedPlayer;
        Player.FallenDown += OnEndGameLost;
    }

    private void SignalNewState(GameStates newState)
    {
        CurrentState = newState;
        Signaller.Fire(new GameflowEvent(CurrentState));
    }

    private void LoadMap()
    {
        MapGenerator.CreateMap();
    }

    public void StartGame()
    {
        UI.StartGame();

        SignalNewState(GameStates.Started);
        StartCoroutine(MainLoop());
        Time.timeScale = 1f;
    }

    private IEnumerator MainLoop()
    {
        if (CurrentState != GameStates.Started)
            yield break;
        if (IterationID >= MaxIterations)
        {
            OnEndGameLost();
            yield break;
        }

        IterationID++;

        yield return VisualManager.NewLoop();

        StartCoroutine(IterationChoosingColors());
    }
    
    private IEnumerator IterationChoosingColors()
    {
        ColorChooser.OnChooseColors(1);

        yield return VisualManager.ColorChooser(ColorChooser.CurrentColors);

        StartCoroutine(IterationWaitingForFalling());
    }

    private IEnumerator IterationWaitingForFalling()
    {
        yield return VisualManager.DelayBeforeFalling();

        StartCoroutine(IterationFalling());
    }

    private IEnumerator IterationFalling()
    {
        yield return VisualManager.Falling(ColorChooser.CurrentColors);

        StartCoroutine(IterationPauseAfterFalling());
    }

    private IEnumerator IterationPauseAfterFalling()
    {
        yield return VisualManager.DelayAfterFalling();

        StartCoroutine(MainLoop());
    }

    private void OnCoinCollectedPlayer(int Amount)
    {
        UI.UpdateText(Amount, WinCoinsAmount);
        if (Amount >= WinCoinsAmount)
            OnEndGameWon();
    }

    private void OnEndGameWon()
    {
        if (CurrentState != GameStates.Started) return;

        Signaller.Fire(new GameflowEvent(GameStates.Won));

        VisualManager.OnEndGame(true);
        UI.EndGameWin();
        Time.timeScale = 0f;
    }

    private void OnEndGameLost()
    {
        if (CurrentState != GameStates.Started) return;

        Signaller.Fire(new GameflowEvent(GameStates.Lost));

        VisualManager.OnEndGame(false);
        UI.EndGameLost();
        Time.timeScale = 0f;
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
        Won,
        Lost
    }
}
