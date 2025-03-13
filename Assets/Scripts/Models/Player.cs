using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed; // Move to scriptables
    [SerializeField] private float Jump; // Move to scriptables
    [SerializeField] private float Dash; // Move to scriptables

    private Rigidbody RB;
    private bool CanDash = true;
    private Vector3 PrefferablePosition;
    private Vector3 RefPrefferable;

    private int CoinsCollected = 0;
    public Action<int> OnCoinCollected;
    public Action FallenDown;

    private const float VELOCITY_DAMP = 0.001f;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void OnMove(Vector3 Delta)
    {
        PrefferablePosition = RB.transform.position + Delta * Time.deltaTime * Speed;

        if (!IsMidAir())
            RB.MovePosition(Vector3.SmoothDamp(RB.position, PrefferablePosition, ref RefPrefferable, 0.2f));
        RB.transform.LookAt(PrefferablePosition);
    }

    public void OnTryJump()
    {
        if (!IsMidAir())
        {
            RB.AddForce((RB.transform.forward + Vector3.up) * Jump, ForceMode.Impulse);
            RefPrefferable = Vector3.zero;
        }
    }

    public void OnDash()
    {
        if (!CanDash) return;

        RB.AddForce(RB.transform.forward * Dash, ForceMode.Impulse);
        StartCoroutine(DashCooldown(2f));
    }

    private System.Collections.IEnumerator DashCooldown(float Time)
    {
        CanDash = false;
        yield return new WaitForSeconds(Time);
        CanDash = true;
    }

    private bool IsMidAir() => Mathf.Abs(RB.linearVelocity.y) > VELOCITY_DAMP;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Coin>())
        {
            CollectCoin(other.GetComponent<Coin>());
        }
        else if (other.tag == "Bottom")
        {
            FallenDown?.Invoke();
        }
    }

    private void CollectCoin(Coin Coin)
    {
        CoinsCollected++;
        OnCoinCollected?.Invoke(CoinsCollected);

        Destroy(Coin.gameObject);
    }
}
