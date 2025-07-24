using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOptionsUi : BaseUi
    {
        [SerializeField] private List<OptionButtonsUi> optionButtons;

        private void Start()
        {
            if (optionButtons == null || optionButtons.Count == 0)
            {
                Debug.LogError("Option buttons are not assigned in the inspector.");
                return;
            }
            foreach (var button in optionButtons)
            {
                button.OnOptionSelected += HandleOptionSelected;
            }
        }

        private void HandleOptionSelected(GameOptions option)
        {
            Debug.Log($"Option selected: {option}");
            EventManager<GameOptionsModel>.TriggerEvent(GameEvents.ON_OPTION_SELECTED, new GameOptionsModel
            {
                GameOptionsTypes = option,
                GameState = GameConstants.GameState.Game
            });
        }

        private void OnDestroy()
        {
            if (optionButtons != null)
            {
                foreach (var button in optionButtons)
                {
                    button.OnOptionSelected -= HandleOptionSelected;
                }
            }
        }
    }
}