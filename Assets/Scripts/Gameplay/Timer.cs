using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Timer : MonoBehaviour
{
    public event Action TimerCompleted;

    [SerializeField]
    private Slider _slider;

    private Tween _timerTween;

    public void StartTimer(float duration)
    {
        _slider.value = 1.0f;
        if(_timerTween == null)
        {
            _timerTween = _slider.DOValue(0.0f, duration).SetAutoKill(false).OnComplete(() => TimerCompleted?.Invoke());
        }
        else
        {
            _timerTween.Restart();
        }
    }

    public void StopTimer()
    {
        _timerTween?.Pause();
    }

    public void ResetTimer()
    {
        _slider.value = 1;
    }
}