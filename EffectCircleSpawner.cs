using System.Collections;
using UnityEngine;

public class EffectCircleSpawner : MonoBehaviour
{
    [ExecuteInEditMode]
    public GameObject particlePrefab; // Particle System prefabýný buradan sürükleyip býrakýn
    public int numberOfParticles = 3; // Oluþturulacak particle sayýsý
    public float radius = 2f; // Dairenin yarýçapýný ayarlayabilirsiniz

    private void Update()
    {
        // K tuþuna basýldýðýnda spawn iþlemi gerçekleþecek
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SpawnParticles());
        }
    }

    IEnumerator SpawnParticles()
    {
        for (int i = 0; i < numberOfParticles; i++) 
        {
            float angle = i * (360f / numberOfParticles);
            Vector3 spawnPosition = GetPositionOnCircle(angle);
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);

            // Her bir particle kendi yönüne baksýn
            particle.transform.LookAt(transform.position);
            particle.transform.Rotate(new Vector3(0,90,0),Space.Self);
   
            yield return null; // Bir frame bekleyerek bir sonraki particle'ý oluþtur
        }
    }

    Vector3 GetPositionOnCircle(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = transform.position.x + radius * Mathf.Cos(radians);
        float y = transform.position.y;
        float z = transform.position.z + radius * Mathf.Sin(radians);

        return new Vector3(x, y, z);
    }
}
