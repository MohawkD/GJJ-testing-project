using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWire
{
    private LineRenderer m_lineRenderer;

    public NewWire(LineRenderer lineRenderer, Vector3 pos)
    {
        m_lineRenderer = lineRenderer;
        m_lineRenderer.startWidth = 4;
        m_lineRenderer.endWidth = 4;
        
        m_lineRenderer.positionCount = 1;
        m_lineRenderer.SetPosition(0, pos);
    }

    public void IncreaseTo(Vector3 pos)
    {
        var positionCount = m_lineRenderer.positionCount;
        m_lineRenderer.SetPosition(positionCount-1, pos);
    }

    public void AddWireNode()
    {
        m_lineRenderer.positionCount++;
    }
}
