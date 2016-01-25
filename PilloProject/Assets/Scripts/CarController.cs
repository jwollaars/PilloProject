using UnityEngine;
using System.Collections;
using Pillo;

public class CarController : MonoBehaviour
{
    private float m_PilloInput;

    [SerializeField]
    private int m_PlayerID;

    private float m_Acceleration = 2f;
    private float m_DeAcceleration = 1f;

    private float m_MinMPH = 0f;
    public float m_CurrentMPH = 0f;
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

        if (m_PilloInput > 0.01f)
        {
            if (m_CurrentMPH < m_MaxMPH)
            {
                m_CurrentMPH += m_Acceleration;
            }
            else
            {
                m_CurrentMPH = m_MaxMPH;
            }
        }
        else if (m_PilloInput < 0.01f)
        {
            if (m_CurrentMPH > m_MinMPH)
            {
                m_CurrentMPH -= m_DeAcceleration;
            }
        }
    }
}