using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private PlayerIndex m_PlayerIndex;
    private bool m_PlayerIndexSet = false;
    private GamePadState m_State;
    private GamePadState m_PrevState;

    private float m_InputHorizontal;
    private float m_InputRightTrigger;
    private float m_InputLeftTrigger;
    private ButtonState m_InputX;

    private bool m_IsGrounded = true;
    private float m_RayLenght = 2f;

    private Rigidbody m_Rigidbody;

    private float m_Acceleration = 2800f;
    private float m_DeAcceleration = 1400f;

    private bool m_Respawn = false;
    private float m_CurrentRespawnTimer = 3f;
    private float m_RespawnTimer = 3f;

    [SerializeField]
    private GameObject m_StartRespawnPoint;
    private Stack<GameObject> m_RespawnPoints;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        m_RespawnPoints = new Stack<GameObject>();
        m_RespawnPoints.Push(m_StartRespawnPoint);
    }

    private void Update()
    {
        if (!m_PlayerIndexSet || !m_PrevState.IsConnected)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)m_PlayerIndex;
            GamePadState testState = GamePad.GetState(testPlayerIndex);

            if (testState.IsConnected)
            {
                m_PlayerIndex = testPlayerIndex;
                m_PlayerIndexSet = true;
            }
        }

        m_PrevState = m_State;
        m_State = GamePad.GetState(m_PlayerIndex);
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Gas();
        Steer();
        Vibration();
        Respawn();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Respawn Point")
        {
            m_RespawnPoints.Push(collider.gameObject);
        }
    }

    private void OnDisable()
    {
        GamePad.SetVibration(m_PlayerIndex, 0f, 0f);
    }

    private void GroundCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, m_RayLenght))
        {
            if (hit.collider.tag == "Ground")
            {
                m_IsGrounded = true;
            }
        }
        else
        {
            m_IsGrounded = false;
        }
    }

    private void Gas()
    {
        if (!InputBlock())
        {
            m_InputRightTrigger = m_PrevState.Triggers.Right;
            m_InputLeftTrigger = m_PrevState.Triggers.Left;
            m_InputX = m_PrevState.Buttons.X;
        }

        if (m_IsGrounded)
        {
            m_Rigidbody.AddForce(transform.forward * m_Acceleration * m_InputRightTrigger * Time.deltaTime, ForceMode.Acceleration);
            m_Rigidbody.AddForce(-transform.forward * m_DeAcceleration * m_InputLeftTrigger * Time.deltaTime, ForceMode.Acceleration);
        }

        if (m_InputX == ButtonState.Pressed)
        {
            // m_Respawn = true;
        }
    }

    private void Steer()
    {
        if (!InputBlock())
        {
            if (m_InputLeftTrigger == 0f)
            {
                m_InputHorizontal = m_PrevState.ThumbSticks.Left.X;
            }
            else if (m_InputLeftTrigger > 0.01f)
            {
                m_InputHorizontal *= m_PrevState.ThumbSticks.Left.X * -1;
            }
        }

        if (Mathf.Abs(m_Rigidbody.velocity.magnitude) >= 5f)
        {
            float rotateAmount = ((m_InputHorizontal * 100f) / (m_Rigidbody.velocity.magnitude / 15f)) * Time.deltaTime;
            rotateAmount = Mathf.Clamp(rotateAmount, -4f, 4f);

            transform.Rotate(new Vector3(0f, rotateAmount, 0f));
        }
    }


    private bool InputBlock()
    {
        if (m_IsGrounded)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Vibration()
    {
        if (m_InputRightTrigger > 0.01f && m_IsGrounded)
        {
            GamePad.SetVibration(m_PlayerIndex, 0.5f, 0f);
        }
        else if (m_IsGrounded)
        {
            GamePad.SetVibration(m_PlayerIndex, 0.1f, 0f);
        }
        else
        {

        }
    }

    private void Respawn()
    {
        if (m_Respawn)
        {
            if (m_CurrentRespawnTimer > 0f)
            {
                m_CurrentRespawnTimer -= Time.deltaTime;
            }

            if (m_CurrentRespawnTimer <= 0f)
            {
                transform.position = m_RespawnPoints.Peek().transform.position;
                transform.rotation = m_RespawnPoints.Peek().transform.rotation;
                m_Respawn = false;
                m_CurrentRespawnTimer = m_RespawnTimer;
            }
        }
    }
}