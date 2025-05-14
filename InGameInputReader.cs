using System;
using Simulation;
using UnityEngine;

public class InGameInputReader : MonoBehaviour
{
    PlayerController _playerController;

    public bool isRunning = false;
    public bool isWaiting = false;


    public static Action OnTap;
    public static Action OnPause;
    
    
    
    public void InitializeSelf(PlayerController player)
    {
        _playerController = player;
    }


    private void Update()
    {
        if (!isRunning) { return; }
        
        if (isWaiting) { CheckTap(); return; }
        
        SendCursorPosition();
        CheckKeyboardPress();
    }

  



    public void CheckTap()
    {
        if (Input.GetMouseButton(0))
        {
            OnTap?.Invoke();
            isWaiting = false;
        }
    }
    
    public void SendCursorPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _playerController.direction = mousePosition;
    }
    
    public void CheckKeyboardPress()
    {
        if (Input.anyKeyDown)
        {
            OnPause?.Invoke();
        }
    }
}