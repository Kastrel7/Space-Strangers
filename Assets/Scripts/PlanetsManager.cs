using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsManager : MonoBehaviour
{
    public int objectCount = 40; // Total number of objects
    public float minDistance = 100.0f; // Minimum distance between objects
    public Vector3 areaSize = new Vector3(750, 200, 750); // Max area size (x, y, z)
    Transform trans;

    private List<Vector3> positions = new List<Vector3>(); // Stores the positions of placed objects

    void Start()
    {
        trans = GetComponent<Transform>();
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 newPosition = GenerateRandomPosition();

            // Retry until a valid position is found
            while (!IsValidPosition(newPosition))
            {
                newPosition = GenerateRandomPosition();
            }

            // Add valid position to the list and instantiate the object
            positions.Add(newPosition);
            trans.position = newPosition;
        }
    }

    // Generate random position within the defined area
    Vector3 GenerateRandomPosition()
    {
        float x = Random.Range(-areaSize.x / 2, areaSize.x / 2);
        float y = Random.Range(-areaSize.y / 2, areaSize.y / 2);
        float z = Random.Range(-areaSize.z / 2, areaSize.z / 2);

        return new Vector3(x, y, z);
    }

    // Check if the generated position is valid (far enough from other objects)
    bool IsValidPosition(Vector3 position)
    {
        foreach (Vector3 existingPosition in positions)
        {
            if (Vector3.Distance(existingPosition, position) < minDistance)
            {
                return false;
            }
        }

        return true;
    }
}