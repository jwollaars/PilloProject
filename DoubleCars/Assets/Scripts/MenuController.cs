using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject m_Help;

    void Start()
    {
        m_Help.SetActive(false);
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenHelp()
    {
        if (m_Help.activeSelf)
        {
            m_Help.SetActive(false);
        }
        else
        {
            m_Help.SetActive(true);
        }
    }
}