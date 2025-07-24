using Model;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class OptionButtonsUi : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameOptions optionName;

        public event Action<GameOptions> OnOptionSelected;

        private void Start()
        {
            if (button == null)
            {
                Debug.LogError("Button is not assigned in the inspector.");
                return;
            }
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Debug.Log($"Button clicked for option: {optionName}");
            OnOptionSelected?.Invoke(optionName);
        }

    }
}