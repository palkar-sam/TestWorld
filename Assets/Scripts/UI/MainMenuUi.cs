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
        [SerializeField] private Button resumeBtn;

        private void Start()
        {
            startNewBtn.onClick.AddListener(OnStartNewGame);
            settingsBtn.onClick.AddListener(OnSettings);
            resumeBtn.onClick.AddListener(OnResume);
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

        private void OnResume()
        {
            Debug.Log("Resume Button Clicked");
            EventManager<GameStateModel>.TriggerEvent(GameEvents.ON_SHOW_VIEW, new GameStateModel { GameState = GameConstants.GameState.Game });
        }
        public override void init<T>(T model)
        {
            throw new NotImplementedException();
        }
    }
}