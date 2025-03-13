using UnityEngine;

public class CameraLerper : MonoBehaviour
{
    [SerializeField] private Transform Camera;

    [Zenject.Inject] private PlayerTransform PlayerTR;

    private Vector3 Delta;
    private Vector3 RefVec = Vector3.zero;

    private void Awake()
    {
        Delta = Camera.position - PlayerTR.Position;
    }

    private void FixedUpdate()
    {
        Camera.position = Vector3.SmoothDamp(Camera.position, PlayerTR.Position + Delta, ref RefVec, 0.2f);
    }
}
