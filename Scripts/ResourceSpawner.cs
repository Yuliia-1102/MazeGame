using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject artifactPrefab;
    public GameObject bombPrefab;
    public GameObject pillPrefab;
    public GameObject teleportPrefab;
    public GameObject timePrefab;

    public MazeGenerator mazeGenerator;
    public MazeRenderer mazeRenderer;
    public AnotherUI anotherUI;

    public int artifactCount;
    private int bombCount;
    private int pillCount;
    private int teleportCount;
    private int timeCount;

    public float yOffset = 0.2f;
    private float cellSize; 

    private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

    void Start()
    {
        if (mazeRenderer == null) mazeRenderer = FindFirstObjectByType<MazeRenderer>();
        cellSize = mazeRenderer.CellSize;

        artifactCount = Random.Range(3, 8);
        pillCount = Random.Range(4, 8);
        teleportCount = Random.Range(4, 8);
        bombCount = Random.Range(5, 11);
        timeCount = Random.Range(4, 11);

        PlaceArtifacts(artifactPrefab, artifactCount);
        PlaceArtifacts(bombPrefab, bombCount);
        PlaceArtifacts(pillPrefab, pillCount);
        PlaceArtifacts(teleportPrefab, teleportCount);
        PlaceArtifacts(timePrefab, timeCount);

        anotherUI.UpdateMaxValueText(artifactCount);
    }

    void PlaceArtifacts(GameObject prefab, int count)
    {
        MazeCell[,] maze = mazeGenerator.GetMaze();
        int width = mazeGenerator.mazeWidth;
        int height = mazeGenerator.mazeHeight;

        for (int i = 0; i < count; i++)
        {
            Vector2Int randomCell;
            bool positionFound = false;
            int attempts = 0;

            do
            {
                randomCell = new Vector2Int(
                    Random.Range(-2, width - 2), 
                    Random.Range(-2, height - 2)
                );
                attempts++;
                positionFound = IsPositionValid(randomCell);
            } while (!positionFound && attempts < 100);

            if (positionFound)
            {
                Vector3 spawnPos = new Vector3(
                    randomCell.x * cellSize + cellSize * 0.5f,
                    yOffset,
                    randomCell.y * cellSize + cellSize * 0.5f
                );

                Instantiate(prefab, spawnPos, Quaternion.identity, transform);
                occupiedPositions.Add(randomCell);
            }
        }
    }

    bool IsPositionValid(Vector2Int cell)
    {
        if (occupiedPositions.Contains(cell)) return false;
        return true;
    }
}