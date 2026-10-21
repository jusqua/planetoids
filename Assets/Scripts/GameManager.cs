using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player playerRef;
    public ParticleSystem explosionPrefab;

    public float respawnTime = 3.0f;
    public float respawnInvulnerabilityTime = 3.0f;
    public int startupLives = 3;
    public int lives = 3;
    public int scoreWeight = 100;
    public int score = 0;

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
        Destroy(explosion.gameObject, explosion.main.duration);
    }

    public void PlanetoidDestroyed(Planetoid planetoid)
    {
        score += (int)(scoreWeight * (planetoid.maxSize - planetoid.size));
        InvokeExplosion(planetoid.transform.position);
    }

    public void PlayerDied()
    {
        lives--;
        if (lives <= 0)
        {
            Invoke(nameof(GameOver), respawnTime);
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
        lives = startupLives;
        score = 0;
        Respawn();
    }
}
