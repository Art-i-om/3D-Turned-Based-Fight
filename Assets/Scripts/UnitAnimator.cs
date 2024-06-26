using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [HideInInspector] public Vector3 targetUnitShootAtPosition;
    
    [SerializeField] private Transform bowTransform;
    [SerializeField] private Transform leftSwordTransform;
    [SerializeField] private Transform rightSwordTransform;
    
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
    }

    private void Start()
    {
        EquipBow();
    }

    private void swordAction_OnSwordActionCompleted(object sender, EventArgs e)
    {
        EquipBow();
    }

    private void swordAction_OnSwordActionStarted(object sender, EventArgs e)
    {
        EquipSword();
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

    private void EquipSword()
    {
        leftSwordTransform.gameObject.SetActive(true);
        rightSwordTransform.gameObject.SetActive(true);
        bowTransform.gameObject.SetActive(false);
    }

    private void EquipBow()
    {
        leftSwordTransform.gameObject.SetActive(false);
        rightSwordTransform.gameObject.SetActive(false);
        bowTransform.gameObject.SetActive(true);
    }
}
