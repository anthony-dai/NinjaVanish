using UnityEngine;

public class IntensityController : MonoBehaviour
{
    private string Light_Intensity_Pref = "Light_Intensity";

    private void Awake()
    {
        SetLightIntensity();
    }


    public void SetLightIntensity()
    {
        float sav_LightIntensity = PlayerPrefs.GetFloat(Light_Intensity_Pref, 1);
        GetComponent<Light>().intensity = sav_LightIntensity;
    }
}
