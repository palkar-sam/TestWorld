using Assets.Scripts;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static readonly object lockObj = new object();

    public static GameManager Instance => _instance;

    [SerializeField] private List<BaseUi> gameUi;
    [SerializeField] private Button backBtn;

    public GameOptions CurrentGameOptions => _currentSelectedGameOption;

    private GameConstants.GameState _currentGameState = GameConstants.GameState.MainMenu;
    private BaseUi _currentUiComponent;
    private GameOptions _currentSelectedGameOption = GameOptions.NONE;

    private void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Time.fixedDeltaTime = 1.0f / Application.targetFrameRate;

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else if (_instance == null)
        {
            lock (lockObj)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void Start()
    {
        _currentUiComponent = gameUi.Find(uiItem => uiItem.UiState == _currentGameState);
        _currentUiComponent?.Show();

        UpdateBackButtonState();

        backBtn.onClick.RemoveAllListeners();
        backBtn.onClick.AddListener(() =>
        {
            Debug.Log("Back Button Clicked");
            if (_currentGameState == GameConstants.GameState.MainMenu)
            {
                Debug.Log("Already at Main Menu, no action taken.");
                return;
            }

            GameStateModel stateMOdel = new GameStateModel
            {
                GameState = GameConstants.GameState.MainMenu
            };

            if (_currentGameState == GameConstants.GameState.Game)
                stateMOdel.GameState = GameConstants.GameState.Options;

            OnShowView(stateMOdel);
        });

        EventManager<GameStateModel>.StartListening(GameEvents.ON_SHOW_VIEW, OnShowView);
        EventManager<GameOptionsModel>.StartListening(GameEvents.ON_OPTION_SELECTED, OnShowView);
    }

    private void OnShowView(GameStateModel model)
    {
        Debug.Log("OnShowView called with state: " + model.GameState);
        BaseUi uiComponent = gameUi.Find(uiItem => uiItem.UiState == model.GameState);
        if (uiComponent != null)
        {
            _currentUiComponent.Hide();
            uiComponent.Show();
            _currentGameState = model.GameState;
            _currentUiComponent = uiComponent;
            UpdateBackButtonState();
        }
        else
        {
            Debug.LogWarning("No UI component found for state: " + model.GameState);
        }
    }

    private void OnShowView(GameOptionsModel options)
    {
        _currentSelectedGameOption = options.GameOptionsTypes;
        ShowUi(options.GameState);
    }

    private void ShowUi(GameConstants.GameState state)
    {
        BaseUi uiComponent = gameUi.Find(uiItem => uiItem.UiState == state);
        if (uiComponent != null)
        {
            _currentUiComponent.Hide();
            uiComponent.Show();
            _currentGameState = state;
            _currentUiComponent = uiComponent;
            UpdateBackButtonState();
        }
        else
        {
            Debug.LogWarning("No UI component found for state: " + state);
        }
    }


    private void UpdateBackButtonState()
    {
        if (_currentGameState == GameConstants.GameState.MainMenu)
        {
            backBtn.gameObject.SetActive(false);
        }
        else
        {
            backBtn.gameObject.SetActive(true);
        }
    }
}
