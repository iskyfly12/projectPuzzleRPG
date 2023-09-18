using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Action Type")]
    [SerializeField] private string actionType;

    private event Action<string> OnCardClick;

    private Button button;

    /*void Awake()
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

    //CALLED WHEN PLAYER SELECTED A CARD
    void OnClick()
    {
        if (OnCardClick != null)
            OnCardClick(actionType);

        Debug.Log("Card pressed");
    }*/

    public string Type() { return actionType; }

}
