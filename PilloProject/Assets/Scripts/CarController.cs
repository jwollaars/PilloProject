using UnityEngine;
using System.Collections;
using Pillo;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private int m_PlayerID;

    private float m_InputHorizontal;
    private float m_InputTriggerLeft;
    private float m_InputTriggerRight;

    private float m_Acceleration = 10f;
    private float m_CurrentAcceleration = 10f;
    private float m_DeAcceleration = 15f;
    private float m_CurrentDeAcceleration = 15f;

    private float m_MinMPH = 0f;
    public float m_CurrentMPH = 0f;
    public float m_TargetMPH = 0f;
    private float m_MaxMPH = 120f;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        m_InputHorizontal = Input.GetAxisRaw("Horizontal " + m_PlayerID);
        m_InputTriggerLeft = Input.GetAxisRaw("Left Trigger " + m_PlayerID);
        m_InputTriggerRight = Input.GetAxisRaw("Right Trigger " + m_PlayerID);

        m_TargetMPH = m_MaxMPH * m_InputTriggerRight;

        if (m_CurrentMPH < m_TargetMPH)
        {
            m_CurrentDeAcceleration = m_DeAcceleration;
            m_CurrentMPH += m_CurrentAcceleration;
            m_CurrentAcceleration = (m_Acceleration / m_CurrentMPH) * m_InputTriggerRight;
        }
        else if (m_CurrentMPH > m_TargetMPH)
        {
            m_CurrentAcceleration = m_Acceleration;
            m_CurrentMPH -= m_CurrentDeAcceleration;
            m_CurrentDeAcceleration = m_DeAcceleration / m_CurrentMPH;
        }

        if (m_InputTriggerLeft > 0.75f)
        {
            m_DeAcceleration *= 30f;
        }
        else
        {
            m_DeAcceleration /= 15f;
        }

        m_CurrentMPH = Mathf.Clamp(m_CurrentMPH, m_MinMPH, m_MaxMPH);

        transform.Translate((-transform.forward * m_CurrentMPH) * 0.5f * Time.deltaTime);
    }
}