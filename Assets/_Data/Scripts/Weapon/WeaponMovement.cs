using System.Collections;
using UnityEngine;

public abstract class WeaponMovement : MonoBehaviour
{
    [SerializeField] protected BoxCollider box;
    [SerializeField] protected string ownerTag;
    [SerializeField] protected Transform ownerObject;
    [SerializeField] protected ObjAttack objAttack;  
    [SerializeField] protected ObjAttack ownerObjAttack;  

    public string OwnerTag => ownerTag;
    public Transform OwnerObject => ownerObject;

    public void StartMoveAndRotate(Transform targetBot, float throwSpeed, float rotationSpeed, Transform owner, ObjAttack ownerAttack)
    {
        ownerObject = owner;
        ownerObjAttack = ownerAttack;  
        StartCoroutine(MoveAndRotateWeapon(targetBot, throwSpeed, rotationSpeed));
    }

    protected abstract IEnumerator MoveAndRotateWeapon(Transform targetBot, float throwSpeed, float rotationSpeed);

    public void SetOwner(string owner)
    {
        ownerTag = owner;
    }

    public void SetOwnerObject()
    {
        ownerObject = null;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (transform.parent.name == GameString.ToString(GameString.WeaponName.TagetSpawnPos)) return;

        objAttack = collision.GetComponentInChildren<ObjAttack>();

        if (collision.gameObject.CompareTag(GameTag.Bot))
        {
            if (collision.gameObject.transform == ownerObject.parent) return;

            if (OwnerTag == GameTag.Player)
            {
                CoinManager.Instance.AddScore(10);

                ownerObjAttack.AdditionalRange(0.1f);
                ownerObjAttack.IncreaseWeaponSize(0.1f);

                SpawnerBot.Instance.DespawnObject(collision.gameObject.transform, objAttack.Character);

            }
            else if (OwnerTag == GameTag.Bot)
            {
                ownerObjAttack.AdditionalRange(0.1f);  
                ownerObjAttack.IncreaseWeaponSize(0.1f);

                SpawnerBot.Instance.DespawnObject(collision.gameObject.transform, objAttack.Character);

            }

            DespawnWeapon();
        }

        if (collision.gameObject.CompareTag(GameTag.Player))
        {
            if (OwnerTag == GameTag.Bot)
            {
                SpawnerBot.Instance.DespawnObject(collision.gameObject.transform, objAttack.Character);
                CoinManager.Instance.DeductScore(20);
                Invoke(nameof(DelayLoseGame), 1f);
            }
        }

        if (collision.gameObject.CompareTag(GameTag.Map))
        {
            DespawnWeapon();
        }
    }

    protected virtual void DelayLoseGame()
    {
        GameManager.Instance.LoseGame();
    }

    protected virtual void DespawnWeapon()
    {
        SetOwnerObject();
        SpawnerWeapon.Instance.Despawn(transform);
    }
}
