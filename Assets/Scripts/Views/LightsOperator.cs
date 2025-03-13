using UnityEngine;

public class LightsOperator : MonoBehaviour
{
    [SerializeField] private Light Lightbulb;
    [SerializeField] private MeshRenderer LightbulbRenderer;
    [SerializeField] private GameObject LightbulbParent;
    [SerializeField] private Light MainLight;
    [SerializeField] private int MapSize; // SCRIPTABLES!!!

    private Material LightbulbMaterial;

    [Zenject.Inject] private Spotlight Spotlight;

    private void Awake()
    {
        SetLightbulb();
    }

    private void SetLightbulb()
    {
        LightbulbParent.transform.position = Vector3.forward * MapSize * 0.5f;
        LightbulbMaterial = LightbulbRenderer.material;
    }

    public void OnChangeColorLightbulb(Material NewMaterial)
    {
        Lightbulb.color = NewMaterial.color;
        Color newCol = NewMaterial.color;
        newCol.a = 0.9f;
        LightbulbMaterial.color = newCol;
    }

    public void SetMainLight(bool Active)
    {
        MainLight.enabled = Active;
    }

    public void ActivateSpotlight(bool Activate)
    {
        if (Activate)
            Spotlight.OnStart();
        else
            Spotlight.OnEnd();
    }
}
