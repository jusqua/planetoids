using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float thrustSpeed;
    public float turnSpeed;

    private Rigidbody2D m_rigidbody;
    private bool m_thrusting;
    private float m_turnDirection;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        m_thrusting = Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed;
        m_turnDirection = 0.0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            m_turnDirection += 1.0f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            m_turnDirection -= 1.0f;
    }

    private void FixedUpdate()
    {
        if (m_thrusting)
        {
            m_rigidbody.AddForce(this.transform.up * thrustSpeed);
        }

        if (m_turnDirection != 0.0f)
        {
            m_rigidbody.AddTorque(m_turnDirection * turnSpeed);
        }
    }
}
