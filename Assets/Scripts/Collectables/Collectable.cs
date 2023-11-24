using DG.Tweening;
using Lean.Pool;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(DOTweenAnimation))]
public abstract class Collectable : MonoBehaviour
{
    public static Action<Collectable> OnPickedUp;

    [SerializeField] protected int _coinValue = 2;

    private Collider _collider;
    private DOTweenAnimation _tweenAnim;
    protected bool _isPickedUp;

    public bool IsPickedUp => _isPickedUp;

    protected virtual void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _tweenAnim = GetComponent<DOTweenAnimation>();
    }

    protected virtual void OnEnable()
    {
        _isPickedUp = false;
        Spawn();
    }

    public virtual void PickUp()
    {
        if (!_isPickedUp)
        {
            _isPickedUp = true;
            OnPickedUp?.Invoke(this);
            Despawn();
        }
    }

    public virtual int GetValue()
    {
        return _coinValue;
    }

    protected virtual void Spawn()
    {
        _tweenAnim.DORestartById("spawn");
    }

    public virtual void Despawn()
    {
        _tweenAnim.DOComplete();
        _tweenAnim.DORestartById("despawn");

        if (_tweenAnim.hasOnComplete)
        {
            _tweenAnim.onComplete.AddListener(HandleTweenCompleted);
        }

        void HandleTweenCompleted()
        {
            _tweenAnim.onComplete.RemoveListener(HandleTweenCompleted);
            LeanPool.Despawn(this, delay: _tweenAnim.duration);
        }
    }
}
