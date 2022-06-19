using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    [SerializeField] int sceneOffset; // to control scene build index
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;
    [SerializeField] Image progressFillBar;
    [SerializeField] Image progressPart;

    [Space]
    [SerializeField] Image fadePanel;

    [Space]
    [Header("Game Over UI")]
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] TextMeshProUGUI congratText;


    private void Start()
    {
        progressFillBar.fillAmount = 0f;

        SetLevelText();

        LevelFade();
    }

    private void SetLevelText()
    {
        int level = SceneController.Instance.GetCurrentSceneIdx() + sceneOffset;

        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    public void UpdateLevelProgressBar()
    {
        float value = 1f - ((float)Level.Instance.objectsInLevel / Level.Instance.totalObjects);

        progressFillBar.DOFillAmount(value, 0.4f);
    }

    private void LevelFade()
    {
        fadePanel.DOFade(0f, 1.2f).From(1f);
    }

    public void GameOver()
    {
        HideProgressPart();

        //Game over UI
        fadePanel.DOFade(0.7f, 0.5f).SetDelay(0.4f).OnComplete(() =>
        {
            gameOverText.DOFade(1f, 0.3f).SetEase(Ease.InSine).OnComplete(() =>
            {
                congratText.DOFade(1f, 0.3f).SetEase(Ease.InSine);
            });
        });
    }

    private void HideProgressPart()
    {
        progressPart.DOFade(0f, 0.4f);
        progressFillBar.DOFade(0f, 0.4f);
        currentLevelText.DOFade(0f, 0.4f);
        nextLevelText.DOFade(0f, 0.4f);
    }

}
