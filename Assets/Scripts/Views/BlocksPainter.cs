using UnityEngine;

public class BlocksPainter : MonoBehaviour
{
    public void PaintAtStart(GameObject[,] Boxes, Material[] ColorsPerCapita)
    {
        foreach(GameObject box in Boxes)
        {
            box.GetComponent<MeshRenderer>().material = ColorsPerCapita[Random.Range(0, ColorsPerCapita.Length)];
        }
    }
}
