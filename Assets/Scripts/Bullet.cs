using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public float speed = 500.0f;
    public float maxLifeTime = 10.0f;

    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    public void Project(Vector2 direction)
    {
        m_rigidbody.AddForce(direction * speed);
        Destroy(gameObject, maxLifeTime);
    }
}
