using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Database
{
    private static string databasePath = "GameDatabase.db";
    private static readonly SQLiteConnection connection;

    static Database()
    {
        connection = new SQLiteConnection(databasePath);

        connection.CreateTable<User>();
        connection.CreateTable<UserRating>();
    }

    public static void RegisterPlayer(string username, string password)
    {
        connection.Insert(new User
        {
            Username = username,
            Password = password,
        });

    }

    public static User GetUser(string username)
    {
        try
        {
            return connection.Get<User>(username);
        }
        catch
        {
            return null;
        }
    }

    public static void Rate(string attractionID, int rating)
    {
        var userRating = GetUserAttractionRating(attractionID);
        if(userRating != null)
        {
            userRating.Rating = rating;
            connection.Update(userRating);
            return;
        }

        connection.Insert(new UserRating
        {
            AttractionID = attractionID,
            Username = User.LoggedInUsername,
            Rating = rating
        });

    }

    public static UserRating GetUserAttractionRating(string attractionID)
    {
        var username = User.LoggedInUsername;
        if (username.Equals(String.Empty))
        {
            return null;
        }

        var results = connection.Query<UserRating>(
               @$"SELECT * FROM {nameof(UserRating)} WHERE
                {nameof(UserRating.AttractionID)} = '{attractionID}' AND 
                {nameof(UserRating.Username)} = '{username}'"
           );

        Debug.Assert(results.Count <= 1, $"{username} has multiple ratings for the same attraction");

        return results.Count == 1 ? results[0] : null;
    }

    public static int GetAttractionTotalRating(string attractionID)
    {
        //var ratings = connection.Table<UserRating>().Where(userRating => userRating.AttractionID == attractionID);

        var ratings = (from userRating in connection.Table<UserRating>() where userRating.AttractionID == attractionID select userRating); // SAME AS ^^

        return ratings.Any() ? ratings.Sum(userRating => userRating.Rating) / ratings.Count() : 0;
    }

    public static void ClearDatabase()
    {
        connection.DeleteAll<User>();
        connection.DeleteAll<UserRating>();
    }


}
