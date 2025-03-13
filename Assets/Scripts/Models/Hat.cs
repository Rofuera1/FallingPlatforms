using UnityEngine;

public class Hat : MonoBehaviour
{
    [SerializeField] private GameObject[] Hats;

    private void Awake()
    {
        Hats[Random.Range(0, Hats.Length)].SetActive(true);
    }
}
