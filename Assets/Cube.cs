using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2000)]
public class Cube : MonoBehaviour
{
    [SerializeField] MeshRenderer m_MeshRenderer;

    [SerializeField] Material[] m_Material;

    public bool isBlue = false;

    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
        isBlue = FirebaseManager._instance.isBlue;
        if (!isBlue)
        {
            m_MeshRenderer.material = m_Material[0];
        }
        else
        {
            m_MeshRenderer.material = m_Material[1];
        }
       
    }

    void Update()
    {
    }
}
