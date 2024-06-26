using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventForRogueShootAction : MonoBehaviour
{
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;
    [SerializeField] private UnitAnimator unitAnimator;
    
    public void StartShoot()
    {
        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();

        unitAnimator.targetUnitShootAtPosition.y = shootPointTransform.position.y;
        
        bulletProjectile.Setup(unitAnimator.targetUnitShootAtPosition, unitAnimator.targetUnit);
        
        // unitAnimator.targetUnit.Damage(40);
    }
}
