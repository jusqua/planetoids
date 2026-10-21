using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Planetoid : MonoBehaviour
{
    public float size;

    private LineRenderer m_lineRenderer;
    private Rigidbody2D m_rigidbody;

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
        transform.localScale = Vector3.one * size;
        m_rigidbody.mass = size;
    }

    private void Update()
    {

    }
}
