using DG.Tweening;
using Lean.Touch;
using PajamaNinja.Utils;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PajamaNinja.PlayerControllers
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Rigidbody))]
    public class TopDownController : BasePlayerController
    {
        [SerializeField] private TopDownControllerDataSO _config;
        [SerializeField] private bool _showMovementTutor = true;
        [ShowIf("_showMovementTutor")][SerializeField] private CanvasGroup _movementTutor;
        [SerializeField] private CanvasGroup _controlCanvas;
        [SerializeField] private bool _transparentJoystick;

        private Joystick _joystick;
        private CharacterController _charContoller;
        private Vector2 _input;
        private Vector3 _moveDirection;
        private Camera _mainCam;
        private MovementAnimationController _moveAnim;
        private bool _fingerDown;
        private Vector3 _prevDirection;

        public TopDownControllerDataSO Config => _config;

        private void Awake()
        {
            _joystick = _controlCanvas.GetComponentInChildren<Joystick>();
            _charContoller = GetComponent<CharacterController>();
            _mainCam = Camera.main;
            _moveAnim = GetComponentInChildren<MovementAnimationController>();
            GetComponent<Rigidbody>().isKinematic = true;

            OnStartedMoving += HandleStartMoving;
            OnStoppedMoving += HandleStoppedMoving;
            LeanTouch.OnFingerDown += (f) => _fingerDown = true;
            LeanTouch.OnFingerUp += (f) => _fingerDown = false;
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            ReadInput();
            Move();
            Rotate();
        }

        private void Init()
        {
            if (_transparentJoystick)
                _controlCanvas.alpha = 0f;

            if (_movementTutor)
            {
                if (_showMovementTutor)
                    ShowMovementTutor();
                else
                    _movementTutor.gameObject.SetActive(false);
            }
        }

        public override void LockInput(bool locked)
        {
            base.LockInput(locked);
            _joystick.gameObject.SetActive(!locked);
        }

        public void ShowMovementTutor()
        {
            _movementTutor.alpha = 0f;
            _movementTutor.gameObject.SetActive(true);
            _movementTutor.DOFade(1, 0.2f).SetAutoKill();
        }

        public void HideMovementTutor()
        {
            _movementTutor.DOComplete();
            _movementTutor.DOFade(0, 0.2f).OnComplete(() =>
            {
                _movementTutor.gameObject.SetActive(false);
            }).SetAutoKill();
        }

        private void ReadInput()
        {
            _input = new Vector2(_joystick.Horizontal, _joystick.Vertical);
#if UNITY_EDITOR
            if (_input == Vector2.zero)
                _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
#endif
            _moveDirection = _mainCam.transform.TransformDirection(new Vector3(_input.x, 0f, _input.y));
            _moveDirection.y = 0f;
        }

        private void Move()
        {
            if (_fingerDown)
            {
                if (_moveDirection != Vector3.zero)
                {
                    if (!IsMoving)
                    {
                        IsMoving = true;
                        OnStartedMoving?.Invoke();
                    }
                }
                else
                {
                    _moveDirection = _prevDirection;
                }

                if (_config.ConstantSpeed)
                    _moveDirection.Normalize();

                _charContoller.SimpleMove(_moveDirection * _config.MoveSpeed);
                _prevDirection = _moveDirection;
            }
            else if (IsMoving && !_fingerDown)
            {
                _prevDirection = Vector3.zero;
                IsMoving = false;
                OnStoppedMoving?.Invoke();
            }
        }

        private void Rotate()
        {
            if (_moveDirection != Vector3.zero)
            {
                if (_config.SmoothRotation)
                    transform.SmoothLookAt(_moveDirection, Vector3.up, _config.TurnSpeed);
                else
                    transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            }
        }

        private void HandleStartMoving()
        {
            if (_movementTutor && _movementTutor.gameObject.activeSelf)
                HideMovementTutor();

            if (_moveAnim)
            {
                if (_config.AlwaysRunning)
                    _moveAnim.SetRun(true);
                else
                    _moveAnim.SetWalk(true);
            }
        }

        private void HandleStoppedMoving()
        {
            if (_moveAnim)
            {
                if (_config.AlwaysRunning)
                    _moveAnim.SetRun(false);
                else
                    _moveAnim.SetWalk(false);
            }
        }
    }
}