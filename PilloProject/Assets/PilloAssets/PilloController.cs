//  PilloController.cs
//  PDK 3
//
//  Created by Salvatore CASTELLANO on 27-03-15.
//  Copyright (c) 2015 PILLO Games. All rights reserved.
//
using UnityEngine;
using Pillo;

public class PilloController : MonoBehaviour 
{
	protected static PilloReceiver m_receiver = null;
    public static PilloController pc;

    //This checks if the current PilloController is the first one.
    //If not it will dispose this PilloController.
    //If this is the first one it will make this the main PilloController.
    void Awake() {
        if(pc != null) {
            DestroyImmediate(this.gameObject);
        } else {
            pc = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

	// Do not change this code
	void Start()
	{
		if (m_receiver == null)
			m_receiver = new PilloReceiver();
		m_receiver.Connect();
	}
	void OnDestroy()
	{
        if(pc == this) {
		    // Never forget to Dispose the PilloReader!!!!
		    if (m_receiver != null)
			    m_receiver.Dispose();
		    m_receiver = null;
        }
	}
	// Below are the API calls that can be used
	public static void ConfigureSensorRange(int min, int max)
	{
		PilloSender.SensorMin = min;
		PilloSender.SensorMax = max;
	}
	public static int GetVersion(PilloID pillo)
	{
		return m_receiver.GetVersion(pillo);
	}
	public static string GetName(PilloID pillo)
	{
		return m_receiver.GetName(pillo);
	}
	public static void SetName(PilloID pillo, string name)
	{
		m_receiver.SetName(pillo, name);
	}
	public static PilloID GetPilloByName(string name)
	{
		return m_receiver.GetPilloByName(name);
	}
	public static bool isBatteryLow(PilloID pillo)
	{
		return m_receiver.GetBatteryLow(pillo);
	}
	public static bool isExternallyPowered(PilloID pillo)
	{
		return m_receiver.GetExternalPower(pillo);
	}
	public static string getUniqueID(PilloID pillo)
	{
		return m_receiver.GetUniqueID(pillo);
	}
	public static Vector4 GetSensors(PilloID pillo)
	{
		Vector4 vector = new Vector4();
		vector.x = m_receiver.GetSensor1(pillo);
		vector.y = m_receiver.GetSensor2(pillo);
		vector.z = m_receiver.GetSensor3(pillo);
		vector.w = m_receiver.GetSensor4(pillo);
		return vector;
	}
	public static float GetSensor(PilloID pillo)
	{
		return m_receiver.GetSensor(pillo, PilloSensor.Sensor1);
	}
	public static float GetSensor(PilloID pillo, PilloSensor sensor)
	{
		return m_receiver.GetSensor(pillo, sensor);
	}

	public static Vector3 GetAccelero(PilloID pillo)
	{
		Vector3 vector = new Vector3();
		vector.x = m_receiver.GetAcceleroX(pillo);
		vector.y = m_receiver.GetAcceleroY(pillo);
		vector.z = m_receiver.GetAcceleroZ(pillo);
		return vector;
	}
	public static float GetAcceleroX(PilloID pillo)
	{
		return m_receiver.GetAcceleroX(pillo);
	}
	public static float GetAcceleroY(PilloID pillo)
	{
		return m_receiver.GetAcceleroY(pillo);
	}
	public static float GetAcceleroZ(PilloID pillo)
	{
		return m_receiver.GetAcceleroZ(pillo);
	}
	// Included only for debugging purposes
	protected static int m_state = 0;
}
