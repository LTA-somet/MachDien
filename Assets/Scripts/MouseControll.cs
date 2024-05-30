using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseControll : MonoBehaviour
{


    private Vector3 mOffset;
    private Vector3 initialPosition;
    public Transform[] targetPositions;
    public Transform objectToMove;
    private bool reachedTarget = false;
    private List<Transform> occupiedPositions = new List<Transform>();
    private Transform FindNearestTargetPosition()
    {
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform target in targetPositions)
        {
            Vector3 screenPos = target.position;
            Vector3 objectPos = objectToMove.position;
            float distance = Vector3.Distance(objectPos, screenPos);

           
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target;
            }
        }

        return nearestTarget;
    }
  
    void Update()
    {
        Transform nearestTarget = FindNearestTargetPosition();

        if (nearestTarget != null)
        {
            Vector3 targetScreenPos = nearestTarget.position;
            Vector3 objectPos = objectToMove.position;
            float distanceToTarget = Vector3.Distance(objectPos, targetScreenPos);

            float threshold = 1.0f;

            if (distanceToTarget < threshold /*&& !IsPositionOccupied(nearestTarget)*/)
            {
                objectToMove.position = nearestTarget.position;
                reachedTarget = true;
            }
            else
            {
                reachedTarget = false;
            }
        }
    }
    private bool IsPositionOccupied(Transform position)
    {
        return occupiedPositions.Contains(position);
    }
    void OnMouseDown()
    {
        Vector3 vector = objectToMove.position - GetMouseWorldPos();
        mOffset = new Vector3(vector.x, vector.y, vector.z);
        initialPosition = objectToMove.position;
        reachedTarget = false;
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, LayerMask.GetMask("obj")))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }


    }

    void OnMouseDrag()
    {
        Vector3 mousePos = GetMouseWorldPos();
        Vector3 dis = mOffset + mousePos;
        if (mousePos != Vector3.zero)
        {
            objectToMove.position = new Vector3(dis.x, dis.y, 0.3f);
        }
    }


    void OnMouseUp()
    {
        if (!reachedTarget)
        {
            objectToMove.position = initialPosition;
        }

    }
}
