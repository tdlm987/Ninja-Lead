﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using Fusion;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quest : NetworkBehaviour
{
    private static Quest instance;
    private void Awake()
    {
        if (Quest.instance != null)
            Debug.LogWarning("Only 1 instace Quest allow!");
        Quest.instance = this;
    }

    [Header("Game Over")]
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI dlg_gameOver;
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtCoin;

    [Header("Pause Game")]
    [SerializeField] private RectTransform panelPauseGame;
    [SerializeField] private TextMeshProUGUI dlg_PauseGame;

    [Header("Panel Info Player")]
    [SerializeField] private TextMeshProUGUI title_Coin;
    [SerializeField] private TextMeshProUGUI txt_Coin;
    [SerializeField] private TextMeshProUGUI title_Distance;
    [SerializeField] private TextMeshProUGUI txt_Distance;
    [SerializeField] private TextMeshProUGUI title_HighScore;
    [SerializeField] private TextMeshProUGUI txt_HighScore;
    public static Quest Instance { get => instance; }

    [Header("Panel Effect")]
    [SerializeField] private CanvasGroup pauseGamePanelCanvasGroup;
    [SerializeField] private float pauseGame_durationIn;
    [SerializeField] private float pauseGame_durationOut;

    protected void Start()
    {
        this.OnStartNewGame();
    }

    public void DislayGameOverPanel(PlayerMovement player)
    {
        player.gameObject.GetComponentInChildren<Canvas>().transform.GetChild(0).gameObject.SetActive(true);
        txtScore.text = DistanceMeasure.Instance.Distance.ToString("N0", new CultureInfo("en-US")) + " M";
        txtCoin.text = PlayerInfo.Instance.Current_Coins.ToString();
    }
    public void DisplayHighScore(int _score)
    {
        this.title_HighScore.text = Localization.title_HighScore;
        this.txt_HighScore.text = _score.ToString("N0", new CultureInfo("en-US")) + " M";
    }
    public void DisplayCurrentCoins(int _coins)
    {
        this.txt_Coin.text = _coins.ToString();
    }
    public void DisplayCurrentDistance(float _distance)
    {
        this.title_Distance.text = Localization.title_Distance;
        this.txt_Distance.text = _distance.ToString("N0", new CultureInfo("en-US"));
    }
    public void StopMeasureDistance()
    {
        DistanceMeasure.Instance.UpdateStateMeasureDistance(false);
        this.SaveHighScore((int)Mathf.Round(DistanceMeasure.Instance.Distance));
    }
    private void SaveHighScore(int score)
    {
        if (score >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            Debug.Log("Đã lưu điểm!");
        }
    }
    public void OnGameOver(PlayerMovement playerMovement)
    {
        Time.timeScale = 0f;
        this.DislayGameOverPanel(playerMovement);
        this.StopMeasureDistance();
    }
    public void OnStartNewGame()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        this.DisplayHighScore(highScore);
        //DistanceMeasure.Instance.UpdateStateMeasureDistance(true);
        this.DisplayCurrentDistance(DefaultValue());
        this.DisplayCurrentCoins(DefaultValue());
        this.panelPauseGame.gameObject.SetActive(false);
        this.panelGameOver.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene("MainGame");
        DOTween.KillAll();
        Time.timeScale = 1f;
        this.OnStartNewGame();
    }
    public void PauseGame()
    {
        this.pauseGamePanelCanvasGroup.alpha = 0;
        this.pauseGamePanelCanvasGroup.gameObject.SetActive(true);
        this.pauseGamePanelCanvasGroup.DOFade(1, pauseGame_durationIn).SetEase(Ease.OutBack).OnComplete(() =>
        {
            Time.timeScale = 0f;
            pauseGamePanelCanvasGroup.interactable = true;
            pauseGamePanelCanvasGroup.blocksRaycasts = true;
        });
        if(HasInputAuthority)
            PlayerMovement.Instance.CheckMove(false);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        this.pauseGamePanelCanvasGroup.DOFade(0, pauseGame_durationOut).SetEase(Ease.InExpo).OnComplete(() =>
        {
            PlayerMovement.Instance.CheckMove(true);
            pauseGamePanelCanvasGroup.interactable = false;
            pauseGamePanelCanvasGroup.blocksRaycasts = false;
            this.pauseGamePanelCanvasGroup.gameObject.SetActive(false);
        });
    }
    public GameObject playerUIPrefab; // Prefab UI của mỗi Player
    public Transform uiContainer; // Nơi chứa các UI của Player

    [SerializeField] private Dictionary<string, PlayerControllerUI> playerUIDictionary = new Dictionary<string, PlayerControllerUI>();
    [SerializeField] private List<PlayerControllerUI> listUIPlayer;
    public void RegisterPlayerUI(string playerID)
    {
        if (!playerUIDictionary.ContainsKey(playerID))
        {
            NetworkObject networkObj = Runner.Spawn(playerUIPrefab, uiContainer.position, Quaternion.identity, Runner.LocalPlayer, (runner, obj) =>
            {
                PlayerControllerUI uiComponent = obj.GetComponent<PlayerControllerUI>();
                DistanceMeasure distanceMeasure = obj.GetComponentInChildren<DistanceMeasure>();
                uiComponent.PlayerID = playerID;
                listUIPlayer.Add(uiComponent);
                playerUIDictionary.Add(playerID, uiComponent);
            });
        }
    }
    public void Update()
    {
        UpdateUIVisibility();
    }
    [SerializeField] private PlayerControllerUI[] listUI1;
    private void UpdateUIVisibility()
    {
        listUI1 = FindObjectsOfType<PlayerControllerUI>();
        foreach (var kvp in listUI1)
        {
            if (listUIPlayer.Contains(kvp))
            {
                kvp.gameObject.SetActive(true);
                kvp.gameObject.GetComponentInChildren<DistanceMeasure>().IsMeasureDistance = true;
            }
            else
            {
                kvp.gameObject.SetActive(false);
            }
        }
    }


    public void UpdatePlayerUI(string playerID/*, string name*/, int coins/*, float health*/)
    {
        if (playerUIDictionary.TryGetValue(playerID, out PlayerControllerUI playerUI))
        {
            playerUI.UpdateUI(coins);
        }
    }
    private int DefaultValue() => 0;
}
