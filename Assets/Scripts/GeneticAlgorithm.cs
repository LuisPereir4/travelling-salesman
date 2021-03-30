using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{
    [Header("--- User inputs")]
    public int citiesNumber;
    public int populationSize;
    [Range(1, 100)] public int generations;

    [Header("--- Generated data")]
    public List<City> cities;
    public List<Route> routes;

    [Header("--- Calculated data by algorithm")]
    public Population population;
    public Route firstFittestIndividual;
    public Route secondFittestIndividual;

    // Screen bounds
    private readonly float yMin = -300;
    private readonly float yMax = 200;
    private readonly float xMin = -400;
    private readonly float xMax = 400;

    private void Awake()
    {
        // Step 1 and 2: Defining initial population and fitness
        GenerateCities(citiesNumber);
        GenerateRoutes(populationSize);
        DefinePopulation();

        //// Step 3: Selection
        //DefineFirstAndSecondFittestIndividuals();

        //// Step 4: Crossover
        //CrossOver(ref firstFittestIndividual, ref secondFittestIndividual);

        //// Step 5 : Mutation
        //Mutation(ref firstFittestIndividual, ref secondFittestIndividual);

        // Test
        GenerationCycle();
    }

    private void GenerateCities(int citiesNumber)
    {
        Debug.Log("Generating cities");

        for (int i = 0; i < citiesNumber; i++)
        {
            City newCity = new City(i, new Vector2(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax)), false);

            if (i == 0)
                newCity.isOriginCity = true;

            cities.Add(newCity);
        }

        Debug.Log("Total of " + citiesNumber + " cities generated");
    }

    private void GenerateRoutes(int populationSize)
    {
        Debug.Log("Generating routes (individuals and population)");

        for (int i = 0; i < populationSize; i++)
        {
            // Shuffle cities and insert into a new route
            Route newRoute = new Route(cities.OrderBy(x => Guid.NewGuid()).ToList());
            routes.Add(newRoute);
        }

        Debug.Log("Total of " + populationSize + " routes generated");
    }

    private void DefinePopulation()
    {
        this.population = new Population(routes);
    }

    private void Selection()
    {
        this.firstFittestIndividual = population.GetFittestIndividual();
        this.secondFittestIndividual = population.GetSecondFittestIndividual();

        Debug.Log("First fitness individual total distance: " + population.GetFittestIndividual().routeTotalDistance);
        Debug.Log("Second fitness individual total distance: " + population.GetSecondFittestIndividual().routeTotalDistance);
    }

    private void CrossOver(ref Route firstFittestIndividual, ref Route secondFittestIndividual)
    {
        Debug.Log("Starting crossover between first and second fittest individuals");

        int crossOverPoint = UnityEngine.Random.Range(1, firstFittestIndividual.citiesOrder.Count);
        Debug.Log("Crossover point: " + crossOverPoint);

        List<City> citiesUntilCrossPoint1 = new List<City>();
        List<City> citiesUntilCrossPoint2 = new List<City>();

        for (int i = 0; i < crossOverPoint; i++)
        {
            citiesUntilCrossPoint1.Add(firstFittestIndividual.citiesOrder[i]);
            citiesUntilCrossPoint2.Add(secondFittestIndividual.citiesOrder[i]);
        }

        for (int i = 0; i < citiesUntilCrossPoint2.Count; i++)
        {
            firstFittestIndividual.citiesOrder[i] = citiesUntilCrossPoint2[i];
            secondFittestIndividual.citiesOrder[i] = citiesUntilCrossPoint1[i];
        }

         Debug.Log("Crossover between first and second fittest individuals done");
    }

    private void Mutation(ref Route firstFittestIndividual, ref Route secondFittestIndividual)
    {
        Debug.Log("Starting mutation between first and second fittest individuals");
     
        City aux;
        int mutationPoint1 = UnityEngine.Random.Range(0, firstFittestIndividual.citiesOrder.Count);
        int mutationPoint2 = UnityEngine.Random.Range(0, firstFittestIndividual.citiesOrder.Count);

        if (mutationPoint1 != mutationPoint2)
        {
            aux = firstFittestIndividual.citiesOrder[mutationPoint1];
            firstFittestIndividual.citiesOrder[mutationPoint1] = firstFittestIndividual.citiesOrder[mutationPoint2];
            firstFittestIndividual.citiesOrder[mutationPoint2] = aux;
        }

        mutationPoint1 = UnityEngine.Random.Range(0, secondFittestIndividual.citiesOrder.Count);
        mutationPoint2 = UnityEngine.Random.Range(0, secondFittestIndividual.citiesOrder.Count);

        if (mutationPoint1 != mutationPoint2)
        {
            aux = secondFittestIndividual.citiesOrder[mutationPoint1];
            secondFittestIndividual.citiesOrder[mutationPoint1] = secondFittestIndividual.citiesOrder[mutationPoint2];
            secondFittestIndividual.citiesOrder[mutationPoint2] = aux;
        }

        Debug.Log("Mutation between first and second fittest individuals done");
    }

    private void GenerationCycle()
    {
        int currentGeneration = 0;

        Debug.LogWarning("Generation " + currentGeneration + ", best route distance: " + population.GetFittestIndividual().routeTotalDistance);

        while (currentGeneration < generations)
        {
            currentGeneration++;

            Selection();
            CrossOver(ref firstFittestIndividual, ref secondFittestIndividual);
            Mutation(ref firstFittestIndividual, ref secondFittestIndividual);
            population.individuals[population.GetLessFitIndividualIndex()] = firstFittestIndividual;
        }

        //FindObjectOfType<UIManager>().GetComponent<UIManager>().DrawRoute(firstFittestIndividual);

        Debug.LogWarning("Finished with generation " + currentGeneration + ", with route distance: " + population.GetFittestIndividual().routeTotalDistance);
    }

}
