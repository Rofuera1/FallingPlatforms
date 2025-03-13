using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Material MaterialOn { get; private set; }

    public void SetMaterial(Material Mat)
    {
        MaterialOn = Mat;
    }

    public void Fall(float Delay = 0f)
    {
        StartCoroutine(FallAfter(Delay));
    }

    private IEnumerator FallAfter(float Time)
    {
        yield return new WaitForSeconds(Time);

        gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject, 5f);
    }
}
