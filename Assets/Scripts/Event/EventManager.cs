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

    private void Start() {
        MainMenuEvent += SubsribeAllEvent;
        MainMenuEvent();
    }

    void SubsribeAllEvent(){
        #region In Game
        InGameEvent += PlayerAnimationController.Instance.RunAnimation;    
        InGameEvent += () => StateManager.Instance.State = State.InGame;
        #endregion

        #region Game Over
            
        #endregion

        #region EndGame
            
        #endregion
    }
}
