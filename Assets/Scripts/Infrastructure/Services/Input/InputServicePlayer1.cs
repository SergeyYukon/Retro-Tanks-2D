
using UnityEngine;

namespace Infrastructure.Services
{
    public class InputServicePlayer1 : IInputService
    {
        private readonly InputController _inputController;

        public InputServicePlayer1(InputController inputController)
        {
            _inputController = inputController;
        }

        public Vector2 Axis => _inputController.Player.Movement.ReadValue<Vector2>();
        public bool PauseButton => _inputController.Player.Pause.triggered;
    }
}
