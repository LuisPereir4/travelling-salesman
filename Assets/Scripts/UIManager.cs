using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject uiParentReference;
    public List<GameObject> citiesIcons;

    public GameObject lineRenderer;

    private void Start()
    {
        DrawCities(FindObjectOfType<GeneticAlgorithm>().cities);
        DrawRoute(FindObjectOfType<GeneticAlgorithm>().population.GetFittestIndividual());
    }

    public void DrawCities(List<City> cities)
    {
        for (int i = 0; i < cities.Count; i++)
        {
            GameObject cityIcon = Instantiate(pinPrefab, uiParentReference.transform);
            cityIcon.transform.localPosition = cities[i].location;

            if (i == 0)
            {
                cityIcon.name = "Origin city ";
                cityIcon.GetComponentInChildren<Text>().text = "Origin city";
                cityIcon.GetComponentInChildren<Image>().color = Color.green;
            }
            else
            {
                cityIcon.name = "City " + i;
                cityIcon.GetComponentInChildren<Text>().text = "City " + i;
            }

            citiesIcons.Add(cityIcon);
        }
    }

    public void DrawRoute(Route route)
    {
        LineRenderer lr = lineRenderer.GetComponent<LineRenderer>();

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 5;
        Material whiteMat = new Material(Shader.Find("Unlit/Texture"));
        lr.material = whiteMat;

        Vector3[] pinPositions = new Vector3[5];
        for (int i = 0; i < route.citiesOrder.Count; i++)
        {
            pinPositions[i] = new Vector3(route.citiesOrder[i].location.x, route.citiesOrder[i].location.y, - 0.5f);
        }

        lr.SetPositions(pinPositions);
    }

}
