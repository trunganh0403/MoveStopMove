using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SpawnerBot : Spawner
{
    [Header("SpawnerBot")]
    private static SpawnerBot instance;
    public static SpawnerBot Instance { get => instance; }

    [SerializeField] protected DeadsState deadState = new();
    [SerializeField] protected IdleState idle = new();

    protected override void Awake()
    {
        base.Awake();
        if (SpawnerBot.instance != null) Debug.LogError("Only 1 SpawnerBot allow to exist");
        SpawnerBot.instance = this;
    }

    public virtual void DespawnObject(Transform obj, Character t)
    {
        t.ChangeState(deadState);
        t.StartCoroutine(AnimationDaead(obj));
    }

    private IEnumerator AnimationDaead(Transform obj)
    {
        yield return new WaitForSeconds(0.7f);      
        this.Despawn(obj);
    }

}
