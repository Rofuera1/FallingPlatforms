using UnityEngine;

public class ColorsContainer : MonoBehaviour
{
    [SerializeField] private ColorProperties[] ColorsToChooseFrom; // Better to move to Scriptables

    public ColorProperties[] GetAllColorProperties()
    {
        return ColorsToChooseFrom;
    }

    public Material[] GetAllColor()
    {
        Material[] Colors = new Material[ColorsToChooseFrom.Length];
        for (int i = 0; i < ColorsToChooseFrom.Length; i++)
            Colors[i] = ColorsToChooseFrom[i].Material;

        return Colors;
    }

    public Material[] GetColorWithPerCapitaParams()
    {
        System.Collections.Generic.List<Material> Colors = new System.Collections.Generic.List<Material>();
        foreach(ColorProperties col in ColorsToChooseFrom)
        {
            for (int i = 0; i < col.PerCapita; i++)
                Colors.Add(col.Material);
        }

        return Colors.ToArray();
    }
}

[System.Serializable]
public class ColorProperties
{
    public Material Material;
    public int PerCapita;
}
