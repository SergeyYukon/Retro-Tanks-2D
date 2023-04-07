
using UnityEngine;

namespace Infrastructure.Services
{
    public class InputService
    {
        private readonly InputController _inputController;

        public InputService()
        {
            _inputController = new InputController();
            _inputController.Enable();
        }

        public Vector2 Axis => _inputController.Player.Movement.ReadValue<Vector2>();
        public bool PauseButton => _inputController.Player.Pause.triggered;
    }
}
