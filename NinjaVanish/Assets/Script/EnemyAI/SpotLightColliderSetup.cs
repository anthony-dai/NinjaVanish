using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightColliderSetup : MonoBehaviour
{
    public Light spotLight;
    public SphereCollider spotlightCollider;
    public LightInfo lightInfo;
    // public LayerMask obstructionMask;

    // This script may also be used later for disabling light sources
    // Only works if straight down spotlight

    // Still need to implement for angled spotlights

    // Start is called before the first frame update
    void Start()
    {
        lightInfo = GameObject.Find("Dummy Player").GetComponent<LightInfo>();
        spotLight = GetComponent<Light>();
        spotlightCollider = GetComponent<SphereCollider>();
        float distance = transform.position.y;
        float innerAngle = (spotLight.innerSpotAngle / 360 ) * 2 * Mathf.PI;
        float radius = Mathf.Tan(innerAngle / 2f) * distance;
        spotlightCollider.isTrigger = true;
        spotlightCollider.center = new Vector3(0f, 0f, transform.position.y);
        spotlightCollider.radius = radius;
    }
}
