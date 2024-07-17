using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVFXPrefab;

    private Vector3 targetPosition;
    private Unit targetUnit;

    private int damage;
    
    public void Setup(Vector3 targetPosition, Unit targetUnit)
    {
        this.targetPosition = targetPosition;
        this.targetUnit = targetUnit;
    }

    private void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);
        
        float moveSpeed = 200f;
        transform.position += moveDir * (moveSpeed * Time.deltaTime);

        float DistanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceBeforeMoving < DistanceAfterMoving)
        {
            transform.position = targetPosition;
            
            trailRenderer.transform.parent = null;
            
            Destroy(gameObject);
            
            targetUnit.Damage(damage);

            Instantiate(bulletHitVFXPrefab, targetPosition, Quaternion.identity);
        }
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }
}
