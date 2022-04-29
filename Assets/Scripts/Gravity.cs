using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] GameObject PlanetDaddy;

    private List<Planet> planets = new List<Planet>();
    private Vector3 RandomVelocity;

    private void Awake()
    {
        foreach (Planet planet in PlanetDaddy.GetComponentsInChildren<Planet>())
        {
            planets.Add(planet);
        }

        RandomVelocity = new Vector3(Random.Range(-20,20), Random.Range(-20,20), Random.Range(-20,20));
    }


    private void Start()
    {
        GetComponent<Rigidbody>().velocity = RandomVelocity;
    }

    private void FixedUpdate()
    {
        Planet closest = GetClosestPlanet();

        Vector3 GravityDirection = (closest.transform.position - transform.position).normalized;
        Physics.gravity = GravityDirection * Physics.gravity.magnitude;
    }

    Planet GetClosestPlanet()
    {
        Planet closest = null;
        float closestDist = float.MaxValue;
        foreach(Planet planet in planets)
        {
            float dist = Vector3.Distance(transform.position, planet.transform.position);
            if (dist < closestDist)
            {
                closest = planet;
                closestDist = dist;
            }
        }
        return closest;
    }
}
