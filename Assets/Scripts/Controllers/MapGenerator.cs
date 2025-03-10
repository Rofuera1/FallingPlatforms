using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int MapSize = 5; // Move to scriptables
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private Transform CenterPosition;

    public void CreateMap(out GameObject[,] Boxes)
    {
        Boxes = new GameObject[MapSize,MapSize];

        float HalfLength = MapSize * 0.5f;
        Vector3 StartTopRightPoint = CenterPosition.position - Vector3.right * HalfLength + Vector3.forward * HalfLength;

        for(int x = 0; x < MapSize; x++)
        {
            for(int y = 0; y < MapSize; y ++)
            {
                Boxes[x, y] = Instantiate(BoxPrefab, StartTopRightPoint + Vector3.right * x + Vector3.down * y, Quaternion.identity);
            }
        }
    }
}
