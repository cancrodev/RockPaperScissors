using System;
using UnityEngine;
using UnityEngine.UI;
using static GlobalEnums;

public class PlayerMove : MonoBehaviour, IPlayerMove
{
    public event Action<MoveType> MovePlayed;

    [SerializeField]
    private MoveType _moveType;

    [SerializeField]
    private Button _button;

    public void Init()
    {
        _button.onClick.AddListener(() => OnButtonClicked());
        Deactivate();
    }

    public void Activate()
    {
        _button.interactable = true;
    }

    private void OnButtonClicked()
    {
        MovePlayed?.Invoke(_moveType);
    }

    public void Deactivate()
    {
        _button.interactable = false;
    }
}