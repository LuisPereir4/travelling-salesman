using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Route
{
    public List<City> citiesOrder;
    public double routeTotalDistance;
    public int totalGenes;

    public Route(List<City> citiesOrder)
    {
        this.citiesOrder = citiesOrder;
        this.routeTotalDistance = CalculateRouteDistance();
        totalGenes = 2;
    }

    public double CalculateRouteDistance()
    {
        double totalDistance = 0;

        for (int i = 0; i < citiesOrder.Count - 1; i++)
        {
            totalDistance += Vector2.Distance(citiesOrder[i].location, citiesOrder[i + 1].location);
        }

        return totalDistance;
    }
}
