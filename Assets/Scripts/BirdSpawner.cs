using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;

    private void Start()
    {
        // InvokeRepeating metodu ile belirli aralıklarla SpawnBird metodu çağrılıyor
        InvokeRepeating("SpawnBird", spawnInterval, spawnInterval);
    }

    private void SpawnBird()
    {
        if (spawnPoints.Length > 0)
        {
            // spawnPoints dizisinden rastgele bir eleman seçiliyor
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // spawnPoint pozisyonunda bir kuş oluşturuluyor
            Instantiate(birdPrefab, spawnPoint.position, spawnPoint.rotation, transform);
        }
    }
}
