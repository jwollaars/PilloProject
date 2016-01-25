using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CarToAttach;

    [SerializeField]
    private Vector3 m_OffSet;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + m_OffSet.x, transform.position.y + m_OffSet.y, transform.position.z + m_OffSet.z);
    }
}