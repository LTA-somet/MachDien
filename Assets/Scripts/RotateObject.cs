using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform pivotPoint;
    public Vector3 rotationAxis = new Vector3(0, 1, 0);
    public float rotationSpeed = 5f;

    private float elapsedTime = 0f;

    void Update()
    {
        if (elapsedTime < 3f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivotPoint.position, rotationAxis, rotationAmount);
            elapsedTime += Time.deltaTime;
        }
    }
}
