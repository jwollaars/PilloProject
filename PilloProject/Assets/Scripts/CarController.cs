using UnityEngine;
using System.Collections;
using Pillo;

public class CarController : MonoBehaviour
{
    private float m_PilloInput;

    [SerializeField]
    private int m_PlayerID;

    private float m_Acceleration = 10f;
    private float m_CurrentAcceleration = 10f;
    private float m_DeAcceleration = 10f;
    private float m_CurrentDeAcceleration = 10f;

    private float m_MinMPH = 0f;
    public float m_CurrentMPH = 0f;
    public float m_TargetMPH = 0f;
    private float m_MaxMPH = 120f;

    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        m_PilloInput = m_GameManager.GetPilloSensorByID((PilloID)m_PlayerID);

        m_TargetMPH = m_MaxMPH * m_PilloInput;

        if (m_CurrentMPH < m_TargetMPH)
        {
            m_CurrentDeAcceleration = m_DeAcceleration;
            m_CurrentMPH += m_CurrentAcceleration;
            m_CurrentAcceleration = (m_Acceleration / m_CurrentMPH) * m_PilloInput;
        }
        else if (m_CurrentMPH > m_TargetMPH)
        {
            m_CurrentAcceleration = m_Acceleration;
            m_CurrentMPH -= m_CurrentDeAcceleration;
            m_CurrentDeAcceleration = m_DeAcceleration / m_CurrentMPH;
        }

        if (m_CurrentMPH <= m_MinMPH)
        {
            m_CurrentMPH = m_MinMPH;
        }
    }
}