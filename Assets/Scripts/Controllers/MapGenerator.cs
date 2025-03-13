using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int MapSize = 5; // Move to scriptables
    [SerializeField] private int CoinAmount = 5;
    [SerializeField] private Box BoxPrefab;
    [SerializeField] private Coin CoinPrefab;
    [SerializeField] private Transform Parent;

    public void CreateMap(out Box[,] Boxes)
    {
        Boxes = new Box[MapSize,MapSize];

        float HalfLength = MapSize * 0.5f;
        Vector3 StartTopRightPoint = - Vector3.right * HalfLength + Vector3.forward * HalfLength;

        List<Vector2Int> AvailiblePlace = new List<Vector2Int>();

        for (int x = 0; x < MapSize; x++)
        {
            for(int y = 0; y < MapSize; y ++)
            {
                Vector3 Position = StartTopRightPoint + Vector3.right * x - Vector3.forward * y;

                Boxes[x, y] = Instantiate(BoxPrefab, Position, Quaternion.identity);
                Boxes[x, y].transform.parent = Parent;

                AvailiblePlace.Add(new Vector2Int(x, y));
            }
        }

        for(int i = 0; i < CoinAmount; i++)
        {
            Vector2Int Place = AvailiblePlace[Random.Range(0, AvailiblePlace.Count)];
            Vector3 Position = StartTopRightPoint + Vector3.right * Place.x - Vector3.forward * Place.y + Vector3.up * 1.5f;

            Coin Coin = Instantiate(CoinPrefab, Position, Quaternion.identity);

            AvailiblePlace.Remove(Place);
        }
    }
}
