
using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanel : GameMonoBehaviour
{
    [SerializeField] protected bool isOpen = false;
    public bool IsOpen => isOpen;

    protected override void Start()
    {
        base.Start();
        this.Close();
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else Close();
    }
    public virtual void Open()
    {
        Time.timeScale = 0.0001f;
        this.gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }

}