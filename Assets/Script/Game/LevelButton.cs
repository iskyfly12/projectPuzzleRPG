using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color colorCompleted;
    [SerializeField] private Color colorNotCompleted;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI textLevelNumber;

    private event Action<LevelButton> OnLevelClick;
    private Button button;
    private Image buttonImage;

    public BoardLayout BoardLayout { get; private set; }
    public int LevelNumber {  get; private set; }

    void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }

    //INITIALIZE LEVEL BUTTON 
    public void Initialize(Action<LevelButton> onLevelClickEvent, BoardLayout board, int levelNumber)
    {
        this.OnLevelClick = onLevelClickEvent;
        this.BoardLayout = board;
        
        LevelNumber = levelNumber;
        textLevelNumber.text = levelNumber.ToString();
    }

    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    //CALLED WHEN PLAYER SELECT A LEVEL IN LEVEL SELECTION
    void OnClick()
    {
        if (OnLevelClick != null)
            OnLevelClick(this);
    }

    //CHANGE WHEN LEVEL IS OR IS NOT COMPLETED
    public void SetCompleted(bool completed)
    {
        buttonImage.color = completed ? colorCompleted : colorNotCompleted;
    }
}
