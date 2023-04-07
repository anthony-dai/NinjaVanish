using UnityEngine;

public class LerpColor : MonoBehaviour
{
    MeshRenderer NeonMeshRenderer;
    [SerializeField] [Range(0f, 1f)] float lerpTime;

    [ColorUsage(true, true)]
    [SerializeField] Color[] myColors;

    private int colorIndex = 0;
    private float t = 0f;
    private int len;

    // Start is called before the first frame update
    void Start()
    {
        NeonMeshRenderer = GetComponent <MeshRenderer>() ;
        len = myColors.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        NeonMeshRenderer.material.SetColor("_EmissionColor", Color.Lerp(NeonMeshRenderer.material.GetColor("_EmissionColor"), myColors[colorIndex], lerpTime*Time.deltaTime));
        DynamicGI.UpdateEnvironment();

        t = Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}
