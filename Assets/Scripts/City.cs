using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class City
{
    public int index;
    public Vector2 location;
    public bool isOriginCity;

    public City(int index, Vector2 location, bool isOriginCity)
    {
        this.index = index;
        this.location = location;
        this.isOriginCity = isOriginCity;
    }
}
