using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButtonBehaviour : MonoBehaviour
{
    private Button m_CmptButton;

    void Start()
    {
        m_CmptButton = GetComponent<Button>();
        m_CmptButton.onClick.AddListener(ChangeToMainScene);
    }

    private void ChangeToMainScene()
    {
        SceneManager.LoadScene("Epikemon");
    }
}
