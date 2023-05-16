using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainScreenUIScript : MonoBehaviour, IScreenView
{
    public event Action PlayClicked;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private TextMeshProUGUI _highScoreTxt;

    [SerializeField]
    private Image _overlay;

    private GameObject _thisObject;

    private StringBuilder _strBuilder = new StringBuilder();
    private const string HIGH_SCORE_MSG = "High Score: ";
    private Color _defaultOverlayColor;

    public void Init()
    {
        _playButton.onClick.AddListener(() => OnPlayButtonClicked());
        _thisObject = gameObject;
        _defaultOverlayColor = _overlay.color;
    }

    public void Open()
    {
        _overlay.raycastTarget = true;
        _overlay.color = _defaultOverlayColor;
        _thisObject.SetActive(true);
        UpdateHighScore();
        _overlay.DOFade(0.0f, 0.45f).OnComplete(() =>
        {
            _overlay.raycastTarget = false;
        });
    }

    public void Close()
    {
        _thisObject.SetActive(false);
    }

    private void OnPlayButtonClicked()
    {
        PlayClicked?.Invoke();
    }

    private void UpdateHighScore()
    {
        _strBuilder.Length = 0;
        _strBuilder.Append(HIGH_SCORE_MSG);
        _strBuilder.Append(PlayerPrefUtils.GetInt(GlobalConsts.HIGH_SCORE_KEY));
        _highScoreTxt.text = _strBuilder.ToString();
    }
}