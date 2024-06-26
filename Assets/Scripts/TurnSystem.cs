using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    
    private int turnNumber = 1;

    public event EventHandler OnTurnChanged;

    private bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one TurnSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    public void NextTurn()
    {
        if (UnitActionSystem.Instance.GetBusy())
        {
            return;
        }

        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}
