using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public bool IsGameStop = false; //oyun durdu mu diye bakıyoruz
    public GameObject pausePanel;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); 
        
    }


    private void Start()
    {
        if (pausePanel)
            pausePanel.SetActive(false);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        PausePanelOnOff();
    }

    public void PausePanelOnOff() //pausepaneliaç kapa fonksiyonu
    {
        if (GameManager.gameOver) //gameOver ı statik yaptığım için class adıyla erişebilirim sadece
        return;


        IsGameStop = !IsGameStop;

        if (pausePanel)
        {
            pausePanel.SetActive(IsGameStop);

            if (SoundManager.instance)
            {
                SoundManager.instance.OutVoiceEffect(0);
                Time.timeScale = (IsGameStop) ? 0 : 1;
            }

        }

    }

    public void PlayAgainFNC()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    
}
