using System;
using UnityEngine;

namespace PajamaNinja.PlayerControllers
{
    public abstract class BasePlayerController : MonoBehaviour
    {
        public Action OnStartedMoving;
        public Action OnStoppedMoving;
        public Action<bool> OnLockInput;

        public bool IsMoving { get; protected set; }
        public bool IsInputLocked { get; protected set; }

        public virtual void LockInput(bool locked)
        {
            IsInputLocked = locked;
            OnLockInput?.Invoke(locked);
        }
    }
}