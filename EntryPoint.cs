using System.Collections;
using System.Collections.Generic;
using Simulation;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public GameObject stateMachine;
    public GameObject inputReader;
    public GameObject timeManager;
    public GameObject UIManager;
    public GameObject player;
    public GameObject asteroidSpawnerFolder;
    public GameObject coinSpawnerFolder;
    
    private TestStateM stateMachineScript;
    private InGameInputReader inputReaderScript;
    private TimeManager timeManagerScript;
    private ScriptedUIManager uiManagerScript;
    private PlayerController playerControllerScript;


    
    
    
    void Start()
    {
        stateMachineScript = stateMachine.GetComponent<TestStateM>();
        stateMachineScript.InitializeSelf();
       
    }

}
