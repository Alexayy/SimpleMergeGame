using System;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Script Reference")] 
    [SerializeField] private FacebookManager _facebook;
    
    [Header("Buttons")]
    [SerializeField] private Button _facebookLoginButton;
    [SerializeField] private Button _facebookLogouButton;
    [SerializeField] private Button _facebookShareButton;
    [SerializeField] private Button _facebookGetFriendsButton;
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _quitAppButton;

    private void Awake()
    {
        _facebookLoginButton.onClick.RemoveAllListeners();
        _facebookLogouButton.onClick.RemoveAllListeners();
        _facebookShareButton.onClick.RemoveAllListeners();
        _facebookGetFriendsButton.onClick.RemoveAllListeners();
        _playGameButton.onClick.RemoveAllListeners();
        _quitAppButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _facebookLoginButton.onClick.AddListener(_facebook.Login);
        _facebookLogouButton.onClick.AddListener(_facebook.Logout);
        _facebookShareButton.onClick.AddListener(_facebook.Share);
        _facebookGetFriendsButton.onClick.AddListener(_facebook.GetFriendsThatPlayThisGame);
        _playGameButton.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
        _quitAppButton.onClick.AddListener(delegate { Application.Quit(); });
    }

    private void Update()
    {
        _playGameButton.enabled = FB.IsLoggedIn;
    }
}
