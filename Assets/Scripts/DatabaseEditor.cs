using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class DatabaseEditor
{
    [MenuItem("Database/Clear Database")]
    private static void ClearDatabase()
    {
        Database.ClearDatabase();
    }
}
