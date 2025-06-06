using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

    [Range(5, 300)]
    public int mazeWidth, mazeHeight;
    private int startX, startY;
    MazeCell[,] maze;

    Vector2Int currentCell;

    private void Awake()
    {
        mazeWidth = Random.Range(7, 16);  
        mazeHeight = Random.Range(7, 16);
    }
    public MazeCell[,] GetMaze() { // ����������� ���� � ��������� � ���� ������

        maze = new MazeCell[mazeWidth, mazeHeight];

        for (int i = 0; i < mazeWidth; i++)
        {
            for (int j = 0; j < mazeHeight; j++)
            {
                maze[i, j] = new MazeCell(i, j);
            }
        }

        CarvePath(startX, startY);

        return maze;

    }

    List<Direction> directions = new List<Direction> {

        Direction.Up, Direction.Down, Direction.Left, Direction.Right

    };

    List<Direction> GetRandomDirections()
    {
        List<Direction> dir = new List<Direction>(directions);
        List<Direction> randomDir = new List<Direction>();

        while (dir.Count > 0)
        {
            int random = Random.Range(0, dir.Count);
            randomDir.Add(dir[random]);
            dir.RemoveAt(random);
        }

        return randomDir;
    }

    bool IsCellValid (int x, int y)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
            return false;
        else
            return true;
    }

    Vector2Int CheckNeighbours()
    {
        List<Direction> randomDir = GetRandomDirections();

        for (int i = 0; i < randomDir.Count; i++)
        {
            Vector2Int neighbour = currentCell;

            switch (randomDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
            }

            if (IsCellValid(neighbour.x, neighbour.y) && !maze[neighbour.x, neighbour.y].visited)
                return neighbour;
        }

        return currentCell;
    }

    void BreakWalls (Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)
        {
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y)
        {
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
        else if (primaryCell.y > secondaryCell.y)
        {
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
    }

    void CarvePath(int x, int y) // ��������� �� �������� �� ������� �������� (������� ����)
    {
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y = 0;
            Debug.LogWarning("Starting position is out of bounds, defaulting to (0,0)");
        }

        currentCell = new Vector2Int(x, y);

        List<Vector2Int> path = new List<Vector2Int>();

        bool deadEnd = false;

        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbours();

            if (nextCell == currentCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                { 
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();

                    if (nextCell != currentCell)
                        break;
                }

                if (nextCell == currentCell)
                    deadEnd = true;

            }
            else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }

    }

}

public enum Direction {
    Up,
    Down,
    Left,
    Right
}

public class MazeCell {

    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;

    public Vector2Int position {
        get {
            return new Vector2Int(x, y);
        }
    }

    public MazeCell (int x, int y)
    {
        this.x = x;
        this.y = y;

        visited = false;
        topWall = leftWall = true;
    }

}