using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public Action<Vector2> OnPressingMovement;
    public Action OnPressedSpace;

    private void Update()
    {
        Vector2 CurrentMovement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            CurrentMovement.y += 1;
        if (Input.GetKey(KeyCode.S))
            CurrentMovement.y -= 1;
        if (Input.GetKey(KeyCode.A))
            CurrentMovement.x -= 1;
        if (Input.GetKey(KeyCode.D))
            CurrentMovement.x += 1;

        if(CurrentMovement.magnitude != 0)
            OnPressingMovement?.Invoke(CurrentMovement);

        if (Input.GetKey(KeyCode.Space))
            OnPressedSpace?.Invoke();
    }
}
