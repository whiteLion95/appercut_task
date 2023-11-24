using UnityEngine;

public class MovementAnimationController : MonoBehaviour
{
    [SerializeField] private string _walkParameter = "isWalking";
    [SerializeField] private string _runParameter = "isRunning";

    private Animator _anim;
    private int _walkHash;
    private int _runHash;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _walkHash = Animator.StringToHash(_walkParameter);
        _runHash = Animator.StringToHash(_runParameter);
    }

    public void SetWalk(bool value)
    {
        _anim.SetBool(_walkHash, value);
    }

    public void SetRun(bool value)
    {
        _anim.SetBool(_runHash, value);
    }
}
