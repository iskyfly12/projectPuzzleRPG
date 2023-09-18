using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//STORE ALL THE BOARDS LAYOUT
public class BoardsData : Singleton<BoardsData>
{
    [SerializeField] private List<BoardLayout> boards;

    public BoardLayout GetBoardLayout(int ID) { return boards.Find(x => x.ID == ID); }

    public int GetBoardsCount() { return boards.Count; }
}
