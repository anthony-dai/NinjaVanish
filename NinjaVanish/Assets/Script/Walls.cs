using UnityEngine;
public class Walls : MonoBehaviour
{
    public GameObject block;
    public GameObject plane;
    public int levelLength = 2;

    private int height;
    private GameObject block_copy;
    private int[] numbers = { -1, 1 };

    void Start()
    {
        
        for (int z = 5; z < levelLength * 5; ++z)
        {
            foreach (var i in numbers)
            {
                height = Random.Range(4, 9);
                block_copy = Instantiate(block, new Vector3(12*i, height, z * 4), Quaternion.identity);
                block_copy.transform.localScale = new Vector3(4, height * 2, 4);
            }
        }

        for (int z = 28; z < levelLength * 20; z += 20)
        {
            Instantiate(plane, new Vector3(0, 0, z), Quaternion.identity);
        }
    }
}