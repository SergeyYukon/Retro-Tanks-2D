using UnityEngine;

namespace Infrastructure.Services
{
    public class InputServicePlayer2 : IInputService
    {
        private readonly InputController _inputController;

        public InputServicePlayer2(InputController inputController)
        {
            _inputController = inputController;
        }

        public Vector2 Axis => _inputController.Player2.Movement.ReadValue<Vector2>();
    }
}
