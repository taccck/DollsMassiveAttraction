using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public enum GravityType { Closest, All };

public class Gravity : MonoBehaviour
{
    public GameObject planetDaddy;
    public float mass = 1;

    public GravityType gravityType;

    private static List<Planet> planets = new List<Planet>();
    private Vector3 RandomVelocity;

    private void Awake()
    {
        RandomVelocity = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20));

        if (planets.Count > 0) return;
        foreach (Planet planet in planetDaddy.GetComponentsInChildren<Planet>())
        {
            planets.Add(planet);
        }
    }

    private void FixedUpdate()
    {
        Vector3 gravityDirection = Vector3.zero;

        switch (gravityType)
        {
            case GravityType.Closest:
                Planet closest = GetClosestPlanet();
                gravityDirection = closest.GravityDirection(transform.position, mass);
                break;

            case GravityType.All:
                foreach(Planet planet in planets)
                    gravityDirection += planet.GravityDirection(transform.position, mass);
                break;
        }

        Physics.gravity = gravityDirection;
        transform.LookAt(gravityDirection);
    }

    Planet GetClosestPlanet()
    {
        Planet closest = null;
        float closestDist = float.MaxValue;
        foreach (Planet planet in planets)
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

[CustomEditor(typeof(Gravity))]
public class GravityEditor : Editor
{
    SerializedProperty planetDaddy;
    SerializedProperty mass;
    SerializedProperty gravityType;

    void OnEnable()
    {
        planetDaddy = serializedObject.FindProperty("planetDaddy");
        mass = serializedObject.FindProperty("mass");
        gravityType = serializedObject.FindProperty("gravityType");
    }

    public override void OnInspectorGUI()
    {
        Gravity gravity = target as Gravity;

        serializedObject.Update();
        EditorGUILayout.PropertyField(planetDaddy);
        EditorGUILayout.PropertyField(mass);
        EditorGUILayout.PropertyField(gravityType);
        serializedObject.ApplyModifiedProperties();
    }
}   
