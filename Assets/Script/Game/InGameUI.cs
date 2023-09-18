using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : Singleton<InGameUI>
{
    [Header("Prefabs")]
    [SerializeField] private List<Pile> pilesPrefab;
    [SerializeField] private List<Card> cardsPrefab;

    [Header("References")]
    [SerializeField] private Transform pilesParent;
    [SerializeField] private Transform cardsParent;

    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI attemptsText;
    [SerializeField] private CanvasGroup inGameButtonCanvas;
    [SerializeField] private CanvasGroup inGamePilesCanvas;
    [SerializeField] private CanvasGroup inGameCompletePanelCanvas;
    [SerializeField] private CanvasGroup inGameTryAgainCanvas;
    [SerializeField] private CanvasGroup inGamePanelCanvas;
    [SerializeField] private CanvasGroup menuPanelCanvas;

    
    //CREATE PILES
    public Pile InstanciatePile(string type)
    {
        Pile pile = pilesPrefab.Find(x => x.Type() == type);
        return Instantiate(pile, pilesParent);
    }

    //CREATE CARDS
    public Card InstanciateCard(string type)
    {
        Card card = cardsPrefab.Find(x => x.Type() == type);
        return Instantiate(card, cardsParent);
    }

    public void DestroyAllCards()
    {
        foreach (Transform child in cardsParent)
            Destroy(child.gameObject);
    }

    public void DestroyAllPiles()
    {
        foreach (Transform child in pilesParent)
            Destroy(child.gameObject);
    }

    #region ENABLE/DISABLE UI
    public void ShowInGameButtons(bool show)
    {
        CanvasGroupGraphic(inGameButtonCanvas, show);
    }

    public void ShowInGamePiles(bool show)
    {
        CanvasGroupGraphic(inGamePilesCanvas, show);
    }

    public void ShowInGameCompletePanel(bool show)
    {
        CanvasGroupGraphic(inGameCompletePanelCanvas, show);
    }

    public void ShowInGameTryAgain(bool show)
    {
        CanvasGroupGraphic(inGameTryAgainCanvas, show);
    }

    public void ShowInGamePanel(bool show)
    {
        CanvasGroupGraphic(inGamePanelCanvas, show);
    }

    public void ShowMenuPanel(bool show)
    {
        CanvasGroupGraphic(menuPanelCanvas, show);
    }
    #endregion

    #region SET UI
    public void UpdateAttemptValue(int value)
    {
        attemptsText.text = value.ToString();
    }
    #endregion

    #region METHODS
    void CanvasGroupGraphic(CanvasGroup canvas, bool enable)
    {
        int from = enable ? 0 : 1;
        int to = enable ? 1 : 0;
        canvas.blocksRaycasts = enable;
        DOVirtual.Float(from, to, 0.5f, x => canvas.alpha = x);
    }
    #endregion
}
