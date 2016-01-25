using UnityEngine;
using System.Collections;
using Pillo;

public class GameManager : MonoBehaviour
{
    private PilloController m_PC;

    [SerializeField][Range(0, 2f)]
    private int[] m_TeamSL = new int[2];
    private int[] m_TeamCL;

    void Start()
    {
        PilloController.ConfigureSensorRange(0x50, 0x6f);

        //m_TeamCL[0] = m_TeamSL[0];
        //m_TeamCL[1] = m_TeamSL[1];
    }

    void Update()
    {

    }

    public float GetPilloSensorByID(PilloID pilloID)
    {
        return PilloController.GetSensor(pilloID);
    }

    public int GetCurrentLaneByID(int ID)
    {
        return m_TeamCL[ID];
    }
}