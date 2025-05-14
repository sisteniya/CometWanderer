
using System.Collections.Generic;
using Simulation;
using UnityEngine;
using UnityEngine.UI;

public class TestStateM : MonoBehaviour
{
    public GameObject exitButton;
    public GameObject playButton;
    public GameObject resumeButton;
    public GameObject restartButton;
    public List<GameObject> mainMenuButton;

    private Button _exitButton;
    private Button _playButton;
    private Button _resumeButton;
    private Button _restartButton;
    private List<Button> _MMButton = new List<Button>();
    
    
    
    public GameObject UIManager;
    private ScriptedUIManager _UImanager;
    
    public GameObject inputReader;
    private InGameInputReader _inputReader;

    public GameObject playerController;
    private PlayerController _playerController;

    public GameObject timeManager;
    private TimeManager _timeManager;


    public bool easyMode = false;
    
    
    

    private void OnEnable() //events subscription
    {
        InGameInputReader.OnTap += ChangeStateToInGame;
        InGameInputReader.OnPause += ChangeStateToPause;
        
        PlayerController.OnBOOM += ChangeStateToGameOver;
        PlayerCollisionReader.OnAsteroidCrashed += ChangeStateToGameOver;
    }

    private void OnDisable() //events desubscription
    {
        InGameInputReader.OnTap -= ChangeStateToInGame;
        InGameInputReader.OnPause -= ChangeStateToPause;
        
        PlayerController.OnBOOM -= ChangeStateToGameOver;
        PlayerCollisionReader.OnAsteroidCrashed -= ChangeStateToGameOver;
    }
    
    
    

    public void InitializeSelf()
    {
        InitializeButtons();
        InitializeManagers();
        ZeroState();
    }
    
    
    
    void InitializeButtons()
    {
        _exitButton = exitButton.GetComponent<Button>();
        _exitButton.onClick.AddListener(ExitGame);
        
        _playButton = playButton.GetComponent<Button>();
        _playButton.onClick.AddListener(ChangeStateToWaiting);
        
        _resumeButton = resumeButton.GetComponent<Button>();
        _resumeButton.onClick.AddListener(ChangeStateToResume);
        
        _restartButton = restartButton.GetComponent<Button>();
        _restartButton.onClick.AddListener(ChangeStateToWaiting);

        foreach (var mainMenuButton in mainMenuButton)
        {
            var buttonComponent = mainMenuButton.GetComponent<Button>();
            _MMButton.Add(buttonComponent);
        }

        foreach (var _mainMenuButton in _MMButton)
        {
            _mainMenuButton.onClick.AddListener(ChangeStateToMainMenu);
        }
    }
    void InitializeManagers()
    {
        _UImanager = UIManager.GetComponent<ScriptedUIManager>();
        _inputReader = inputReader.GetComponent<InGameInputReader>();
        _timeManager = timeManager.gameObject.GetComponent<TimeManager>();
        _playerController = playerController.GetComponent<PlayerController>();
        
        _UImanager.InitializeSelf();
        _inputReader.InitializeSelf(_playerController);
        _timeManager.InitializeSelf();
        _playerController.InitializeSelf();
    }
    
    
    

    private void ZeroState()
    {
        ControlInput(0); //false, false
        ControlPlayer(3); //false, disable
        ControlTime(0); //stop, stop
        _UImanager.ShowHideMenu(7); //main menu 
    }
    
    
    

    private void ChangeStateToWaiting()
    {
        ControlTime(0); //stop, stop
        ControlPlayer(1); //false, enable player
        
        _UImanager.ShowHideMenu(1); //waiting menu
        
        ControlInput(1); //true, true
    }
    private void ChangeStateToInGame()
    {
        _UImanager.ShowHideMenu(2); //in game menu
        
        ControlInput(2); //true, false
        ControlPlayer(2); //true, enable
        ControlTime(2); //start, start
    }
    private void ChangeStateToPause()
    {
        ControlInput(0); //false, false
        ControlPlayer(0); //false, enable
        ControlTime(0); //stop, stop
        
        
        _UImanager.ShowHideMenu(3); //pause menu
        
    }
    private void ChangeStateToResume()
    {
        ControlInput(2); //true, false
        ControlPlayer(2); //true, enable
        ControlTime(2); //start, start
        
        _UImanager.ShowHideMenu(4); //in game menu
    }
    private void ChangeStateToGameOver()
    {
        ControlInput(0); //false, false
        ControlPlayer(0); //false, disable
        ControlTime(3); //stop, stop, destroy
        
        _UImanager.ShowHideMenu(6); //game over menu
        
    }
    private void ChangeStateToMainMenu()
    {
        ControlInput(0); //false, false
        ControlPlayer(3); //false, disable
        ControlTime(3); //stop, stop, destroy
        
        _UImanager.ShowHideMenu(7); //main menu
    }
    private void ExitGame()
    {
        Application.Quit();
    }




    void ControlInput(int state)
    {
        switch (state)
        {
            case 0: _inputReader.isRunning = false; //before game, game over, pause, to mm;
                    _inputReader.isWaiting = false;
                        break;
            
            case 1: _inputReader.isRunning = true; //waiting state, restart waiting
                    _inputReader.isWaiting = true;
                        break;
            case 2: _inputReader.isRunning = true; //in game, resume
                    _inputReader.isWaiting = false;
                        break;
        }
    }

    void ControlPlayer(int state)
    {
        switch (state)
        {
            case 0: _playerController.isRunning = false; break;
            case 1: _playerController.isRunning = false; 
                    _playerController.EnablePLayer();
                        break;
            case 2: _playerController.isRunning = true; break;
            case 3: _playerController.isRunning = false; //main menu
                    _playerController.DisablePLayer();
                        break;
        }
    }

    void ControlTime(int state)
    {
        switch (state)
        {
            case 0: _timeManager.StopSpawning();   //stop everything
                    _timeManager.StopMovingObjects();
                        break;
            case 2: _timeManager.StartSpawning(); //start everything
                    _timeManager.StartMovingObjects();
                        break;
            case 3: _timeManager.StartSpawning();
                    _timeManager.StopMovingObjects();
                    _timeManager.GameOverProcess();
                        break;
        }
    }
}

