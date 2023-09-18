using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Board : Singleton<Board>
{
    [Header("Prefabs")]
    [SerializeField] private List<Cell> cellsPrefabs;
    [SerializeField] private Player playerPrefab;

    public List<Cell> Cells { get; private set; }
    public Cell StartCell { get; private set; }
    public Cell ExitCell { get; private set; }
    public Player.PlayerRotation StartRotation { get; private set; }
    public Player Player { get; private set; }

    //GENERATE BOARD 
    public void GenerateBoard(BoardLayout board)
    {
        //CLEAR CELLS 
        Cells = new List<Cell>();
        BoardLayout boardData = board;

        int index = -1;
        for (int y = 0; y < boardData.boardSizeY; y++)
        {
            for (int x = 0; x < boardData.boardSizeX; x++)
            {
                index++;
                Vector3 pos = new Vector3(x, 0, -y);
                Cell cellPrefab = cellsPrefabs.Find(x => x.cellType == boardData.boardCellsType[index]);
                Cell cell = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                cell.Init(pos);

                Cells.Add(cell);

                if (cell.cellType == CellType.Start)
                {
                    if (StartCell == null)
                        StartCell = cell;
                    else
                        Debug.LogError("More than one START CELL");
                }
                else if (cell.cellType == CellType.Exit)
                {
                    if (ExitCell == null)
                        ExitCell = cell;
                    else
                        Debug.LogError("More than one EXIT CELL");
                }
            }
        }

        transform.position = -FindCenterOfTransforms();
    }

    //GENERATE PLAYER IN START CELL AND ROTATION
    public void GeneratePlayer(BoardLayout board)
    {
        Player player = Instantiate(playerPrefab);
        player.transform.SetParent(transform);
        player.transform.localPosition = StartCell.CellPosition();
        player.SetCellPosition(StartCell);

        StartRotation = board.playerStartRotation;
        player.SetRotation(StartRotation);

        this.Player = player;
    }

    //RESET PLAYER POSITION AND ROTATION
    public void SetPlayerToStart()
    {
        if (this.Player == null || StartCell == null)
            return;
        
        this.Player.transform.localPosition = StartCell.CellPosition();
        this.Player.SetCellPosition(StartCell);
        this.Player.SetRotation(StartRotation);
    }

    //CLEAR BOARD AND RESET POSITION
    public void ClearBoard()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        Cells.Clear();
        StartCell = null;
        ExitCell = null;
        transform.position = Vector3.zero;
    }

    //GET BOARD CELL
    public Cell GetCell(Vector3 position)
    {
        return Cells.Find(x => x.CellPosition() == position);
    }

    //CHECK IF CELL IS HAS A OBSTACLE (TO AVOID PLAYER MOVEMENT)
    public bool IsCellObstacle(Cell cell)
    {
        if (cell == null)
            return true;
        else
            return cell.cellType == CellType.Obstacle;
    }

    //CHECK IF CELL IS AN EXIT (TO COMPLETE LEVEL)
    public bool IsExitCell(Cell cell)
    {
        if (cell == null)
            return false;
        else
            return cell.cellType == CellType.Exit;
    }

    //METHOD TO CENTER THE BOARD
    Vector3 FindCenterOfTransforms()
    {
        Vector3 centerPoint = Vector3.zero;

        foreach (Transform child in transform)
        {
            centerPoint += child.position;
        }

        centerPoint /= transform.childCount;
        return centerPoint;
    }
}
