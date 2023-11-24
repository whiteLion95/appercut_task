public class Collectable_3 : Collectable
{
    private CollectablesManager _collectablesManager;

    private void Start()
    {
        _collectablesManager = CollectablesManager.Instance;
    }

    protected override void OnEnable()
    {
        _coinValue = 0;
        base.OnEnable();
    }

    public override int GetValue()
    {
        if (_collectablesManager.LastCollected && !_collectablesManager.LastCollected.Equals(this) && _collectablesManager.LastCollected.IsPickedUp)
        {
            _coinValue = CollectablesManager.Instance.LastCollected.GetValue();
            return _coinValue;
        }
        else
            return _coinValue;
    }
}
