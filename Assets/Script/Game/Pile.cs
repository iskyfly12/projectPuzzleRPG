using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pile : MonoBehaviour
{
    [Header("Action Type")]
    [SerializeField] private string actionType;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textCardsCount;

    private event Action<Pile> OnPileClick;
    private Button button;

    private int cardsCount;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    //CALLED WHEN PLAYER PRESS A PILE
    void OnClick()
    {
        if (OnPileClick != null)
            OnPileClick(this);
    }

    //INITIALIZE PILE
    public void Init(Action<Pile> onPileClickEvent)
    {
        this.OnPileClick = onPileClickEvent;
    }

    //SET NUMBER OF THE CARDS OF THE PILE
    public void SetCardsCount(int value)
    {
        cardsCount = value;
        textCardsCount.text = cardsCount.ToString();
    }

    public int GetCardsCount()
    {
        return cardsCount;
    }

    public string Type() { return actionType; }
}
