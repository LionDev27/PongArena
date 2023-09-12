using System;
using DG.Tweening;
using UnityEngine;

public class CounterAnimation : MonoBehaviour
{
    [SerializeField] private UIScaleAnimation[] _animationSequence;

    public static Action OnAnimationEnd;
    public static CounterAnimation Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    public Sequence CounterAnim(bool wait)
    {
        var seq = DOTween.Sequence();
        if (wait)
            seq.AppendInterval(1);
        for (var i = 0; i < _animationSequence.Length; i++)
        {
            var scaleAnimation = _animationSequence[i];
            scaleAnimation.t.localScale = Vector3.zero;
            seq.Append(scaleAnimation.t.DOScale(Vector3.one, scaleAnimation.seconds)
                .SetEase(Ease.OutBounce));
            if (i == _animationSequence.Length - 1)
                seq.Join(scaleAnimation.t.DOShakeRotation(scaleAnimation.seconds));
            seq.Append(scaleAnimation.t.DOScale(Vector3.zero, 0.05f));
            seq.Join(scaleAnimation.t.DORotate(Vector3.zero, 0.05f));
        }

        seq.onComplete += () => UIController.Instance.ShowGameMenu();

        return seq;
    }
    
    private void StartFirstCounterAnimation()
    {
        var seq = CounterAnim(true);

        seq.onComplete += () => OnAnimationEnd?.Invoke();

        seq.Play();
    }

    private void OnEnable()
    {
        NetworkInitializer.OnLobbyCompleted += StartFirstCounterAnimation;
        ScoreController.OnPlayerScored += StartFirstCounterAnimation;
    }

    private void OnDisable()
    {
        NetworkInitializer.OnLobbyCompleted -= StartFirstCounterAnimation;
        ScoreController.OnPlayerScored -= StartFirstCounterAnimation;
    }
}

[Serializable]
public struct UIScaleAnimation
{
    public Transform t;
    public float seconds;
}