using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitAnimator))]
public class WeaponChanger : MonoBehaviour
{
    [SerializeField] private List<Transform> swordTransforms;
    [SerializeField] private List<Transform> bowTransforms;

    private SwordAction _swordAction;
    
    private bool rangeWeaponEquiped = true;

    private void Awake()
    {
        _swordAction = GetComponent<SwordAction>();
        
        _swordAction.OnSwordActionStarted += swordAction_OnSwordActionStarted;
        _swordAction.OnSwordActionCompleted += swordAction_OnSwordActionCompleted;
    }
    

    private void swordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
        rangeWeaponEquiped = !rangeWeaponEquiped;
        ChangeWeapon();
    }

    private void swordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        rangeWeaponEquiped = !rangeWeaponEquiped;
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        foreach (var sword in swordTransforms)
        {
            sword.gameObject.SetActive(!rangeWeaponEquiped);
        }
        
        foreach (var bow in bowTransforms)
        {
            bow.gameObject.SetActive(rangeWeaponEquiped);
        }
    }
}
