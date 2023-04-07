using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Randomize_Walls : MonoBehaviour
{
    public string searchTag = "Pillar";
    private int height;

    void Start()
    {
        FindObjectwithTag(searchTag);
    }

    public void FindObjectwithTag(string _tag)
    {
        Transform parent = transform;
        GetChildObject(parent, _tag);
    }

    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                RandomizeHeight(child.gameObject);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }

    public void RandomizeHeight(GameObject pillar)
    {
        height = Random.Range(20, 24);
        pillar.transform.localScale = new Vector3(pillar.transform.localScale.x, ((float)(height * 2))/((float)20), pillar.transform.localScale.z);
        pillar.transform.SetPositionAndRotation(new Vector3(pillar.transform.position.x, ((float)(height))/((float)20), pillar.transform.position.z), Quaternion.identity);

    }
}
