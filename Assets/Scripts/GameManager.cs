using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player playerRef;
    public ParticleSystem explosionPrefab;

    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int lives = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InvokeExplosion(Vector3 position)
    {
        var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion.gameObject, explosion.main.duration + explosion.main.startLifetime.constantMax);
    }

    public void PlanetoidDestroyed(Planetoid planetoid)
    {
        InvokeExplosion(planetoid.transform.position);
    }

    public void PlayerDied()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Invoke(nameof(Respawn), respawnTime);
        }
        InvokeExplosion(playerRef.transform.position);
    }

    private void Respawn()
    {
        playerRef.transform.position = Vector3.zero;
        playerRef.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        playerRef.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnInvulnerabilityTime);
    }

    private void TurnOnCollisions()
    {
        playerRef.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        // TODO
    }
}
