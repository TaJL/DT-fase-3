using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BillboardGrid : MonoBehaviour
{

    public Vector3 grid_size = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AlignAllChildren();
    }


    private void AlignAllChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = SnapToGrid(transform.GetChild(i).position);
        }
    }

    public Vector3 SnapToGrid(Vector3 position)
    {
        return new Vector3(
        Mathf.Round(Mathf.Round(position.x * grid_size.x) / grid_size.x),
        Mathf.Round(Mathf.Round(position.y * grid_size.y) / grid_size.y),
        Mathf.Round(Mathf.Round(position.z * grid_size.z) / grid_size.z)
        );
    }
}
