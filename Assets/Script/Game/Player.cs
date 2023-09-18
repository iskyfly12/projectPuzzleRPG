using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerRotation { Up, Down, Left, Right }
    private Vector3 cellPosition;

    public Vector3 GetCellPosition()
    {
        return cellPosition;
    }
    public void SetCellPosition(Cell cell)
    {
        cellPosition = cell.CellPosition();
    }

    //GET THE CELL IN FRONT OF THE PLAYER
    public Vector3 ForwardCell()
    {
        return transform.localPosition + transform.right;
    }

    public void SetRotation(PlayerRotation rotation)
    {
        switch (rotation)
        {
            case PlayerRotation.Up:
                transform.localEulerAngles = (Vector3.up * 270);
                break;
            case PlayerRotation.Down:
                transform.localEulerAngles = (Vector3.up * 90);
                break;
            case PlayerRotation.Left:
                transform.localEulerAngles = (Vector3.up * 180);
                break;
            case PlayerRotation.Right:
                transform.localEulerAngles = (Vector3.up * 0);
                break;
        }
    }
}
