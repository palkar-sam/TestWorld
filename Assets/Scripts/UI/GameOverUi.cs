using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class GameOverUi : BaseUi
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text clickCountText;
        [SerializeField] private Button claimButton;

        private BoardCompleteModel _boardCompleteModel;

        public override void init<T>(T model)
        {
            _boardCompleteModel = model as BoardCompleteModel;

            scoreText.text = $"Total Items Collected : {_boardCompleteModel.CollectedScore}";
            clickCountText.text = $"Total Clikcs : {_boardCompleteModel.ClicksScore}";
        }

        private void OnClaimButton()
        {
            EventManager<GameStateModel>.TriggerEvent(GameEvents.ON_SHOW_VIEW, new GameStateModel() {
                GameState = GameConstants.GameState.MainMenu
            });
        }

        private void Start()
        {
            claimButton.onClick.AddListener(OnClaimButton);
        }
    }
}