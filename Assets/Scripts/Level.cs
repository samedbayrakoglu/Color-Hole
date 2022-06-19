using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level : MonoBehaviour
{
    #region Singleton
    public static Level Instance;

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

    [SerializeField] ParticleSystem winFX;

    [Space]
    [SerializeField] Transform objectsParent;

    public int objectsInLevel;
    public int totalObjects;

    [Space]
    [Header("Level Materials")]
    [SerializeField] Material boardMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] SpriteRenderer boardBordersSprite;
    [SerializeField] SpriteRenderer boardSideSprite;
    [SerializeField] Image progressFillBar;

    [Space]
    [Header("Level Colors")]
    [SerializeField] Color boardColor;
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;
    [SerializeField] Color boardBorderColor;
    [SerializeField] Color boardSideColor;
    [SerializeField] Color progressBarColor;
    [SerializeField] Color cameraColor;



    private void Start()
    {
        CountObjects();

        UpdateLevelColors();
    }

    private void CountObjects()
    {
        totalObjects = objectsParent.childCount;

        objectsInLevel = totalObjects;
    }

    public void RemoveOneObject()
    {
        objectsInLevel--;
    }

    public void LevelCompleted()
    {
        GameData.isGameOver = true;

        PlayWinFX();

        Invoke("LoadNextLevel", winFX.main.duration);
        // LoadNextLevel();
    }

    public void LevelFailed()
    {
        GameData.isGameOver = true;

        Camera.main.transform.DOShakePosition(1f, 0.2f, 20, 90).OnComplete(() =>
        {
            SceneController.Instance.RestartLevel();
        });
    }

    private void LoadNextLevel()
    {
        SceneController.Instance.LoadLevel(SceneController.Instance.GetCurrentSceneIdx() + 1); // load next level
    }

    private void PlayWinFX()
    {
        winFX.Play();
    }

    private void UpdateLevelColors()
    {
        boardMaterial.color = boardColor;
        objectMaterial.color = objectColor;
        obstacleMaterial.color = obstacleColor;

        boardBordersSprite.color = boardBorderColor;
        boardSideSprite.color = boardSideColor;

        progressFillBar.color = progressBarColor;

        Camera.main.backgroundColor = cameraColor;
    }

    private void OnValidate()
    {
        UpdateLevelColors();
    }
}
