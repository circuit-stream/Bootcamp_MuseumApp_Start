using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherData
{
    public Coord coord;
    public List<Weather> weather;
    public string @base;
    public Main main;
    public int visibility;
    public Wind wind;
    public Rain rain;
    public Clouds clouds;
    public int dt;
    public Sys sys;
    public int timezone;
    public int id;
    public string name;
    public int cod;
}

[System.Serializable]
public class Coord
{
    public double lon;
    public double lat;
}

[System.Serializable]
public class Weather
{
    public int id;
    public string main;
    public string description;
    public string icon;
}

[System.Serializable] 
public class Main
{
    public double temp;
    public double feels_like;
    public double temp_min;
    public double temp_max;
    public int pressure;
    public int humidity;
    public int sea_level;
    public string grnd_level;
}

[System.Serializable]
public class Wind
{
    public double speed;
    public int deg;
    public double gust;
}

[System.Serializable]
public class Clouds
{
    public int all { get; set; }
}

[System.Serializable]
public class Rain
{
    public double _1h { get; set; }
}

[System.Serializable]
public class Sys
{
    public int type { get; set; }
    public int id { get; set; }
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}
