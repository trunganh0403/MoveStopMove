using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer botRenderer;
    [SerializeField] private Image image;
    [SerializeField] private Color[] colors;

    protected virtual void Awake()
    {
        colors = new Color[]
      {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            Color.cyan
      };
    }

    private void OnEnable()
    {
        if (colors.Length > 0)
        {
            Color randomColor = colors[Random.Range(0, colors.Length)];
            ApplyColor(randomColor);
        }
        else
        {
            Debug.LogWarning("No colors assigned for the bot.");
        }
    }

    //private void Start()
    //{
    //    if (colors.Length > 0)
    //    {
    //        Color randomColor = colors[Random.Range(0, colors.Length)];
    //        ApplyColor(randomColor);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("No colors assigned for the bot.");
    //    }
    //}

    private void ApplyColor(Color color)
    {
        if (botRenderer != null)
        {
            botRenderer.material.color = color;
        } 
        
        if (image != null)
        {
            image.color = color;
        }
    }
}
