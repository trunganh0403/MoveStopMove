using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomBot : MonoBehaviour
{
    [Header("Spawner Random")]
    [SerializeField] protected List<Transform> spawnPos;
    [SerializeField] protected float randomDelay = 2f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 9f;

    protected virtual void FixedUpdate()
    {
        this.JunkSpawning();
    }

    protected virtual void JunkSpawning()
    {
        if (this.RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;
        //Debug.Log(SpawnerBot.Instance.SpawnedCount);

        Transform botPrefab = SpawnerBot.Instance.RandomPrefab();
        Transform spawnPos = RandomPrefabPos();
        Transform bot = SpawnerBot.Instance.Spawn(botPrefab, spawnPos.position, Quaternion.identity);
        bot.gameObject.SetActive(true);
    }

    public virtual Transform RandomPrefabPos()
    {
        int rand = Random.Range(0, this.spawnPos.Count);
        return this.spawnPos[rand];
    }

    protected virtual bool RandomReachLimit()
    {
        return SpawnerBot.Instance.SpawnedCount >= this.randomLimit;
    }
}
