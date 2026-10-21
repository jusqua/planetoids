using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Planetoid planetoidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void Spawn()
    {
        for (var i = 0; i < spawnAmount; i++)
        {
            var spawnDirection = (Vector3)(Random.insideUnitCircle.normalized * spawnDistance);
            var spawnPosition = transform.position + spawnDirection;
            var spawnRotation = Quaternion.AngleAxis(Random.Range(-trajectoryVariance, trajectoryVariance), Vector3.forward);
            var planetoid = Instantiate(planetoidPrefab, spawnPosition, spawnRotation);
            planetoid.size = Random.Range(planetoid.minSize, planetoid.maxSize);
            planetoid.SetTrajectory(spawnRotation * -spawnDirection);
        }
    }
}
