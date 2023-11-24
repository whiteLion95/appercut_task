using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public ReactiveProperty<int> CoinsCount { get; private set; }

    private void Awake()
    {
        CoinsCount = new ReactiveProperty<int>();
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Collectable collectable))
        {
            CoinsCount.Value += collectable.GetValue();
            collectable.PickUp();
        }
    }
}