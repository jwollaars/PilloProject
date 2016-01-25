using UnityEngine;
using System.Collections;
using Pillo;

public class GameManager : MonoBehaviour
{
    private PilloController m_PC;

    void Start()
    {
        PilloController.ConfigureSensorRange(0x50, 0x6f);
    }

    void FixedUpdate()
    {

    }

    public float GetPilloSensorByID(PilloID pilloID)
    {
        return PilloController.GetSensor(pilloID);
    }
}
