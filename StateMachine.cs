using Simulation;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    /*public InGameInputReader _inputReader;
    public TimeManager _timeManager;
    public ScriptedUIManager _scriptedUIManager;
    
    public int currentState; //0 - init, 1 - gameplay, 2 - game over 


    public void InitializeSelf(InGameInputReader input, TimeManager time, ScriptedUIManager UImanager)
    {
        _inputReader = input;
        _timeManager = time;
        _scriptedUIManager = UImanager;
    }

    public void StartGame()
    {
        currentState = 1;
        _timeManager.StartSpawning();
    }
    
    
    private void OnEnable()
    {
        PlayerCollisionReader.OnAsteroidCrashed += HandleCrash;
        PlayerController.OnBOOM += HandleBOOM;
    }
    private void OnDisable()
    {
        PlayerCollisionReader.OnAsteroidCrashed -= HandleCrash;
        PlayerController.OnBOOM -= HandleBOOM;
    }



    void HandleBOOM()
    {
        
    }
    
    void HandleCrash()
    {
        
    }
    void ChangeState(int newState)
    {
        currentState = newState;
    }*/
}



//STATE 0 - BEFORE GAME 
//    spawners don't spawn, player doesn't exist 


//STATE 1 - IN GAME 
//    player spawns, 