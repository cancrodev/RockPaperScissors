using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedMoveUI : MonoBehaviour
{
    [SerializeField]
    private Image _displayImage;

    [SerializeField]
    private TextMeshProUGUI _nametxt;

    [SerializeField]
    private TextMeshProUGUI _effectTxt;

    private GameObject _thisObject;
    private Transform _effectTransform;
    private Vector3 _startScale = new Vector3(100, 100, 100);
    private Tween _scaleTween;

    public void Init()
    {
        _thisObject = gameObject;
        _effectTransform = _effectTxt.GetComponent<Transform>();
    }

    public void DisplaySelectedMove(SelectedMoveData moveData)
    {
        _nametxt.text = moveData.Name;
        _displayImage.sprite = moveData.Sprite;
        _effectTxt.text = moveData.EffectName;
        _effectTransform.localScale = _startScale;
        _thisObject.SetActive(true);
        if(_scaleTween != null)
        {
            _scaleTween.Restart();
        }
        else
        {
            _scaleTween = _effectTransform.DOScale(Vector3.one, 0.45f).SetAutoKill(false);
        }
    }

    public void HideObject()
    {
        _thisObject.SetActive(false);
        _effectTxt.text = "";
    }
}