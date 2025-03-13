using UnityEngine;

public class BlocksPainter : MonoBehaviour
{
    public void PaintAtStart(Box[,] Boxes, Material[] ColorsPerCapita)
    {
        foreach(Box box in Boxes)
        {
            Material Mat = ColorsPerCapita[Random.Range(0, ColorsPerCapita.Length)];

            box.GetComponent<MeshRenderer>().material = Mat;
            box.SetMaterial(Mat);
        }
    }
}
