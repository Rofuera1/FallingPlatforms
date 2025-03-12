using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed; // Move to scriptables
    [SerializeField] private float Jump; // Move to scriptables

    private Rigidbody RB;
    private Vector3 PrefferablePosition;
    private Vector3 RefPrefferable;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void OnMove(Vector3 Delta)
    {
        PrefferablePosition = RB.transform.position + Delta * Time.deltaTime * Speed;

        RB.MovePosition(Vector3.SmoothDamp(RB.position, PrefferablePosition, ref RefPrefferable, 0.2f));
        RB.transform.LookAt(PrefferablePosition);
    }

    public void OnTryJump()
    {
        if (Mathf.Abs(RB.linearVelocity.y) < 0.001f)
            RB.AddForce((RB.transform.forward + Vector3.up) * Jump);
    }
}
