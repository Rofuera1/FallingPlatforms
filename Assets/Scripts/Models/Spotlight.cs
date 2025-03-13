using System.Collections;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] private Transform SpotlightTR;
    [Zenject.Inject] private PlayerTransform Player;

    private Light Light;
    private IEnumerator CurrentSpotlighter;

    private void Awake()
    {
        Light = SpotlightTR.GetComponent<Light>();
    }

    public void OnStart()
    {
        if (CurrentSpotlighter != null)
            StopCoroutine(CurrentSpotlighter);

        Light.enabled = true;
        StartCoroutine(CurrentSpotlighter = FollowPlayer());
    }

    private IEnumerator FollowPlayer()
    {
        Vector3 RefVec = Vector3.zero;
        Vector3 LookAt = Player.Position;

        while(true)
        {
            LookAt = Vector3.SmoothDamp(LookAt, Player.Position, ref RefVec, 0.1f);

            SpotlightTR.LookAt(LookAt);

            yield return null;
        }
    }

    public void OnEnd()
    {
        if (CurrentSpotlighter != null)
            StopCoroutine(CurrentSpotlighter);

        Light.enabled = false;
    }
}
