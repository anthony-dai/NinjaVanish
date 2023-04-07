using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private GameObject wall;
    private Vector3 offset;
    private bool wallhit;
    private List<GameObject> rayList = new List<GameObject>();
    private Color tempcolor;
    public LayerMask ignoreMask;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        ignoreMask = ~LayerMask.GetMask(new string[] {"SusColliderPlayer", "LightCollider"});
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }


    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        RaycastHit Hit;
            
            if(Physics.Raycast(transform.position, direction, out Hit, Mathf.Infinity, ignoreMask))
            {

                wallhit = Hit.transform.gameObject.CompareTag("Wall");
                if (wallhit)
                {
                    
                    rayList.Add(Hit.transform.gameObject);
                    
                    for(int i = 0; i<rayList.Count; i++)
                    {
                        GameObject wall = rayList[i];
                        tempcolor = wall.GetComponent<Renderer>().material.color;
                        tempcolor.a = .5f;
                        wall.GetComponent<Renderer>().material.color = tempcolor;

                    }
                    
                    
                }
             

                
                else
                {
                    for(int i = 0; i<rayList.Count; i++)
                    {
                        GameObject wall = rayList[i];
                        tempcolor = wall.GetComponent<Renderer>().material.color;
                        tempcolor.a = 1f;
                        wall.GetComponent<Renderer>().material.color = tempcolor;

                    }
                    rayList.Clear();
                }
                
            }

    }

}
    



