using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    public Transform[] objectsToCheck;
    private List<Transform> usedPositions = new List<Transform>();
    private Vector3[] initialPositions;
    private bool[] initialPositionsStatus;
    public GameObject[] objectsToRandom;
    public Transform[] positionsRandom;

    public GameObject switcher;
    public GameObject voltNeedle;
    public GameObject ampereNeedle;
    public GameObject lighter;

    private bool voltNeedleRotated = false;
    private bool ampereNeedleRotated = false;
    public Vector3 voltRotationPoint;
    public Vector3 ampereRotationPoint;

    private void Start()
    {
        initialPositions = new Vector3[objectsToCheck.Length];
        initialPositionsStatus = new bool[objectsToCheck.Length];

       
        for (int i = 0; i < objectsToCheck.Length; i++)
        {
            initialPositions[i] = objectsToCheck[i].position;
            initialPositionsStatus[i] = true;
        }

        RandomizeObjectPositions();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckObjectsPositions();
        }
        if (PrintResults() && !voltNeedleRotated)
        {
            RotateVoltNeedle();
            voltNeedleRotated = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra xem va chạm có phải là với Switcher không
        if (other.gameObject == switcher && PrintResults() && !ampereNeedleRotated&&voltNeedleRotated)
        {
            RotateAmpereNeedle();
            ampereNeedleRotated = true;
        }
    }
    private void CheckObjectsPositions()
    {
        for (int i = 0; i < objectsToCheck.Length; i++)
        {
            Transform objectToCheck = objectsToCheck[i];

            
            if (Vector3.Distance(initialPositions[i], objectToCheck.position) <=0.4f)
            {
                initialPositionsStatus[i] = true;
            }
            else
            {
                initialPositionsStatus[i] = false;
            }
        }

        Debug.Log(PrintResults());
    }
    private void RandomizeObjectPositions()
    {
      
        usedPositions.Clear();

       
        for (int i = 0; i < objectsToRandom.Length; i++)
        {
            
            Transform randomPosition = GetRandomUnusedPosition();

           
            objectsToRandom[i].transform.position = randomPosition.position;

           
            usedPositions.Add(randomPosition);
        }
    }
    private Transform GetRandomUnusedPosition()
    {
        
        while (true)
        {
            
            Transform randomPosition = positionsRandom[Random.Range(0, positionsRandom.Length)];

            
            if (!usedPositions.Contains(randomPosition))
            {
                return randomPosition;
            }
        }
    }
    private bool PrintResults()
    {
        for (int i = 0; i < initialPositionsStatus.Length; i++)
        {
            if (initialPositionsStatus[i]==false)
            { return false; }
        }
        return true;
    }
    private void RotateVoltNeedle()
    {
        voltNeedle.transform.RotateAround(voltRotationPoint, Vector3.forward, 60f);
        lighter.SetActive(true);
    }
    private void RotateAmpereNeedle()
    {
        ampereNeedle.transform.RotateAround(ampereRotationPoint, Vector3.forward, 60f);
    }
}
