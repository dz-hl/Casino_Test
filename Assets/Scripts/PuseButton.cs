using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuseButton : MonoBehaviour
{
    public Button pButton;
    [SerializeField] GameObject panel;
    private Scene scene;
    void Start()
    {
        Button button = pButton.GetComponent<Button>();
        button.onClick.AddListener(Pause);
        button.onClick.AddListener(Continue);
        button.onClick.AddListener(ToMainMenu);
        panel.SetActive(false);
    }

    public void Pause()
    {
        panel.SetActive(true);
    }

    public void Continue()
    {
        panel.SetActive(false);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
