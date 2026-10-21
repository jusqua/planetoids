using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustSpeed;
    public float turnSpeed;

    private LineRenderer m_lineRenderer;
    private Rigidbody2D m_rigidbody;
    private bool m_thrusting;
    private float m_turnDirection;
    private float m_offset;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_lineRenderer = GetComponent<LineRenderer>();
        m_offset = m_lineRenderer.GetPosition(0).y;
    }

    private void Update()
    {
        m_thrusting = Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed;
        m_turnDirection = 0.0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            m_turnDirection += 1.0f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            m_turnDirection -= 1.0f;
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            Shoot();
    }

    private void FixedUpdate()
    {
        if (m_thrusting)
            m_rigidbody.AddForce(transform.up * thrustSpeed);

        if (m_turnDirection != 0.0f)
            m_rigidbody.AddTorque(m_turnDirection * turnSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planetoid"))
        {
            m_rigidbody.angularVelocity = 0;
            m_rigidbody.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
            GameManager.Instance.PlayerDied();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position + transform.up * m_offset, transform.rotation).Project(transform.up);
    }
}
