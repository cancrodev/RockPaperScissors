using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameScreenUIScript : MonoBehaviour, IScreenView
{
    [SerializeField]
    private SelectedMoveUI _playerSelectedMove;

    [SerializeField]
    private SelectedMoveUI _cpuSelectedMove;

    [SerializeField]
    private CanvasGroup _resultCanvasGroup;

    [SerializeField]
    private TextMeshProUGUI _resultTxt;

    private GameObject _thisObject;

    public void Init()
    {
        _thisObject = gameObject;
        _playerSelectedMove.Init();
        _cpuSelectedMove.Init();
    }

    public void Open()
    {
        _thisObject.SetActive(true);
    }

    public void Close()
    {
        _thisObject.SetActive(false);
    }

    public void DisplayRoundMoves(SelectedMoveData playerMoveData, SelectedMoveData cpuMoveData)
    {
        _playerSelectedMove.DisplaySelectedMove(playerMoveData);
        _cpuSelectedMove.DisplaySelectedMove(cpuMoveData);
    }

    public void ShowResult(string result)
    {
        _resultTxt.text = result;
        _resultCanvasGroup.DOFade(1.0f, 0.45f);
    }

    public void DisplayCpuMove(SelectedMoveData cpuMoveData)
    {
        _cpuSelectedMove.DisplaySelectedMove(cpuMoveData);
    }

    public void HideSelectedMovesUI()
    {
        _playerSelectedMove.HideObject();
        _cpuSelectedMove.HideObject();
        _resultCanvasGroup.DOFade(0, 0.3f);
    }
}