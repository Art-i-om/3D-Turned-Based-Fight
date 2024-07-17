using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAction : BaseAction
{
    public event EventHandler OnGrenadeTrow;
    
    [SerializeField] private Transform grenadeProjectilePrefab;

    [SerializeField] private int maxThrowDistance = 7;

    [HideInInspector] public GridPosition gridPosition;
    
    private enum State
    {
        Aiming,
        Throwing,
        Cooloff
    }
    
    private State state;
    private float stateTimer;

    private Vector3 ThrowPosition;
    
    private bool canThrowGrenade;
    
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        stateTimer -= Time.deltaTime;
        
        switch (state)
        {
            case State.Aiming:
                Vector3 aimDir = (ThrowPosition - unit.GetWorldPosition()).normalized;
                float rotateSpeed = 20f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.Throwing:
                if (canThrowGrenade)
                {
                    Throw();
                    canThrowGrenade = false;
                }
                break;
            case State.Cooloff:
                break;
        }
        
        if (stateTimer <= 0f)
        {
            NextState();
        }
    }
    
    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Throwing;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Throwing:
                state = State.Cooloff;
                float coolOffStateTime = 0.5f;
                stateTimer = coolOffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }
    
    private void Throw()
    {
        OnGrenadeTrow?.Invoke(this, EventArgs.Empty);
    }

    public override string GetActionName()
    {
        return "Grenade";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        ActionStart(onActionComplete);
        
        this.gridPosition = gridPosition;
        
        ThrowPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        
        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;

        canThrowGrenade = true;
        
        StartReloading();
    }

    public void OnGrenadeBehaviourComplete()
    {
        ActionComplete();
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        
        for (int x = -maxThrowDistance; x <= maxThrowDistance; x++)
        {
            for (int z = -maxThrowDistance; z <= maxThrowDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z); 
                if (testDistance > maxThrowDistance)
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }
        
        return validGridPositionList;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction()
        {
            gridPosition = gridPosition,
            actionValue = 0,
        };
    }
    
    public override int ReloadTime()
    {
        return 2;
    }
}
