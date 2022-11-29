using System;
using System.Collections.Generic;
using Facebook.Unity;
using TMPro;
using UnityEngine;

public class FacebookManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _friendsListPlayingTheGame;

    private void Awake()
    {
        if (!FB.IsInitialized)
            FB.Init(delegate
                {
                    if (FB.IsInitialized)
                        FB.ActivateApp();
                    else
                        Debug.Log("Nesto ne radi :(");
                }, isGameShown =>
                {
                    if (!isGameShown)
                        Time.timeScale = 0;
                    else
                        Time.timeScale = 1;
                }
            );
        else
            FB.ActivateApp();
    }

    #region Authentication

    public void Login()
    {
        List<string> permissions = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(permissions);
        Debug.Log("Login called!");
    }

    public void Logout()
    {
        FB.LogOut();
        Debug.Log("Logout called!");
    }

    #endregion

    #region Social

    public void Share()
    {
        FB.ShareLink(new Uri("http://google.com"), "Zajeban je ovaj FB SDK",
            new System.Uri("C:'\'Users\vikop\'OneDrive\'Pictures\'IMUM_COELI_HELIOCENTRIC_SIDE_A.jpg").ToString());
    }

    public void GameRequest()
    {
        FB.AppRequest("Something something, come play with me ;)", title: "Placeholder String :]");
    }

    public void GetFriendsThatPlayThisGame()
    {
        string query = "me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            Dictionary<string, object> dictionary =
                (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            List<object> friendsList = (List<object>)dictionary["data"];
            _friendsListPlayingTheGame.text = string.Empty;

            foreach (var friend in friendsList)
            {
                _friendsListPlayingTheGame.text += ((Dictionary<string, object>)dictionary)["name"];
            }
        });
    }

    #endregion
}