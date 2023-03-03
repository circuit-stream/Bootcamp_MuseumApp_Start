using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Table("User")]
public class User
{
    public static string loggedUserSaveKey = "loggedUserSaveKey";
    public static string LoggedInUsername => PlayerPrefs.GetString(loggedUserSaveKey, String.Empty);
    public static bool IsLoggedIn => !LoggedInUsername.Equals(String.Empty);

    public static void Login(string username) => PlayerPrefs.SetString(loggedUserSaveKey, username);

    public static void LogOff() => PlayerPrefs.DeleteKey(loggedUserSaveKey);

    [PrimaryKey] public string Username { get; set; }

    // THIS IS NOT RECOMMENDED. DON'T STORE PASSWORDS LIKE THIS
    public string Password { get; set; }
}
