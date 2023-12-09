using UnityEditor;
using UnityEngine;

public class EffectCircleSpawnerTool : EditorWindow
{
    public GameObject particlePrefab;
    public GameObject playerObject;
    public int numberOfParticles = 3;
    public float radius = 2f;

    [MenuItem("Tool/EffectCircleSpawnerTool")]
    public static void ShowWindow()
    {
        GetWindow<EffectCircleSpawnerTool>("EffectCircleSpawnerTool");
    }

    private void OnGUI()
    {
        particlePrefab = EditorGUILayout.ObjectField("Particle Prefab", particlePrefab, typeof(GameObject), true) as GameObject;
        playerObject = EditorGUILayout.ObjectField("Player Object", playerObject, typeof(GameObject), true) as GameObject;
        numberOfParticles = EditorGUILayout.IntField("Number of Particles", numberOfParticles);
        radius = EditorGUILayout.FloatField("Radius", radius);

        if (GUILayout.Button("Spawn Particles"))
        {
            SpawnParticles();
        }
    }

    private void SpawnParticles()
    {
        if (particlePrefab == null || playerObject == null)
        {
            Debug.LogError("Particle Prefab and Player Object must be assigned!");
            return;
        }

        for (int i = 0; i < numberOfParticles; i++)
        {
            float angle = i * (360f / numberOfParticles);
            Vector3 spawnPosition = GetPositionOnCircle(angle, playerObject.transform.position);
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);
            particle.transform.LookAt(playerObject.transform.position);
            particle.transform.Rotate(new Vector3(0, 90, 0), Space.Self);

            // Eðer sahne içinde anýnda görmek istiyorsanýz:
            // Undo.RegisterCreatedObjectUndo(particle, "Spawn Particle");
        }
    }

    private Vector3 GetPositionOnCircle(float angle, Vector3 center)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = center.x + radius * Mathf.Cos(radians);
        float y = center.y;
        float z = center.z + radius * Mathf.Sin(radians);

        return new Vector3(x, y, z);
    }
}
