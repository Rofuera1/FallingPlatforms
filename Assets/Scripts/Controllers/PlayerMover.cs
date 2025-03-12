using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Zenject.Inject] private InputController Input;
    [Zenject.Inject] private Player Player;

    private void Awake()
    {
        Input.OnPressingMovement += OnMovePlayer;
        Input.OnPressedSpace += OnJumpPlayer;
    }

    private void OnMovePlayer(Vector2 Delta)
    {
        Player.OnMove(new Vector3(Delta.x, 0f, Delta.y));
    }

    private void OnJumpPlayer()
    {
        Player.OnTryJump();
    }
}
