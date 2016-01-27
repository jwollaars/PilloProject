using UnityEngine;
using System.Collections;

public class RespawnController : MonoBehaviour
{
    private float m_RespawnTimer = 2f;
    private float m_TimerReload = 2;

    public void Respawn(GameObject carID, Vector3 respawnPos)
    {
        m_RespawnTimer -= Time.deltaTime;

        if (m_RespawnTimer <= 0)
        {
            carID.transform.position = respawnPos;
            m_RespawnTimer -= m_TimerReload;
        }
    }
}