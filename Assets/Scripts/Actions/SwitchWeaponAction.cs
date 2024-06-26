using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponAction : BaseAction
{
    public override string GetActionName()
    {
        return "Switch";
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        throw new NotImplementedException();
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        throw new NotImplementedException();
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {
        return new EnemyAIAction()
        {
            gridPosition = gridPosition,
            actionValue = 0
        };
    }
}
