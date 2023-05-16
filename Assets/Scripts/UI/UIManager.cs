using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public event Action StartGame;

    [SerializeField]
    private MainScreenUIScript _mainMenuScreen;

    [SerializeField]
    private GameScreenUIScript _gamePlayScreen;

    private void Awake()
    {
        _mainMenuScreen.Init();
        _gamePlayScreen.Init();
        _mainMenuScreen.PlayClicked += OnPlayClicked;
    }

    private void Start()
    {
        _mainMenuScreen.Open();
    }

    private void OnPlayClicked()
    {
        _gamePlayScreen.HideSelectedMovesUI();
        _gamePlayScreen.Open();
        _mainMenuScreen.Close();
        StartGame?.Invoke();
    }

    public void OnRoundLost()
    {
        _mainMenuScreen.Open();
        _gamePlayScreen.Close();
    }

    public void DisplayPlayedMoves(SelectedMoveData playerMoveData, SelectedMoveData cpuMoveData)
    {
        _gamePlayScreen.DisplayRoundMoves(playerMoveData, cpuMoveData);
    }

    public void ShowResult(string result)
    {
        _gamePlayScreen.ShowResult(result);
    }

    public void ShowCpuMove(SelectedMoveData cpuMoveData)
    {
        _gamePlayScreen.DisplayCpuMove(cpuMoveData);
    }

    public void OnRoundFinished()
    {
        _gamePlayScreen.HideSelectedMovesUI();
    }
}