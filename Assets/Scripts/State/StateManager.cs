using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State{  
    MainMenu,
    InGame,
    GameOver,
    EndGame

}

public class StateManager : Singleton<StateManager>
{
    private State _state;

    public State State { get => _state; set => _state = value; }

    private void Start() {
        State = State.MainMenu;
    }
}
