﻿using System.Collections;
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
    }

    void Start()
    {
        m_lineRenderer.SetPosition(0, this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        var positionCount = m_lineRenderer.positionCount;
        positionCount++;
        m_lineRenderer.positionCount = positionCount;
        m_lineRenderer.SetPosition(positionCount-1, this.transform.position);
    }
}