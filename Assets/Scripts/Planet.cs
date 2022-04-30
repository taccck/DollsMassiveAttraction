using UnityEngine;
using UnityEditor;

public class Planet : MonoBehaviour
{
    public bool useRadius = false;
    [SerializeField] float radius = 10;
    public bool useGravityScale = false;
    [SerializeField] float gravityScale = 9.8f;
    [SerializeField] float mass = 10;

    public Vector3 GravityDirection(Vector3 from, float massF)
    {
        Vector3 GravityDirection = (transform.position - from).normalized;
        float distance = Vector3.Distance(transform.position, from);
        float GravityMagnitude = useGravityScale ? gravityScale : (massF * mass) / distance;
        GravityDirection *= GravityMagnitude;
        if (useRadius && distance > radius) return Vector3.zero;
        return GravityDirection;
    }

    void OnDrawGizmos()
    {
        if (!useRadius) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

[CustomEditor(typeof(Planet))]
[CanEditMultipleObjects]
public class PlanetEditor : Editor
{
    SerializedProperty useRadius;
    SerializedProperty radius;
    SerializedProperty useGravityScale;
    SerializedProperty gravityScale;
    SerializedProperty mass;
    void OnEnable()
    {
        useRadius = serializedObject.FindProperty("useRadius");
        radius = serializedObject.FindProperty("radius");
        useGravityScale = serializedObject.FindProperty("useGravityScale");
        gravityScale = serializedObject.FindProperty("gravityScale");
        mass = serializedObject.FindProperty("mass");
    }

    public override void OnInspectorGUI()
    {
        Planet planet = target as Planet;

        serializedObject.Update();
        EditorGUILayout.PropertyField(useRadius);
        if (planet.useRadius) EditorGUILayout.PropertyField(radius);
        EditorGUILayout.PropertyField(useGravityScale);
        if (planet.useGravityScale) EditorGUILayout.PropertyField(gravityScale);
        if (!planet.useGravityScale) EditorGUILayout.PropertyField(mass);

        serializedObject.ApplyModifiedProperties();
    }
}   
