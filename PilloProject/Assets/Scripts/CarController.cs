using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    private float m_InputHorizontal;
    private float m_InputVertical;

    private bool m_BlockInput;

    private float m_Acceleration = 20f;
    private float m_CurrentAcceleration = 20f;
    private float m_DeAcceleration = 40f;
    private float m_CurrentDeAcceleration = 40f;

    private float m_MinMPH = 0f;
    private float m_CurrentMPH = 0f;
    private float m_MaxMPH = 120f;
    private float m_TargetMPH = 0f;

    private bool m_Respawn = false;
    private float m_CurrentRespawnTimer = 3f;
    private float m_RespawnTimer = 3f;

    [SerializeField]
    private GameObject m_StartRespawnPoint;
    private Stack<GameObject> m_RespawnPoints;

    private void Start()
    {
        m_RespawnPoints = new Stack<GameObject>();
        m_RespawnPoints.Push(m_StartRespawnPoint);
    }

    private void FixedUpdate()
    {
        GasAndSteer();
        Respawn();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Respawn Point")
        {
            m_RespawnPoints.Push(collider.gameObject);
            Debug.Log(m_RespawnPoints.Peek().name);
        }
    }

    private void GasAndSteer()
    {
        if (!m_BlockInput)
        {
            m_InputHorizontal = Input.GetAxisRaw("Horizontal");
            m_InputVertical = Input.GetAxisRaw("Vertical");


            m_TargetMPH = m_MaxMPH * m_InputVertical;

            if (m_InputVertical != 0f)
            {
                if (m_InputVertical >= 0.01f)
                {
                    if (m_CurrentMPH < m_TargetMPH)
                    {
                        m_CurrentMPH += m_CurrentAcceleration;
                        m_CurrentAcceleration = (m_Acceleration / m_CurrentMPH) * m_InputVertical;
                        m_CurrentDeAcceleration = m_DeAcceleration;
                    }
                }
                else if (m_InputVertical <= -0.01f)
                {
                    //if (m_CurrentMPH > m_TargetMPH)
                    //{
                    //    m_CurrentMPH -= m_CurrentAcceleration;
                    //    m_CurrentAcceleration = (m_Acceleration / m_CurrentMPH) * m_InputVertical;
                    //    m_CurrentDeAcceleration = m_DeAcceleration;
                    //}
                }
            }
            else if (m_InputVertical == 0f)
            {
                if (m_CurrentMPH != m_TargetMPH)
                {
                    m_CurrentMPH -= m_CurrentDeAcceleration;
                    m_CurrentDeAcceleration = m_DeAcceleration / m_CurrentMPH;
                    m_CurrentAcceleration = m_Acceleration;
                }
            }

            if (m_InputHorizontal != 0f)
            {
                transform.Rotate(new Vector3(0f, m_InputHorizontal * 100f * Time.deltaTime, 0f));
            }

            if (m_CurrentMPH > 0.01f)
            {
                if (m_CurrentMPH <= m_MinMPH)
                {
                    m_CurrentMPH = m_MinMPH;
                }
                else if (m_CurrentMPH >= m_MaxMPH)
                {
                    m_CurrentMPH = m_MaxMPH;
                }
            }
            else if (m_CurrentMPH < -0.01f)
            {
                if (m_CurrentMPH <= -m_MinMPH)
                {
                    m_CurrentMPH = -m_MinMPH;
                }
                else if (m_CurrentMPH >= -m_MaxMPH)
                {
                    m_CurrentMPH = -m_MaxMPH;
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                m_Respawn = true;
            }

            transform.Translate(Vector3.forward * m_CurrentMPH * Time.deltaTime);
        }
    }

    private void Respawn()
    {
        if (m_Respawn)
        {
            m_BlockInput = true;

            if (m_CurrentRespawnTimer > 0f)
            {
                m_CurrentRespawnTimer -= Time.deltaTime;
            }

            if (m_CurrentRespawnTimer <= 0f)
            {
                m_BlockInput = false;
                transform.position = m_RespawnPoints.Peek().transform.position;
                transform.rotation = m_RespawnPoints.Peek().transform.rotation;
                m_Respawn = false;
                m_CurrentRespawnTimer = m_RespawnTimer;
            }
        }
    }
}