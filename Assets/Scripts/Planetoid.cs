using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Planetoid : MonoBehaviour
{
    public float speed = 10.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float maxLifeTime = 30.0f;
    public float size
    {
        get
        {
            return m_size;
        }
        set
        {
            m_size = value;
            transform.localScale = Vector2.one * m_size;
            m_rigidbody.mass = m_size;
        }
    }

    private LineRenderer m_lineRenderer;
    private Rigidbody2D m_rigidbody;
    private float m_size;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        var positions = new Vector3[m_lineRenderer.positionCount];
        m_lineRenderer.GetPositions(positions);
        m_lineRenderer.SetPositions(positions.Select(position => position * Random.Range(0.5f, 1.0f)).ToArray());
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
    }

    public void SetTrajectory(Vector2 direction)
    {
        m_rigidbody.AddForce(direction * speed);

        Destroy(gameObject, maxLifeTime);
    }
}
