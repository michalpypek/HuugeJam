using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject playerPrefab;

    public float scale;
    public float amplitude;
    public float heightScale;
    public float heightAmplitude;
    public Texture2D mazeImg;

    private List<GameObject> tileList = new List<GameObject>();

    private int[][] grid;                               // A jagged array of tile types representing the board, like a grid.
    private List<Vector2> frontierList = new List<Vector2>();
    private List<Vector2> neighbours = new List<Vector2>();
    private GameObject boardHolder;                           // GameObject that acts as a container for all other tiles.


    private void Start()
    {
        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");
        SetupTilesArray();
        CreateTileList();
        ImageToArray();
        InstantiateTiles();
        GameManager.instance.FindPlayer();
        GameManager.instance.playerScript.maze = boardHolder;
    }

    #region ListsFunctions

    void CreateTileList()
    {
        for (int i = 0; i < grid.Length * grid[0].Length; i++)
        {
            GameObject tile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            tile.SetActive(false);
            tile.transform.SetParent(boardHolder.transform);
            tileList.Add(tile);
        }
    }

    GameObject GetTile()
    {
        foreach (GameObject tile in tileList)
        {
            if (!tile.activeSelf)
            {
                return tile;
            }
        }

        return null;
    }

    void ResetTiles()
    {
        foreach (GameObject tile in tileList)
        {
            tile.SetActive(false);
        }
    }

    #endregion

    public void Reset()
    {

    }

    void SetupTilesArray()
    {
        grid = new int[mazeImg.width][];

        for (int i = 0; i < grid.Length; i++)
        {
            grid[i] = new int[mazeImg.height];
        }
    }

    void ImageToArray()
    {
        for (int x = 0; x < mazeImg.width; x++)
        {
            for (int y = 0; y < mazeImg.height; y++)
            {
                Color col = mazeImg.GetPixel(x, y);

                if (col == Color.white)
                {
                    grid[x][y] = 0;
                }
                // 2 - player
                if (col == Color.green)
                {
                    grid[x][y] = 2;
                }
                //3 - goal
                if (col == Color.blue)
                {
                    grid[x][y] = 3;
                }

                if (col == Color.black)

                {
                    grid[x][y] = 1;
                }
            }
        }
    }

    //void PrimGeneration()
    //{
    //    //A Grid consists of a 2 dimensional array of cells.
    //    //A Cell has 2 states: Blocked or Passage.
    //    //Start with a Grid full of Cells in state Blocked.
    //    for (int x = 0; x < grid.Length; x++)
    //    {
    //        for (int y = 0; y < grid[x].Length; y++)
    //        {
    //            grid[x][y] = 1;
    //        }
    //    }

    //    //Pick a random Cell, set it to state Passage and Compute its frontier cells.A frontier cell of a Cell is a cell with distance 2 in state Blocked and within the grid.
    //    int randX = Random.Range(0, columns);
    //    int randY = Random.Range(0, rows);
    //    grid[randX][randY] = 0;
    //    AddFrontier(randX, randY);

    //    //While the list of frontier cells is not empty:
    //    while (frontierList.Count>0)
    //    {
    //        //Pick a random frontier cell from the list of frontier cells.
    //        Vector2 randFrontier = GetRandomFromList(frontierList);
    //        //Let neighbors(frontierCell) = All cells in distance 2 in state Passage.
    //        SetNeighbours(ref randFrontier);
    //        //Pick a random neighbor and connect the frontier cell with the neighbor by setting the cell in-between to state Passage.
    //        Vector2 randNeighbour = GetRandomFromList(neighbours);

    //        if (randNeighbour.x != -1 && randNeighbour.y != -1)
    //        {
    //            ConnectNeighbour(ref randFrontier, ref randNeighbour);
    //        }
    //        //Compute the frontier cells of the chosen frontier cell and add them to the frontier list.
    //        AddFrontier((int)randFrontier.x,(int)randFrontier.y);

    //        //Remove the chosen frontier cell from the list of frontier cells.

    //        frontierList.Remove(randFrontier);

    //    }
    //}

    //void AddFrontier(int x, int y)
    //{
    //    if (x - 2 > 0 && x - 2 < columns)
    //    { 
    //        frontierList.Add(new Vector2(x - 2, y));
    //    }
    //    if (x + 2 > 0 && x + 2 < columns)
    //    {
    //        frontierList.Add(new Vector2(x + 2, y));
    //    }
    //    if (y - 2 > 0 && y - 2 < rows)
    //    {
    //        frontierList.Add(new Vector2(x, y - 2));
    //    }
    //    if (y + 2 > 0 && y + 2 < rows)
    //    {
    //        frontierList.Add(new Vector2(x, y + 2));
    //    }
    //}

    //Vector2 GetRandomFromList(List<Vector2> list)
    //{
    //    if (list.Count > 0)
    //    {
    //        int randX = Random.Range(0, list.Count - 1);

    //        return list[randX];
    //    }

    //    else
    //    {
    //        return new Vector2(-1,-1);
    //    }
    //}

    //void SetNeighbours(ref Vector2 cell)
    //{
    //    neighbours.Clear();
    //    Debug.Log(cell.x);
    //    if (cell.x - 2 > 0 && cell.x - 2 < columns && grid[(int)cell.x - 2][(int)cell.y] == 0)
    //    {
    //        neighbours.Add(new Vector2(cell.x - 2, cell.y));
    //    }

    //    if (cell.x + 2 > 0 && cell.x + 2 < columns && grid[(int)cell.x + 2][(int)cell.y] == 0)
    //    {
    //        neighbours.Add(new Vector2(cell.x + 2, cell.y));
    //    }

    //    if (cell.y - 2 > 0 && cell.y - 2 < rows && grid[(int)cell.x][(int)cell.y - 2] == 0)
    //    { 
    //        neighbours.Add(new Vector2(cell.x, cell.y - 2));
    //    }

    //    if (cell.y + 2 > 0 && cell.y + 2 < rows && grid[(int)cell.x ][(int)cell.y + 2] == 0)
    //    {
    //        neighbours.Add(new Vector2(cell.x, cell.y + 2));
    //    }
    //}

    //void ConnectNeighbour(ref Vector2 cell,ref Vector2 neighbour)
    //{
    //    switch((int)(neighbour.x - cell.x))
    //    {
    //        case (-2):
    //            grid[(int)cell.x - 1][(int)cell.y] = 0;
    //            return;
    //        case (2):
    //            grid[(int)cell.x + 1][(int)cell.y] = 0;
    //            return;
    //        default: break;
    //    }

    //    switch ((int)(neighbour.y - cell.y))
    //    {
    //        case (-2):
    //            grid[(int)cell.x][(int)cell.y - 1] = 0;
    //            return;
    //        case (2):
    //            grid[(int)cell.x][(int)cell.y + 1] = 0;
    //            return;
    //        default: break;
    //    }
    //}

    void ArrayGoInCircles()
    {
        int minX = 0;
        int maxX = 5;
        int minY = 0;
        int maxY = 5;

        for (int i = minX; i < maxX; i++)
        {
            for (int j = minY; j < maxY; j++)
            {
                if (i == minX || i == maxX || j == maxY)
                {
                    //Set the height
                }
            }
        }
    }

    void InstantiateTiles()
    {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                // ... and instantiate correct tile for it
                //if (grid[i][j] == 0)
                //{
                //    GameObject tile = GetTile();
                //    tile.transform.position = new Vector2(i, j);
                //    tile.SetActive(true);
                //    tile.GetComponent<SpriteRenderer>().color = Color.white;
                //}

                if (grid[i][j] == 1)
                {
                    GameObject tile = GetTile();
                    tile.transform.position = new Vector2(i, j);
                    tile.SetActive(true);
                    //tile.GetComponent<SpriteRenderer>().color = Color.black;
                }

                if(grid[i][j] == 2)
                {
                    GameObject player = (GameObject)Instantiate(playerPrefab, new Vector3(i, j, 0), Quaternion.identity);
                    player.transform.SetParent(boardHolder.transform);
                    player.gameObject.name = "Player";
                }
            }
        }
    }

    bool RandomChance(int percentage)
    {
        return Random.Range(0, 100) < percentage;
    }

}
