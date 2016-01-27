using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CarToAttach;

    [SerializeField]
    private float m_Distance;
    [SerializeField]
    private float m_Height;
    [SerializeField]
    private float m_LerpSpeed;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(GetBehindPosition(m_CarToAttach, m_Distance).x, GetBehindPosition(m_CarToAttach, m_Distance).y + m_Height, GetBehindPosition(m_CarToAttach, m_Distance).z), m_LerpSpeed * Time.deltaTime);

        transform.LookAt(Vector3.Lerp(transform.position, m_CarToAttach.transform.position, m_LerpSpeed * Time.deltaTime));
    }

    private Vector3 GetBehindPosition(GameObject target, float distance)
    {
        return target.transform.position - target.transform.forward * distance;
    }
}