using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class BaseUi : MonoBehaviour
    {
        [SerializeField] private GameConstants.GameState _uiState;

        public GameConstants.GameState UiState => _uiState;

        public bool IsVisible => gameObject.activeSelf;

        public void SetVisibility(bool visibility)
        {
            gameObject.SetActive(visibility);
        }

        public virtual void Show()
        {
            SetVisibility(true);
        }

        public virtual void Hide()
        {
            SetVisibility(false);
        }
    }
}