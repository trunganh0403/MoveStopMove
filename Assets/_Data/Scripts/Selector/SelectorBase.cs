using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameString;
using Unity.VisualScripting;

public abstract class SelectorBase : MonoBehaviour
{
    [Header("SelectorBase")]
    [SerializeField] protected Button startButton;
    [SerializeField] protected Button buyButton;

    [SerializeField] protected Button nextButton;
    [SerializeField] protected Button previousButton;
    
    [SerializeField] protected Button colorButton;
    [SerializeField] protected Button weaponButton;
   

    [SerializeField] protected int currentIndex = 0;
    [SerializeField] protected bool isColorMode = true;
    [SerializeField] protected Text cost;

    [SerializeField] protected Color selectedColor = Color.white;
    [SerializeField] protected Color unselectedColor = Color.gray;

    protected virtual void Awake()
    {
        // For Override
    }
    protected void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        buyButton.onClick.AddListener(OnBuyButtonClicked);

        nextButton.onClick.AddListener(OnNextButtonClicked);
        previousButton.onClick.AddListener(OnPreviousButtonClicked);
       
        colorButton.onClick.AddListener(OnColorButtonClicked);
        weaponButton.onClick.AddListener(OnWeaponButtonClicked);

        LoadPreviousSelection();
        UpdateButtonColors();
        cost.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }

    protected virtual void OnNextButtonClicked()
    {
        currentIndex = (currentIndex + 1) % GetSelectionCount();
        UpdateSelection();
    }

    protected virtual void OnPreviousButtonClicked()
    {
        currentIndex = (currentIndex - 1 + GetSelectionCount()) % GetSelectionCount();
        UpdateSelection();
    }

    protected virtual void OnColorButtonClicked()
    {
        ToggleDisplay(cost.gameObject,false);
        ToggleDisplay(buyButton.gameObject,false);

        isColorMode = true;
        currentIndex = 0;
        UpdateSelection();
        UpdateButtonColors();
    }

    protected virtual void OnWeaponButtonClicked()
    {
        ToggleDisplay(cost.gameObject,true);
        ToggleDisplay(buyButton.gameObject, true);

        isColorMode = false;
        currentIndex = 0;
        UpdateSelection();
        UpdateButtonColors();
    }

    protected virtual void OnStartButtonClicked()
    {
        LoadNextScene();
    }
    
    protected virtual void OnBuyButtonClicked()
    {
        PurchaseItem();
    }

    protected abstract int GetSelectionCount();
    protected abstract void UpdateSelection();
    protected abstract void PurchaseItem();


    protected virtual void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }

    private void UpdateButtonColors()
    {
        ColorBlock colorBlock = colorButton.colors;
        ColorBlock weaponColorBlock = weaponButton.colors;

        if (isColorMode)
        {
            colorBlock.normalColor = selectedColor;
            weaponColorBlock.normalColor = unselectedColor;
        }
        else
        {
            colorBlock.normalColor = unselectedColor;
            weaponColorBlock.normalColor = selectedColor;
        }

        colorButton.colors = colorBlock;
        weaponButton.colors = weaponColorBlock;
    }

    protected virtual void ToggleDisplay(GameObject obj,bool isVisible)
    {
        if (obj == null) return;
        obj.SetActive(isVisible);
    }      

    protected virtual void LoadPreviousSelection()
    {
        //FOR OVR
    }
}
