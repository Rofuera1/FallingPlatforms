using System.Collections.Generic;
using UnityEngine;

public class ColorChooser : MonoBehaviour
{
    [Zenject.Inject] private ColorsContainer ColorsContainer;
    private List<Material> ColorsPool;

    public ColorsOnCurrentLoop CurrentColors { get; private set; }

    private void Awake()
    {
        ColorsPool = new List<Material>();
        ColorsPool.AddRange(ColorsContainer.GetAllColor());
    }

    public void OnChooseColors(int ColorsToChooseAmount)
    {
        if (ColorsToChooseAmount > ColorsPool.Count) throw new System.Exception("Exceeded amount of free colors");

        List<Material> ColorsPoolLocal = new List<Material>();
        ColorsPoolLocal.AddRange(ColorsPool);

        CurrentColors = new ColorsOnCurrentLoop(ColorsToChooseAmount);
        for(int i = 0; i < ColorsToChooseAmount; i++)
        {
            int RandomID = Random.Range(0, ColorsPoolLocal.Count);
            CurrentColors.Colors[i] = ColorsPoolLocal[RandomID];
            ColorsPoolLocal.RemoveAt(RandomID);
        }
    }
}

public class ColorsOnCurrentLoop
{
    public Material[] Colors { get; private set; }

    public ColorsOnCurrentLoop(int Amount)
    {
        Colors = new Material[Amount];
    }
    public ColorsOnCurrentLoop(Material[] colors)
    {
        Colors = colors;
    }
}
