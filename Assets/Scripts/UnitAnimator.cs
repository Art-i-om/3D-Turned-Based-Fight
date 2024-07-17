using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Unit))]
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [HideInInspector] public Vector3 targetUnitShootAtPosition;

    [HideInInspector] public Unit targetUnit;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoving += MoveAction_OnStartMoving;
            moveAction.OnStopMoving += MoveAction_OnStopMoving;
        }
        
        if (TryGetComponent<ShootAction>(out ShootAction shootAction))
        {
            shootAction.OnShoot += ShootAction_OnShoot;
        }
        
        if (TryGetComponent<SwordAction>(out SwordAction swordAction))
        {
            swordAction.OnSwordActionStarted += swordAction_OnSwordActionStarted;
            swordAction.OnSwordActionCompleted += swordAction_OnSwordActionCompleted;
        }

        if (TryGetComponent<GrenadeAction>(out GrenadeAction grenadeAction))
        {
            grenadeAction.OnGrenadeTrow += grenadeAction_OnGrenadeTrow;
        }
    }

    private void grenadeAction_OnGrenadeTrow(object sender, EventArgs e)
    {
        animator.SetTrigger("Throw");
    }

    private void swordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
    }

    private void swordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        animator.SetTrigger("SwordSlash");
    }

    private void MoveAction_OnStartMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", true);
    }

    private void MoveAction_OnStopMoving(object sender, EventArgs e)
    {
        animator.SetBool("IsWalking", false);
    }
    
    private void ShootAction_OnShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        animator.SetTrigger("Shoot");
        
        targetUnitShootAtPosition = e.targetUnit.GetWorldPosition();
        targetUnit = e.targetUnit;
    }
}
