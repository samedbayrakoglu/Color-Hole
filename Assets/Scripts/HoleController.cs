using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [Space]
    [SerializeField] MeshFilter boardMeshFilter;
    [SerializeField] MeshCollider boardMeshCollider;

    [Space]
    [SerializeField] float holeEncloseRadius;
    [SerializeField] Transform holeCenter;

    [Space]
    [SerializeField] float movementSpeed;

    private Mesh referenceMesh;

    private List<int> holeVertices = new();
    private List<Vector3> holeVerticeOffsets = new();

    private int holeVerticesCount;

    private float deltaX, deltaY;

    private Vector3 newHoleCenter, updatedHoleCenter;



    private void Awake()
    {
        SetupGameData();

        referenceMesh = boardMeshFilter.mesh;

        DetermineHoleVertices(); // vertices belongs to hole circle

        holeVerticesCount = holeVertices.Count;

    }

    private void SetupGameData()
    {
        GameData.isGameOver = false;
        GameData.isMoving = false;
    }

    private void Update()
    {
        GameData.isMoving = Input.GetMouseButton(0);

        if (GameData.isMoving && !GameData.isGameOver)
        {
            MoveHoleCenter();

            UpdateHoleVerticesPosition();
        }
    }

    private void MoveHoleCenter()
    {
        deltaX = Input.GetAxis("Mouse X");
        deltaY = Input.GetAxis("Mouse Y");

        Vector3 delta = new Vector3(deltaX, 0f, deltaY);
        newHoleCenter = Vector3.Lerp(holeCenter.position, holeCenter.position + delta, movementSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(newHoleCenter.x, -referenceMesh.bounds.extents.x + holeEncloseRadius, referenceMesh.bounds.extents.x - holeEncloseRadius);
        float clampedZ = Mathf.Clamp(newHoleCenter.z, -referenceMesh.bounds.extents.z + holeEncloseRadius, referenceMesh.bounds.extents.z - holeEncloseRadius);

        updatedHoleCenter = new Vector3(clampedX, newHoleCenter.y, clampedZ);

        holeCenter.position = updatedHoleCenter;
    }

    private void UpdateHoleVerticesPosition()
    {
        Vector3[] verticesCopy = referenceMesh.vertices;

        for (int i = 0; i < holeVerticesCount; i++)
        {
            verticesCopy[holeVertices[i]] = holeCenter.position + holeVerticeOffsets[i];
        }

        referenceMesh.vertices = verticesCopy;

        boardMeshFilter.mesh = referenceMesh;
        boardMeshCollider.sharedMesh = referenceMesh;
    }

    private void DetermineHoleVertices()
    {
        for (int i = 0; i < referenceMesh.vertexCount; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, referenceMesh.vertices[i]);

            if (distance < holeEncloseRadius)
            {
                holeVertices.Add(i);

                holeVerticeOffsets.Add(referenceMesh.vertices[i] - holeCenter.position);
            }
        }
    }
}
