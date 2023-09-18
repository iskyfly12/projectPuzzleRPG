using UnityEngine;

public enum CellType { Empty, Start, Obstacle, Exit };

[System.Serializable]
public class Cell : MonoBehaviour
{
    public CellType cellType;
    private Vector3 position;

    public void Init(Vector3 position)
    {
        this.position = position;
    }
    /*
    public Cell(Vector3 position)
    {
        this.position = position;
    }
    */
    public Vector3 CellPosition()
    {
        return position;
    }
}
