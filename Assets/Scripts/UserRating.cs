using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Table("UserRating")]
public class UserRating
{
    [PrimaryKey][AutoIncrement] public int ID { get; set; }
    public string Username { get; set; }
    public string AttractionID { get; set; }
    public int Rating { get; set; }
}
