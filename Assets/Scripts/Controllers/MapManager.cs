using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Zenject.Inject] private MapGenerator MapGenerator;
    [Zenject.Inject] private BlocksPainter Painter;
    [Zenject.Inject] private ColorsContainer ColorChooser;

    private GameObject[,] Boxes;

    public void CreateMap()
    {
        MapGenerator.CreateMap(out Boxes);
        Painter.PaintAtStart(Boxes, ColorChooser.GetColorWithPerCapitaParams());
    }
}
