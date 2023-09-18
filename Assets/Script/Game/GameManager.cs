using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<Action> actionsOrder = new List<Action>();
    private List<Card> cardsOrder = new List<Card>();
    private List<Pile> pilesOrder = new List<Pile>();
    private List<string> actions = new List<string>();

    private BoardLayout currentBoardLayout;

    private int attempts = 0;

    void Start()
    {
        StartGame();
    }

    //START THE GAME
    public void StartGame()
    {
        currentBoardLayout = null;

        for (int i = 0; i < GetAllActions().Count(); i++)
            actions.Add(GetAllActions().ToList()[i].Name);

        InGameUI.Instance.ShowMenuPanel(true);
    }

    //INITIALIZE STAGE FROM LEVEL SELECTED
    public void InitializeStage(BoardLayout board)
    {
        //CLEAN BOARD
        if (currentBoardLayout != null)
        {
            Board.Instance.ClearBoard();
            ClearCard();
            ClearPiles();
        }

        //GENERATE NEW BOARD
        Board.Instance.GenerateBoard(board);
        Board.Instance.GeneratePlayer(board);
        currentBoardLayout = board;

        //ENABLE IN-GAME UI
        InGameUI.Instance.ShowInGamePanel(true);
        InGameUI.Instance.ShowInGameButtons(true);
        InGameUI.Instance.ShowInGamePiles(true); 
        InGameUI.Instance.ShowInGameCompletePanel(false);

        attempts = 0;

        //GENERATE PILES
        pilesOrder = new List<Pile>();
        int pilesCount = board.cardsSettings.Count;
        for (int i = 0; i < pilesCount; i++)
        {
            BoardLayout.CardsSettings cardSettings = board.cardsSettings[i];
            string actionType = cardSettings.actionType;
            int cardsCount = cardSettings.count;

            Pile pile = InGameUI.Instance.InstanciatePile(actionType);
            pile.Init(AddCard);
            pile.SetCardsCount(cardsCount);

            pilesOrder.Add(pile);
        }
    }

    //CALLED WHEN PRESSED 'COMEÇAR' BUTTON
    public void StartStage()
    {
        if (currentBoardLayout == null)
            return;

        InGameUI.Instance.ShowInGameButtons(false);
        InGameUI.Instance.ShowInGamePiles(false);

        StartCoroutine(StartStageEnumerator());
    }

    //COMPLETE AND SAVE THE LEVEL
    void Complete()
    {
        InGameUI.Instance.ShowInGameCompletePanel(true);

        SaveSystem.Instance.SaveLevelAsCompleted(currentBoardLayout.ID);
    }

    //ADD CARD WHEN PRESSED ONE OF THE PILES
    void AddCard(Pile pile)
    {
        int pileCardCount = pile.GetCardsCount();

        if (pileCardCount <= 0) return;
        pile.SetCardsCount(pile.GetCardsCount() - 1);

        string pileType = pile.Type();

        //INSTANTIATE CARD
        Card card = InGameUI.Instance.InstanciateCard(pileType);
        cardsOrder.Add(card);

        //SET ACTION ORDER
        Action action = GetAllActions().ToList().Find(x => x.Name == pileType);
        actionsOrder.Add(action);

        Debug.Log("Card added: " + action.Name);
    }

    //CLEAR ALL CARDS WHEN PRESSED 'LIMPAR' BUTTON
    void ClearCard()
    {
        InGameUI.Instance.DestroyAllCards();

        //RESET PILES CARDS NUMBER
        for (int i = 0; i < pilesOrder.Count; i++)
        {
            BoardLayout.CardsSettings cardSettings = currentBoardLayout.cardsSettings[i];
            int cardsCount = cardSettings.count;
            pilesOrder[i].SetCardsCount(cardsCount);
        }

        actionsOrder.Clear();
        cardsOrder.Clear();
    }

    //CLEAR PILES
    void ClearPiles()
    {
        InGameUI.Instance.DestroyAllPiles();
        pilesOrder.Clear();
    }

    //GET ALL ACTIONS 
    IEnumerable<Action> GetAllActions()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(Action)))
            .Select(type => Activator.CreateInstance(type) as Action);
    }

    //START PLAYER MOVEMENTS
    IEnumerator StartStageEnumerator()
    {
        yield return new WaitForEndOfFrame();

        Player player = Board.Instance.Player;

        yield return new WaitForSeconds(1);

        //FOR EACH ACTION EXECUTE PLAYER MOVEMENT
        for (int i = 0; i < actionsOrder.Count; i++)
        {
            Vector3 currentPlayerPosition = player.GetCellPosition();
            ActionType actionType = actionsOrder[i].Type;
            switch (actionType)
            {
                case ActionType.MoveAction:

                    Cell forwardCell = Board.Instance.GetCell(player.ForwardCell());
                    bool hasObstacle = Board.Instance.IsCellObstacle(forwardCell);

                    if (!hasObstacle)
                    {
                        actionsOrder[i].Execute(player.transform);
                        player.SetCellPosition(forwardCell);
                    }
                    else
                    {
                        Debug.Log("Cant move forward!");
                    }
                    break;
                case ActionType.RotateAction:
                    actionsOrder[i].Execute(player.transform);
                    break;
                case ActionType.AttackAction:
                    break;
            }

            Destroy(cardsOrder[i].gameObject);
            yield return new WaitForSeconds(1);
        }

        //CHECK IF PLAYER IS IN THE EXIT CELL
        Cell currentLastCell = Board.Instance.GetCell(player.GetCellPosition());
        bool isExit = Board.Instance.IsExitCell(currentLastCell);

        //UPDATE ATTEMPT
        InGameUI.Instance.UpdateAttemptValue(attempts + 1);

        //RESET PILES AND CARDS IF PLAYER IS NOT IN THE EXIT CELL
        if (isExit)
        {
            Complete();
        } 
        else
        {
            ClearCard();
            Board.Instance.SetPlayerToStart();
            InGameUI.Instance.ShowInGameButtons(true);
            InGameUI.Instance.ShowInGamePiles(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EraseGame()
    {
        SaveSystem.Instance.ClearSave();
    }
}
