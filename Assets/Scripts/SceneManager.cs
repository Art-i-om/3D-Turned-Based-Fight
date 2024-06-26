using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private void Start()
    {
        Unit.OnAnyUnitDead += Unit_OnAnyUnitDead;
    }

    private void Unit_OnAnyUnitDead(object sender, EventArgs e)
    {
        if (UnitManager.Instance.GetFriendlyUnitList().Count == 0 || UnitManager.Instance.GetEnemyUnitList().Count == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
