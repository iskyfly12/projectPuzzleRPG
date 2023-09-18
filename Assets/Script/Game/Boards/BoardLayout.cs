using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardLayoutData", menuName = "ScriptableObjects/BoardScriptableObject", order = 1)]
public class BoardLayout : ScriptableObject
{
    [Header("ID")]
    public int ID;

    [Header("Board Settings")]
    public int boardSizeX = 5;
    public int boardSizeY = 5;

    [Header("Cards Settings")]
    public List<CardsSettings> cardsSettings;

    [Header("Cards Settings")]
    public Player.PlayerRotation playerStartRotation;

    [HideInInspector] public CellType[] boardCellsType;

    [System.Serializable]
    public struct CardsSettings
    {
        public string actionType;
        public int count;
    }
}
