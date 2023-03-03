using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayfabController 
{

    private static PlayfabController instance;
    public static PlayfabController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayfabController();
            }
            return instance;
        }
    }

    private PlayfabController()
    {

    }

    public Dictionary<string, string> titleData;
    public Action titleDataAquired;
    public void LoginWithPlayfab(Action callback = null)
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "GettingStartedGuide",
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(
            request, 
            result => OnLoginSuccess(result, callback), 
            error => OnPlayfabFailure(error, "LoginWithEmailAddress"));
    }

    public void LoginWithPlayfab(string email, string password, Action callback = null)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(
            request,
            result => OnLoginSuccess(result, callback),
            error => OnPlayfabFailure(error, "LoginWithEmailAddress"));

    }

    public void RegisterPlayfabUser(string email,string password, Action callback = null)
    {
        var request = new RegisterPlayFabUserRequest
        {
            RequireBothUsernameAndEmail = false,
            Email = email,
            Password = password
        };
        PlayFabClientAPI.RegisterPlayFabUser(
            request,
            result => OnRegister(result, callback),
            error => OnPlayfabFailure(error, "LoginWithEmailAddress"));
    }

    private void OnRegister(RegisterPlayFabUserResult result, Action callback)
    {
        Debug.Log($"Registered new user: {result.PlayFabId}");
        callback?.Invoke();
    }

    public void FetchTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataAquired, error => OnPlayfabFailure(error, "GetTitleData"));
    }

    private void OnTitleDataAquired(GetTitleDataResult obj)
    {
        foreach(var entry in obj.Data)
        {
            Debug.Log($"Data: {entry.Key} - {entry.Value}");
        }
        titleData = obj.Data;
        titleDataAquired?.Invoke();
    }

    private void OnLoginSuccess(LoginResult result, Action callback)
    {
        Debug.Log($"Successfully logged in with: {result.PlayFabId}");
        callback?.Invoke();
    }

    private void OnPlayfabFailure(PlayFabError error, string requestName)
    {
        Debug.LogWarning($"Something went wrong with request: {requestName}");
        Debug.LogError(error.GenerateErrorReport());
    }
}
