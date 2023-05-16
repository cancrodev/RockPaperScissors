using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GlobalEnums;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerMove> _playerMovesList;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private MovesConfig _movesConfigData;

    private int _currentScore;
    private MoveType _currCpuMove;
    private List<MoveType> _cpuMoves = new List<MoveType> { MoveType.Rock, MoveType.Paper, MoveType.Scissors, MoveType.Lizard, MoveType.Spock };

    private Random _random = new Random();
    private StringBuilder _result = new StringBuilder();

    private void Start()
    {
        _uiManager.StartGame += OnGameStarted;
        for (int i = 0; i < _playerMovesList.Count; i++)
        {
            _playerMovesList[i].Init();
            _playerMovesList[i].MovePlayed += OnPlayerMoveSelected;
        }
    }

    private void OnPlayerMoveSelected(MoveType playerMove)
    {
        _timer.StopTimer();
        CheckMoves(playerMove, _currCpuMove);
    }

    private void CheckMoves(MoveType playerMove, MoveType cpuMove)
    {
        var playerMoveData = _movesConfigData.GetDataForMove(playerMove);
        var cpuMoveData = _movesConfigData.GetDataForMove(cpuMove);
        var playerUIData = new SelectedMoveData(playerMove.ToString(), playerMoveData._sprite);
        var cpuUIData = new SelectedMoveData(cpuMove.ToString(), cpuMoveData._sprite);
        _result.Length = 0;
        //If same moves, then round drawn
        if (playerMove.Equals(cpuMove))
        {
            _result.Append("Draw!");
            OnRoundDrawnAsync();
        }
        //Player had the winning move
        else if(playerMoveData._relations.Any(relation => relation._otherMoveType.Equals(cpuMove)))
        {
            _result.Append("You Won!");
            cpuUIData.EffectName = _movesConfigData.GetEffectName(playerMove, cpuMove);
            OnRoundWonAsync();
        }
        //Otherwise, player had the losing move
        else
        {
            _result.Append("You Lost!");
            playerUIData.EffectName = _movesConfigData.GetEffectName(cpuMove, playerMove);
            OnRoundLostAsync();
        }
        _uiManager.DisplayPlayedMoves(playerUIData, cpuUIData);
        _uiManager.ShowResult(_result.ToString());
    }

    private void OnGameStarted()
    {
        _currentScore = 0;
        _timer.TimerCompleted += OnTimerCompleted;
        StartTimerAsync();
    }

    private async void StartTimerAsync()
    {
        await Task.Delay(1000);
        //Play CPU move
        _currCpuMove = _cpuMoves[(int)Math.Floor(_random.NextDouble() * _cpuMoves.Count)];
        var cpuMoveData = _movesConfigData.GetDataForMove(_currCpuMove);
        var cpuUIData = new SelectedMoveData(_currCpuMove.ToString(), cpuMoveData._sprite);
        _uiManager.ShowCpuMove(cpuUIData);

        //Enable player interaction
        for (int i = 0; i < _playerMovesList.Count; i++)
        {
            _playerMovesList[i].Activate();
        }

        //Start Timer
        _timer.StartTimer(1.5f);
    }

    private void OnTimerCompleted()
    {
        _result.Length = 0;
        _result.Append("You Lost!");
        OnRoundLostAsync();
    }

    private async void OnRoundWonAsync()
    {
        _currentScore++;
        await Task.Delay(3000);
        _uiManager.OnRoundFinished();
        StartTimerAsync();
    }

    private async void OnRoundDrawnAsync()
    {
        await Task.Delay(3000);
        _uiManager.OnRoundFinished();
        StartTimerAsync();
    }

    private async void OnRoundLostAsync()
    {
        for (int i = 0; i < _playerMovesList.Count; i++)
        {
            _playerMovesList[i].Deactivate();
        }
        if (_currentScore > PlayerPrefUtils.GetInt(GlobalConsts.HIGH_SCORE_KEY))
        {
            PlayerPrefUtils.SetInt(GlobalConsts.HIGH_SCORE_KEY, _currentScore);
        }
        _timer.TimerCompleted -= OnTimerCompleted;
        _currentScore = 0;
        _uiManager.ShowResult(_result.ToString());

        await Task.Delay(3000);

        if (this == null)
            return;
        _uiManager.OnRoundFinished();
        _timer.ResetTimer();
        _uiManager.OnRoundLost();
    }
}