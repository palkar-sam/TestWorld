using Model;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuUi : BaseUi
    {
        [SerializeField] private Button startNewBtn;
        [SerializeField] private Button settingsBtn;

        private void Start()
        {
            startNewBtn.onClick.AddListener(OnStartNewGame);
            settingsBtn.onClick.AddListener(OnSettings);
        }

        private void OnStartNewGame()
        {
            Debug.Log("Start New Game Button Clicked");
            EventManager<GameStateModel>.TriggerEvent(GameEvents.ON_SHOW_VIEW, new GameStateModel { GameState = GameConstants.GameState.Options});
        }

        private void OnSettings()
        {
            Debug.Log("Settings Button Clicked");
            EventManager<GameStateModel>.TriggerEvent(GameEvents.ON_SHOW_VIEW, new GameStateModel { GameState = GameConstants.GameState.Settings });
        }

        public override void init<T>(T model)
        {
            throw new NotImplementedException();
        }
    }
}