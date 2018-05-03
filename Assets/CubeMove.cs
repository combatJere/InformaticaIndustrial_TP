using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void Move()
    {
        Vector3 moveVector = new Vector3(10, 0, 0);
        transform.Rotate(moveVector);
    }
}
