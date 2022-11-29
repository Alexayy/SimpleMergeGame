using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _quitApplicationButton;

    private void Awake()
    {
        _playAgainButton.onClick.RemoveAllListeners();
        _mainMenuButton.onClick.RemoveAllListeners();
        _quitApplicationButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _playAgainButton.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
        _mainMenuButton.onClick.AddListener(delegate { SceneManager.LoadScene("MainMenu"); });
        _quitApplicationButton.onClick.AddListener(delegate { Application.Quit(); });
    }
}
