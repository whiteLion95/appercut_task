using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "TopDownMovementDataSO", menuName = "ScriptableObject/TopDownControllerDataSO")]
public class TopDownControllerDataSO : ScriptableObject
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private bool _constantSpeed = true;
    [SerializeField] private bool _smoothRotation = true;
    [ShowIf("_smoothRotation")][SerializeField] private float _turnSpeed = 1500f;
    [SerializeField] private bool _alwaysRunning = true;

    public float MoveSpeed => _moveSpeed;
    public bool ConstantSpeed => _constantSpeed;
    public bool SmoothRotation => _smoothRotation;
    public float TurnSpeed => _turnSpeed;
    public bool AlwaysRunning => _alwaysRunning;
}
