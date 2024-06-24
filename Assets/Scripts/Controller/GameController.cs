using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    InGame,
    Pause,
    Setting,
    SelectLevel,
    SelectModel
}
public class GameController : Singleton<GameController>
{
    public event EventHandler OnGameStateUpdated;
    public GameState GameState => gameState;


    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private SettingUI settingUI;
    [SerializeField] private PlayingUI playingUI;
    [SerializeField] private PauseUI pauseUI;
    [SerializeField] private SelectLevelUI selectLevelUI;
    [SerializeField] private SelectModelUI selectModelUI;


    private GameState gameState;

    private new void Awake()
    {
        UIController.Instance.ClearStackAndSetInitialPage(mainMenuUI);
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        if (UIController.Instance.InitialPage == mainMenuUI && UIController.Instance.IsOnTop(mainMenuUI))
        {
            UIController.Instance.PushAndShowWithoutHidingPrevious(settingUI);
        }
        else if (UIController.Instance.InitialPage == playingUI && UIController.Instance.IsOnTop(playingUI))
        {
            UIController.Instance.PushAndShowWithoutHidingPrevious(pauseUI);
        }
        else
        {
            UIController.Instance.PopAndReturn();
        }
    }

    public void UpdateGameState(GameState state)
    {
        gameState = state;

        switch (state)
        {
            default:
            case GameState.MainMenu:
                Handle_MainMenuState();
                break;
            case GameState.InGame:
                Handle_PlayingState();
                break;
            case GameState.Pause:
                Handle_PauseState();
                break;
            case GameState.Setting:
                break;
            case GameState.SelectLevel:
                break;
            case GameState.SelectModel:
                break;
        }

        OnGameStateUpdated?.Invoke(this, EventArgs.Empty);
    }

    private void Handle_PauseState()
    {
        //stop the game
        FreezeTime();
    }

    private void Handle_MainMenuState()
    {
        UnFreezeTime();
    }

    private void Handle_PlayingState()
    {
        UnFreezeTime();
    }

    // freeze the time
    public void FreezeTime()
    {
        Time.timeScale = 0;
    }

    //unfreeze the time
    public void UnFreezeTime()
    {
        Time.timeScale = 1;
    }


}
