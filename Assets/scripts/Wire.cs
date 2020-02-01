using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Wire : MonoBehaviour
{
    
    private LineRenderer m_lineRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.startWidth = 8;
        m_lineRenderer.endWidth = 8;
        //color of wire
//        m_lineRenderer.material.color = Color.blue;
    }

    void Start()
    {
        m_lineRenderer.positionCount = 1;
        m_lineRenderer.SetPosition(0, this.transform.position);
    }

    // Update is called once per frame
    public void IncreaseTo(Vector3 pos)
    {
        var positionCount = m_lineRenderer.positionCount;
        positionCount++;
        m_lineRenderer.positionCount = positionCount;
        m_lineRenderer.SetPosition(positionCount-1, pos);
        
    }
}
