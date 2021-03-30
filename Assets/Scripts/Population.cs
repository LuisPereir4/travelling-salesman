using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Population
{
    public List<Route> individuals;

    public Population(List<Route> individuals)
    {
        this.individuals = individuals;
    }

    public Route GetFittestIndividual()
    {
        int fittestIndex = 0;
        double lowerDistance = individuals[0].routeTotalDistance;

        for (int i = 0; i < individuals.Count; i++)
        {
            if (individuals[i].routeTotalDistance < lowerDistance)
            {
                fittestIndex = i;
                lowerDistance = individuals[i].routeTotalDistance;
            }
        }

        return individuals[fittestIndex];
    }

    public Route GetSecondFittestIndividual()
    {
        int fittestIndex = 0;
        int secondFittestIndex = 1;

        for (int i = 0; i < individuals.Count; i++)
        {
            if (individuals[i].routeTotalDistance < individuals[fittestIndex].routeTotalDistance)
            {
                secondFittestIndex = fittestIndex;
                fittestIndex = i;
            }
            else if (individuals[i].routeTotalDistance < individuals[secondFittestIndex].routeTotalDistance)
            {
                secondFittestIndex = i;
            }
        }

        return individuals[secondFittestIndex];
    }

    public int GetLessFitIndividualIndex()
    {
        int lessFitIndex = 0;
        double greaterDistance = individuals[0].routeTotalDistance;

        for (int i = 0; i < individuals.Count; i++)
        {
            if (individuals[i].routeTotalDistance > greaterDistance)
            {
                lessFitIndex = i;
                greaterDistance = individuals[i].routeTotalDistance;
            }
        }

        return lessFitIndex;
    }

}
