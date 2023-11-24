using Cysharp.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [Inject]
    private Player _player;

    private void Start()
    {
        _player.CoinsCount.Subscribe(coins =>
        {
            _text.text = coins.ToString();
        });
    }
}
