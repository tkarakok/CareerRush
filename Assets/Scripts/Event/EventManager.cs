using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void StateActions();
    public StateActions MainMenuEvent;
    public StateActions InGameEvent;
    public StateActions EndGameEvent;
    public StateActions GameOverEvent;

    public delegate void Money();
    public Money MoneyEvent;

    private void Start() {
        MainMenuEvent += SubsribeAllEvent;
        MainMenuEvent();
    }

    void SubsribeAllEvent(){
        #region In Game
        InGameEvent += PlayerAnimationController.Instance.RunAnimation;    
        InGameEvent += () => StateManager.Instance.State = State.InGame;
        InGameEvent += UiManager.Instance.InGameTextUpdate;
        #endregion

        #region Game Over
        GameOverEvent += PlayerAnimationController.Instance.GameOverAnimation;
        GameOverEvent += () => StateManager.Instance.State = State.GameOver;
        GameOverEvent += UiManager.Instance.GameOver;
        #endregion

        #region Finish
        EndGameEvent += GameManager.Instance.Finish;
        
        #endregion

        #region Money Event
        MoneyEvent += UiManager.Instance.InGameTextUpdate;
        #endregion

    }
}
