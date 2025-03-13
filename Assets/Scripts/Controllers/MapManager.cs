using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Zenject.Inject] private MapGenerator MapGenerator;
    [Zenject.Inject] private BlocksPainter Painter;
    [Zenject.Inject] private ColorsContainer ColorChooser;

    private Box[,] Boxes; // Я мог сделать сортировку заранее, но если сохранять все в массиве (а он не очень большой) - можно делать прикольные эффекты без особых проблем (падение с одной стороны к другой, из центра наружу и тд)
    private int MapSize;

    public void CreateMap()
    {
        MapGenerator.CreateMap(out Boxes);
        MapSize = Boxes.GetLength(0);
        Painter.PaintAtStart(Boxes, ColorChooser.GetColorWithPerCapitaParams());
    }

    public IEnumerator FallObjects(ColorsOnCurrentLoop Colors, float DelayBetween)
    {
        yield return PrettyFalling(Colors.Colors[0], DelayBetween);
    }

    private IEnumerator PrettyFalling(Material Material, float DelayBetween)
    {
        FallingType Type = (FallingType)(Random.Range(0, 4));

        for (int i = 0; i < MapSize; i++)
        {
            Box[] BoxesToFall = GetBoxesOfType(Material, Type, i);

            int id = 0;
            foreach(Box box in BoxesToFall)
            {
                box.Fall(id++ * 0.07f);
            }

            yield return new WaitForSeconds(DelayBetween);
        }
    }

    private Box[] GetBoxesOfType(Material Material, FallingType Type, int I)
    {
        switch (Type)
        {
            case FallingType.LeftToRight:
                return GetBoxesLeftToRight(Material, I);
            case FallingType.RightToLeft:
                return GetBoxesRightToLeft(Material, I);
            case FallingType.BottomToTop:
                return GetBoxesBottomToTop(Material, I);
            case FallingType.TopToBottom:
                return GetBoxesTopToBottom(Material, I);
        }

        return null;
    }

    private Box[] GetBoxesLeftToRight(Material Material, int I)
    {
        List<Box> Result = new List<Box>();

        for(int y = 0; y < MapSize; y++)
        {
            if (Boxes[I, y].MaterialOn == Material)
                Result.Add(Boxes[I, y]);
        }

        return Result.ToArray();
    }

    private Box[] GetBoxesRightToLeft(Material Material, int I)
    {
        List<Box> Result = new List<Box>();

        for (int y = 0; y < MapSize; y++)
        {
            int i = MapSize - I - 1;
            if (Boxes[i, y].MaterialOn == Material)
                Result.Add(Boxes[i, y]);
        }

        return Result.ToArray();
    }

    private Box[] GetBoxesTopToBottom(Material Material, int I)
    {
        List<Box> Result = new List<Box>();

        for (int y = 0; y < MapSize; y++)
        {
            int i = MapSize - I - 1;
            if (Boxes[y, i].MaterialOn == Material)
                Result.Add(Boxes[y, i]);
        }

        return Result.ToArray();
    }

    private Box[] GetBoxesBottomToTop(Material Material, int I)
    {
        List<Box> Result = new List<Box>();

        for (int y = 0; y < MapSize; y++)
        {
            int i = I;
            if (Boxes[y, i].MaterialOn == Material)
                Result.Add(Boxes[y, i]);
        }

        return Result.ToArray();
    }

    public enum FallingType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
}
