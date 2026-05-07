using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    // UI Panels
    public Slider xpBar;
    public GameObject mainMenuPanel;
    public GameObject inGamePanel;
    public GameObject placeWeaponPanel;
    public GameObject weaponSlotButtonPrefab;
    public GameObject chooseUpgradePanel;
    public GameObject gameWinPanel;
    public GameObject upgradeBtn1;
    public GameObject upgradeBtn2;
    public GameObject nextLevelBtn;
    // UI Texts & Bars
    public TextMeshProUGUI levelInGameText;
    public TextMeshProUGUI XPBarText;
    public Slider scoreBar;
    public TextMeshProUGUI scoreBarText;
    // Settings Panel
    public GameObject settingPanel;
    public GameObject musicSlider;
    public GameObject sfxSlider;
    private bool isSettingPanelOpen = false;

    private readonly System.Collections.Generic.Dictionary<WeaponUpgrade, string> upgradeLabels = new System.Collections.Generic.Dictionary<WeaponUpgrade, string>
    {
        { WeaponUpgrade.Damage, "Damage +" },
        { WeaponUpgrade.Time, "Faster Attacks" },
        { WeaponUpgrade.Range, "Range +" },
        { WeaponUpgrade.MoreWeapons, "More Weapons" }
    };

    void Awake()
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

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
            HandleGameStateChanged(GameManager.instance.gameState);
        }
    }

    private void OnDisable()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.OnGameStateChanged -= HandleGameStateChanged;
        }
    }

    private void HandleGameStateChanged(GameState state)
    {
        HideAllPanels();

        switch (state)
        {
            case GameState.MainMenu:
                ShowPanel(mainMenuPanel);
                break;
            case GameState.Playing:
                ShowPanel(inGamePanel);
                break;
            case GameState.PlaceWeapon:
                ShowPanel(placeWeaponPanel);
                break;
            case GameState.ChooseUpgrade:
                ShowPanel(chooseUpgradePanel);
                break;
            case GameState.GameWin:
                ShowPanel(gameWinPanel);
                break;
            default:
                throw new System.ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void SetUpXPBar(float minXP, float maxXP)
    {
        if (xpBar != null)
        {
            xpBar.minValue = minXP;
            xpBar.maxValue = maxXP;
            XPBarText.text = $"{minXP} / {maxXP} $";
        }
    }
    public void UpdateXPBar(float currentXP)
    {
        if (xpBar != null)
        {
            xpBar.value = currentXP;
            XPBarText.text = $"{currentXP} / {xpBar.maxValue} $";
        }
    }

    public void SetUpScoreBar(float minScore, float maxScore)
    {
        if (scoreBar != null)
        {
            scoreBar.minValue = minScore;
            scoreBar.maxValue = maxScore;
            scoreBar.value = minScore;

            if (scoreBarText != null)
            {
                scoreBarText.text = $"{minScore} / {maxScore} XP";
            }
        }
    }

    public void UpdateScoreBar(float currentScore)
    {
        if (scoreBar != null)
        {
            scoreBar.value = currentScore;

            if (scoreBarText != null)
            {
                scoreBarText.text = $"{currentScore} / {scoreBar.maxValue} XP";
            }
        }
    }

    public void SetLevelText(int levelIndex)
    {
        if (levelInGameText != null)
        {
            levelInGameText.text = "LEVEL " + (levelIndex + 1);
        }
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            Debug.Log("Showing panel: " + panel.name);
            panel.SetActive(true);
        }
    }
    private void HidePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    private void HideAllPanels()
    {
        HidePanel(mainMenuPanel);
        HidePanel(inGamePanel);
        HidePanel(placeWeaponPanel);
        HidePanel(chooseUpgradePanel);
        HidePanel(gameWinPanel);
        HidePanel(settingPanel);
    }
    public void SetUpgradeButtons(WeaponUpgrade upgrade1, WeaponUpgrade upgrade2)
    {
        upgradeBtn1.GetComponent<UpgradeBtnController>().upgrade = upgrade1;
        upgradeBtn2.GetComponent<UpgradeBtnController>().upgrade = upgrade2;

        SetButtonLabel(upgradeBtn1, GetUpgradeLabel(upgrade1));
        SetButtonLabel(upgradeBtn2, GetUpgradeLabel(upgrade2));
    }

    private string GetUpgradeLabel(WeaponUpgrade upgrade)
    {
        if (upgradeLabels.TryGetValue(upgrade, out string label))
        {
            return label;
        }
        return "Unknown";
    }
    private void SetButtonLabel(GameObject buttonObj, string label)
    {
        TMP_Text tmpText = buttonObj.GetComponentInChildren<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = label;
            return;
        }

        Text legacyText = buttonObj.GetComponentInChildren<Text>();
        if (legacyText != null)
        {
            legacyText.text = label;
            return;
        }

        Debug.LogWarning("No text component found on button: " + buttonObj.name);
    }

    public void SetUpWeaponSlotButton(GameObject obstacle)
    {
        GameObject newButton = Instantiate(weaponSlotButtonPrefab, placeWeaponPanel.transform);
        WeaponSlotController controller = newButton.GetComponent<WeaponSlotController>();
        controller.obstacle = obstacle;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obstacle.transform.position);
        newButton.transform.position = screenPos;
    }

    public void ClearWeaponSlotButtons()
    {
        if (placeWeaponPanel == null)
        {
            return;
        }

        Transform panel = placeWeaponPanel.transform;
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            Transform child = panel.GetChild(i);
            if (child.GetComponent<WeaponSlotController>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void MenuPlayButton()
    {
        GameEvents.Publish(new PlayButtonPressedEvent());
    }
    public void NextLevelButton()
    {
        GameEvents.Publish(new NextLevelButtonPressedEvent());
    }
    public void BackToMenuButton()
    {
        GameEvents.Publish(new BackToMenuButtonPressedEvent());
    }
    public void ReplayLevelButton()
    {
        GameEvents.Publish(new ReplayLevelButtonPressedEvent());
    }
    public void ToggleSettingPanel()
    {
        if (settingPanel != null)
        {
            musicSlider.GetComponent<Slider>().value = AudioManager.instance.GetMusicVolume();
            sfxSlider.GetComponent<Slider>().value = AudioManager.instance.GetSFXVolume();
            isSettingPanelOpen = !isSettingPanelOpen;
            settingPanel.SetActive(isSettingPanelOpen);
        }
    }
    public void OnMusicVolumeChanged()
    {
        float volume = musicSlider.GetComponent<Slider>().value;
        AudioManager.instance.SetMusicVolume(volume);
    }
    public void OnSFXVolumeChanged()
    {
        float volume = sfxSlider.GetComponent<Slider>().value;
        AudioManager.instance.SetSFXVolume(volume);
        AudioManager.instance.PlayPopSFX();
    }
}
