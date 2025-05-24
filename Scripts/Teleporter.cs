using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public MazeGenerator mazeGenerator;
    public Transform current_position;

    private void Awake()
    {
        current_position = GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Teleport"))
        {
            MazeCell[,] maze = mazeGenerator.GetMaze();
            int width = mazeGenerator.mazeWidth;
            int height = mazeGenerator.mazeHeight;

            Vector2Int randomCell = new Vector2Int(
                        Random.Range(0, width - 1),
                        Random.Range(0, height - 1)
                        );


            Vector3 destination;
            destination.x = randomCell.x;
            destination.y = 0;
            destination.z = randomCell.y;

            other.gameObject.SetActive(false);
            current_position.position = destination;
        }
    }
}
