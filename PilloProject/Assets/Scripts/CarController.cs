using UnityEngine;
using System.Collections;
using Pillo;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private int m_PlayerIDAccelerate;
    [SerializeField]
    private int m_PlayerIDSteer;

    private float m_PilloInputAccelerate;
    private float m_PilloInputSteer;

    private float m_Acceleration = 10f;
    private float m_CurrentAcceleration = 10f;
    private float m_DeAcceleration = 15f;
    private float m_CurrentDeAcceleration = 15f;

    private float m_MinMPH = 0f;
    public float m_CurrentMPH = 0f;
    public float m_TargetMPH = 0f;
    private float m_MaxMPH = 120f;

    private float m_SteerSpeed = 20f;
    private float m_MinRotation = -360f;
    private float m_CurrentRotation = 360f;
    private float m_MaxRotation = 360f;

    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = GameObject.Find("Scripts").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Steer();
    }

    private void Accelerate()
    {
        m_PilloInputAccelerate = m_GameManager.GetPilloSensorByID((PilloID)m_PlayerIDAccelerate);

        m_TargetMPH = m_MaxMPH * m_PilloInputAccelerate;

        if (m_CurrentMPH < m_TargetMPH)
        {
            m_CurrentDeAcceleration = m_DeAcceleration;
            m_CurrentMPH += m_CurrentAcceleration;
            m_CurrentAcceleration = (m_Acceleration / m_CurrentMPH) * m_PilloInputAccelerate;
        }
        else if (m_CurrentMPH > m_TargetMPH)
        {
            m_CurrentAcceleration = m_Acceleration;
            m_CurrentMPH -= m_CurrentDeAcceleration;
            m_CurrentDeAcceleration = m_DeAcceleration / m_CurrentMPH;
        }

        m_CurrentMPH = Mathf.Clamp(m_CurrentMPH, m_MinMPH, m_MaxMPH);

        transform.Translate((-transform.forward * m_CurrentMPH) * 0.5f * Time.deltaTime);
    }

    private void Steer()
    {
        m_PilloInputSteer = m_GameManager.GetPilloSensorByID((PilloID)m_PlayerIDSteer);

        //if (m_PilloInputSteer >= 0f && m_PilloInputSteer < 0.4f)
        //{
        //    if (m_CurrentRotation < -90f)
        //    {
        //        m_CurrentRotation -= m_SteerSpeed * Time.deltaTime;
        //    }
        //}
        //else if (m_PilloInputSteer > 0.6f && m_PilloInputSteer <= 1f)
        //{
        //    if (m_CurrentRotation < 90f)
        //    {
        //        m_CurrentRotation += m_SteerSpeed * Time.deltaTime;
        //    }
        //}

        

        m_CurrentRotation = 360f * m_PilloInputSteer;
        m_CurrentRotation = Mathf.Clamp(m_CurrentRotation, 0f, 360f);

        transform.rotation = Quaternion.Euler(new Vector3(0f, m_CurrentRotation, 0f));
    }
}