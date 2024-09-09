using System.Collections.Generic;
using UnityEngine;
using static GameString;

public abstract class ObjAttack : MonoBehaviour
{
    [SerializeField] protected List<WeaponMovement> weaponMovements;
    [SerializeField] protected WeaponName nameWeapon;
    [SerializeField] protected WeaponMovement weaponMovementComponent;

    [SerializeField] protected Character character;

    [SerializeField] protected Transform tagetSpawn;
    [SerializeField] protected Transform tagetBot;
    [SerializeField] protected Transform weaponPrefab;
    [SerializeField] protected Transform holderSpawnWeapon;
    [SerializeField] protected Vector3 spawnPos;

    [SerializeField] protected float atkRange;
    [SerializeField] protected float atkSpeed;

    [SerializeField] protected float rotationSpeed = 360f;
    [SerializeField] protected float timeDelay = 1f;

    [SerializeField] protected float currentTime = 0f;
    [SerializeField] protected bool isWeaponThrown = false;
    [SerializeField] protected bool hasTargetBot = false;
    [SerializeField] public bool isAttack = false;

    [SerializeField] protected float additionalRange = 0f;

    [SerializeField] protected List<WeaponDataSO> weaponDataSOs;

    public bool IsWeaponThrown => isWeaponThrown;
    public float AtkRange => atkRange;
    public Character Character => character;

    protected virtual void Start()
    {
        SetNameWeapon();
        SetSpawnPos();
        SpawnWeapon(spawnPos);
    }

    protected virtual void Update()
    {
        DetectBrick();
        HandleDetectionAndThrow();
    }

    protected abstract void SetNameWeapon();

    protected virtual void HandleDetectionAndThrow()
    {
        if (!character.IsInIdleState() || character.IsInDeadState()) return;

        if (hasTargetBot)
        {
            if (currentTime >= timeDelay)
            {
                ThrowWeapon();
                currentTime = 0f;
            }
        }
        else
        {
            currentTime = timeDelay;
        }
    }

    protected virtual void SetSpawnPos()
    {
        spawnPos = tagetSpawn.position;
    }

    protected virtual void SpawnWeapon(Vector3 position)
    {
        if (isWeaponThrown) return;

        weaponPrefab = SpawnerWeapon.Instance.Spawn(GameString.ToString(nameWeapon), spawnPos, Quaternion.identity);

        if (weaponPrefab == null) return;

        weaponPrefab.gameObject.SetActive(true);
        weaponPrefab.SetParent(tagetSpawn);
        weaponPrefab.localRotation = Quaternion.Euler(90, 0, 0);
        weaponPrefab.position = tagetSpawn.position;

        if (weaponPrefab.TryGetComponent(out weaponMovementComponent))
        {
            weaponMovementComponent.SetOwner(character.CompareTag(GameTag.Player) ? GameTag.Player : GameTag.Bot);
        }

        isWeaponThrown = false;
    }

    public virtual void ChangeWeapon(WeaponName newWeaponName)
    {
        nameWeapon = newWeaponName;
        SetNameWeapon();
        if (weaponPrefab != null)
        {
            SpawnerWeapon.Instance.Despawn(weaponPrefab);
        }
        SpawnWeapon(spawnPos);
    }

    protected virtual void ThrowWeapon()
    {
        if (isWeaponThrown) return;

        isAttack = true;
        weaponPrefab.SetParent(holderSpawnWeapon);
        RotateParentTowardsTarget();
        SetAtkSpeed();
        weaponMovementComponent.StartMoveAndRotate(tagetBot, atkSpeed, rotationSpeed, transform, this);

        isWeaponThrown = false;
        hasTargetBot = false;
        SpawnWeapon(spawnPos);
    }


    protected virtual void DetectBrick()
    {
        Collider[] hits = Physics.OverlapSphere(transform.parent.position, atkRange);
        bool foundTargetBot = false;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject != transform.parent.gameObject && (hit.CompareTag(GameTag.Player) || hit.CompareTag(GameTag.Bot)))
            {
                currentTime += Time.deltaTime;
                tagetBot = hit.transform;
                foundTargetBot = true;
                break;
            }
        }

        hasTargetBot = foundTargetBot;
    }

    protected virtual void RotateParentTowardsTarget()
    {
        if (hasTargetBot)
        {
            Vector3 directionToTarget = (tagetBot.position - transform.parent.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
            transform.parent.rotation = lookRotation;
        }
    }

    public virtual void SetAtkSpeed()
    {
        foreach (var weapon in weaponDataSOs)
        {
            if (weapon.weaponName == nameWeapon)
            {
                this.atkSpeed = weapon.atkSpeed;
                this.atkRange = weapon.atkRange + additionalRange;
            }
        }
    }

    public virtual void IncreaseWeaponSize(float scaleIncrease)
    {
        transform.parent.localScale += Vector3.one * scaleIncrease;
    }

    public virtual void AdditionalRange(float add)
    {
        additionalRange += add;
    }

    public bool IsTargetInRange(Transform target)
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.parent.position, target.position);
        return distance <= atkRange;
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.parent.position, atkRange);
    }
}
