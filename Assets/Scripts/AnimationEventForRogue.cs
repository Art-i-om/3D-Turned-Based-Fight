using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationEventForRogueShootAction : MonoBehaviour
{
    [Header("Unit Settings")]
    [SerializeField] private UnitAnimator unitAnimator;
    [SerializeField] private Unit unit;
    
    [Header("Shoot Action")]
    [SerializeField] private Transform bulletProjectilePrefab;
    [SerializeField] private Transform shootPointTransform;

    [Header("Grenade Action")]
    [SerializeField] private Transform grenadeProjectilePrefab;
    [SerializeField] private Transform throwPositionTransform;
    
    public void StartShoot()
    {
        Transform bulletProjectileTransform = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);
        
        BulletProjectile bulletProjectile = bulletProjectileTransform.GetComponent<BulletProjectile>();
        
        bulletProjectile.SetDamage(unit.GetAction<ShootAction>().GetDamage());

        unitAnimator.targetUnitShootAtPosition.y = shootPointTransform.position.y;
        
        bulletProjectile.Setup(unitAnimator.targetUnitShootAtPosition, unitAnimator.targetUnit);
    }

    public void ThrowGrenade()
    {
        GrenadeAction grenadeAction = unit.GetAction<GrenadeAction>();

        Vector3 throwPosition = new Vector3
        (
            throwPositionTransform.position.x,
            throwPositionTransform.position.y,
            throwPositionTransform.position.z
        );
        
        Transform grenadeProjectileTransform = Instantiate(grenadeProjectilePrefab, throwPosition, Quaternion.identity);
        GrenadeProjectile grenadeProjectile = grenadeProjectileTransform.GetComponent<GrenadeProjectile>();
        grenadeProjectile.Setup(grenadeAction.gridPosition, grenadeAction.OnGrenadeBehaviourComplete);
    }
    
    public void SwordAttack()
    {
        SwordAction swordAction = unit.GetAction<SwordAction>();
        
        swordAction.targetUnit.Damage(unit.GetAction<SwordAction>().GetDamage());
    }
}
