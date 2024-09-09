using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : GameMonoBehaviour
{
    private static CoinManager instance;
    public static CoinManager Instance => instance;


    public Text scoreText; 

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
            scoreText = GetComponent<Text>();
            UpdateCoineText();

        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    protected override void Start()
    {
        UpdateCoineText();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadScoreText();
    }

    protected virtual void LoadScoreText()
    {
        if (scoreText != null) return;

    }    

    public void AddScore(int points)
    {
        GameDataS0.Instance.coinSO.currentCoin += points;
        UpdateCoineText();
    }

    public void DeductScore(int points)
    {
        GameDataS0.Instance.coinSO.currentCoin -= points;
        UpdateCoineText();
    }

    public void UpdateCoineText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coin: " + GameDataS0.Instance.coinSO.currentCoin.ToString();
        }
    }

    public void ResetScore()
    {
        GameDataS0.Instance.coinSO.currentCoin = 0;
        UpdateCoineText();
    }
}
