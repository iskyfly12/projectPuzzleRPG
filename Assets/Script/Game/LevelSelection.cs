using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    private List<LevelButton> levels = new List<LevelButton>();

    [SerializeField] private LevelButton levelButtonPrefab;

    void Start()
    {
        InitializeLevelSelection();
    }

    //CREATE LEVELS BASED ON BOARDS LAYOUT COUNT
    public void InitializeLevelSelection()
    {
        int boardsCount = BoardsData.Instance.GetBoardsCount();
        Debug.Log(boardsCount);
        for (int i = 1; i <= boardsCount; i++)
        {
            BoardLayout board = BoardsData.Instance.GetBoardLayout(i);
            if (board == null)
                continue;

            LevelButton levelButton = Instantiate(levelButtonPrefab, transform);
            levelButton.Initialize(StartLevel, board, board.ID);

            levels.Add(levelButton);    
        }

        UpdateCompletedLevels();
    }

    //UPDATE BUTTONS IF THE LEVEL IS COMPLETE
    public void UpdateCompletedLevels()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            bool isCompleted = SaveSystem.Instance.IsLevelCompleted(levels[i].LevelNumber);
            levels[i].SetCompleted(isCompleted);
        }
    }

    //CALLED WHEN PLAYER SELECT A LEVEL
    public void StartLevel(LevelButton level)
    {
        if (level.BoardLayout == null)
            return;

        GameManager.Instance.InitializeStage(level.BoardLayout);
        InGameUI.Instance.ShowMenuPanel(false);
        InGameUI.Instance.ShowInGamePiles(true);
        InGameUI.Instance.ShowInGameButtons(true);  
    }
}
