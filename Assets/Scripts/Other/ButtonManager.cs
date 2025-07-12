using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class ButtonManager : MonoBehaviour
{
    public GameObject gameover;
    public GameObject Buttons;
    public GameObject Settings;
    public GameObject Pause;


    public Slider musicSlider;
    public Slider soundSlider;
    public float musicSize, soundSize;

    public GameObject MusicBox;
    public GameObject Pick;

    private bool isPaused = false;

    public GameManager gm;
    public Collect cll;

    public int bestScore;
    public TextMeshProUGUI bestscore_text;




    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicSize");
        soundSlider.value = PlayerPrefs.GetFloat("SoundSize");
        bestScore = PlayerPrefs.GetInt("BestScore");


    }

    void Update()
    {


        bestscore_text.text = "Best Score: " + bestScore.ToString(); 
        musicSize = musicSlider.value;
        soundSize = soundSlider.value;

        PlayerPrefs.SetFloat("MusicSize", musicSize);
        PlayerPrefs.SetFloat("SoundSize", soundSize);

        AudioSource music = MusicBox.GetComponent<AudioSource>();
        music.volume = musicSize;

        AudioSource pick = Pick.GetComponent<AudioSource>();
        pick.volume = soundSize;


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == "SampleScene" && cll.hp > 0)
        {   

            // ESC tuşuna basıldığında oyun durumu değiştirilir ve pause paneli aktif veya pasif hale getirilir
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f; // Oyun zaman ölçeğini ayarla (0f = duraklatılmış, 1f = normal hız)
            Pause.SetActive(isPaused); // Pause panelini aktif veya pasif hale getir
            Settings.SetActive(false); // Pause panelini aktif veya pasif hale getir    
            
        }

    

        
    }



    public void play_btn()
    {
        SceneManager.LoadScene(1);
    }

    public void quit_btn()
    {
        Application.Quit();
    }


    public void restart_btn()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        gameover.SetActive(false);
    }


    public void Settings_btn()
    {

        Buttons.SetActive(false);
        Settings.SetActive(true);
    }

     public void Back_btn()
    {

        Buttons.SetActive(true);
        Settings.SetActive(false);
    }



    public void Settings_btn2()
    {

        Pause.SetActive(false);
        Settings.SetActive(true);
    }

    public void Back_btn2()
    {

        Pause.SetActive(true);
        Settings.SetActive(false);
    }

    public void Resume()
    {
        isPaused = !isPaused;
        Time.timeScale = 1f;
        Pause.SetActive(false);
    }

     public void mmenu_btn()
    {
        isPaused = !isPaused;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void playAgain_btn()
    {   
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
        
    }



}