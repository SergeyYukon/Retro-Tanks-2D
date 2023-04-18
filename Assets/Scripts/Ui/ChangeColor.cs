using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Data
{
    public class ChangeColor : MonoBehaviour
    {
        [Header("Player 1")]
        [SerializeField] private Image player1Image;
        [SerializeField] private Slider sliderPlayer1R;
        [SerializeField] private Slider sliderPlayer1G;
        [SerializeField] private Slider sliderPlayer1B;
        [Header("Player 2")]
        [SerializeField] private Image player2Image;
        [SerializeField] private Slider sliderPlayer2R;
        [SerializeField] private Slider sliderPlayer2G;
        [SerializeField] private Slider sliderPlayer2B;

        private GameData _gameData;
        private SaveLoadService _saveLoadService;

        public void Construct(GameData gameData, SaveLoadService saveLoadService)
        {
            _gameData = gameData;
            _saveLoadService = saveLoadService;

            SetColors();
        }

        private void Update()
        {
            player1Image.color = new Color(sliderPlayer1R.value, sliderPlayer1G.value, sliderPlayer1B.value, 1f);
            player2Image.color = new Color(sliderPlayer2R.value, sliderPlayer2G.value, sliderPlayer2B.value, 1f);
        }

        public void ColorChanged()
        {
            _gameData.Player1Color = player1Image.color;
            _gameData.Player2Color = player2Image.color;
            _gameData.CustomColor = true;
            _saveLoadService.SaveProgress(_gameData, SaveLoadKeys.GameDataPlayer1Key);
        }

        private void SetColors()
        {
            if (!_gameData.CustomColor)
            {
                Color _defaultPlayer1Color = new (1, 0, 0.25f, 1);
                Color _defaultPlayer2Color = new (1, 0, 0.75f, 1);

                player1Image.color = _defaultPlayer1Color;
                player2Image.color = _defaultPlayer2Color;
                sliderPlayer1R.value = _defaultPlayer1Color.r;
                sliderPlayer1G.value = _defaultPlayer1Color.g;
                sliderPlayer1B.value = _defaultPlayer1Color.b;
                sliderPlayer2R.value = _defaultPlayer2Color.r;
                sliderPlayer2G.value = _defaultPlayer2Color.g;
                sliderPlayer2B.value = _defaultPlayer2Color.b;
                _gameData.Player1Color = player1Image.color;
                _gameData.Player2Color = player2Image.color;
            }
            else
            {
                player1Image.color = _gameData.Player1Color;
                player2Image.color = _gameData.Player2Color;
                sliderPlayer1R.value = player1Image.color.r;
                sliderPlayer1G.value = player1Image.color.g;
                sliderPlayer1B.value = player1Image.color.b;
                sliderPlayer2R.value = player2Image.color.r;
                sliderPlayer2G.value = player2Image.color.g;
                sliderPlayer2B.value = player2Image.color.b;
            }
        }
    }
}
