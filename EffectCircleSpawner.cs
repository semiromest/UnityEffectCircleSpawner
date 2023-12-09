using System.Collections;
using UnityEngine;

public class EffectCircleSpawner : MonoBehaviour
{
    [ExecuteInEditMode]
    public GameObject particlePrefab; // Particle System prefab�n� buradan s�r�kleyip b�rak�n
    public int numberOfParticles = 3; // Olu�turulacak particle say�s�
    public float radius = 2f; // Dairenin yar��ap�n� ayarlayabilirsiniz

    private void Update()
    {
        // K tu�una bas�ld���nda spawn i�lemi ger�ekle�ecek
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

            // Her bir particle kendi y�n�ne baks�n
            particle.transform.LookAt(transform.position);
            particle.transform.Rotate(new Vector3(0,90,0),Space.Self);
   
            yield return null; // Bir frame bekleyerek bir sonraki particle'� olu�tur
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
