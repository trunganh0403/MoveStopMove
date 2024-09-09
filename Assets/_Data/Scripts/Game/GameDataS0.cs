using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataS0 : MonoBehaviour
{
    private static GameDataS0 instance;
    public static GameDataS0 Instance { get => instance; }

    public PlayerDataSO playerData;
    public CoinSO coinSO;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
