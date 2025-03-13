using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Zenject.Inject] private InputController Input;
    [Zenject.Inject] private Player Player;
    [Zenject.Inject] private Zenject.SignalBus Signaller;

    private void Awake()
    {
        Signaller.Subscribe<GameflowManager.GameflowEvent>(SubscribeToEvents);
    }

    private void SubscribeToEvents(GameflowManager.GameflowEvent Event)
    {
        if(Event.NewState == GameflowManager.GameStates.Started)
        {
            Input.OnPressingMovement += OnMovePlayer;
            Input.OnPressedSpace += OnJumpPlayer;
            Input.OnPressedShift += OnDash;
        }
    }

    private void OnMovePlayer(Vector2 Delta)
    {
        Player.OnMove(new Vector3(Delta.x, 0f, Delta.y));
    }

    private void OnJumpPlayer()
    {
        Player.OnTryJump();
    }

    private void OnDash()
    {
        Player.OnDash();
    }
}
