using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWire
{
    private LineRenderer m_lineRenderer;
    private Vector3 posOffset = new Vector3(-68, 0, 0);

    public NewWire(LineRenderer lineRenderer, Vector3 pos)
    {
        m_lineRenderer = lineRenderer;
        m_lineRenderer.startWidth = 32;
        m_lineRenderer.endWidth = 32;
        
        m_lineRenderer.positionCount = 1;
        m_lineRenderer.SetPosition(0, pos + posOffset);
    }

    public void IncreaseTo(Vector3 pos)
    {
        var positionCount = m_lineRenderer.positionCount;
        m_lineRenderer.SetPosition(positionCount-1, pos + posOffset);
    }

    public void AddWireNode()
    {
        var lastpoint = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1);
        m_lineRenderer.positionCount++;
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount-1, lastpoint);
    }
}
