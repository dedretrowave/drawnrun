using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class Input
    {
        private Controls _controls;

        public event Action<Vector2> MoveStarted;
        public event Action<Vector2> MovePerformed;
        public event Action<Vector2> MoveCanceled;
        public event Action ClickStarted;
        public event Action ClickPerformed;
        public event Action ClickCanceled;

        public Input()
        {
            _controls = new();
            _controls.Enable();

            _controls.Pointer.Move.started += OnMoveStarted;
            _controls.Pointer.Move.performed += OnMovePerformed;
            _controls.Pointer.Move.canceled += OnMoveCanceled;

            _controls.Pointer.Click.started += OnClickStarted;
            _controls.Pointer.Click.performed += OnClickPerformed;
            _controls.Pointer.Click.canceled += OnClickCanceled;
        }

        public void Disable()
        {
            _controls.Disable();
        }

        private void OnMoveStarted(InputAction.CallbackContext context)
        {
            MoveStarted?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MovePerformed?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveCanceled?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnClickStarted(InputAction.CallbackContext context)
        {
            ClickStarted?.Invoke();
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            ClickPerformed?.Invoke();
        }

        private void OnClickCanceled(InputAction.CallbackContext context)
        {
            ClickCanceled?.Invoke();
        }
    }
}