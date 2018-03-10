
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public Collider2D spawnCollider;

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", Random.Range(1, 2f), Random.Range(3f, 6f));
    }
    Vector2 RandomPosition()
    {
        return new Vector2(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            Random.Range(spawnCollider.bounds.min.y, spawnCollider.bounds.max.y)
        );

    }
    void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, RandomPosition(), Quaternion.identity);
    }
}
