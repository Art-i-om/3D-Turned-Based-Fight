using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsReload : MonoBehaviour
{
    public static SkillsReload Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one SkillsReload! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateSkillsReloadUI(List<ActionButtonUI> actionButtonUIList)
    {
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedAction.StartReloading();

        if (selectedAction.GetReloadLeft() <= 0)
        {
            return;
        }
        
        foreach (var actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateReloadVisual();
        }
    }
}
