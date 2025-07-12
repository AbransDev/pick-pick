using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Collect : MonoBehaviour
{
    public PlayerMovement plMove;
    public Image healthBar;
    public Image energyBar;
    public TextMeshProUGUI scoreText;

    public float hp = 100f;
    public float initHealth;
    public float initEnergy;

    public int score = 0;
    private float startTime;
    private bool startDelay = true;

    public GameObject Pick;
    private AudioSource pickSound;

    void Start()
    {
        initHealth = hp;
        initEnergy = plMove.energy;
        startTime = Time.time; // Oyun süresini başlangıç zamanı olarak kaydet
        pickSound = Pick.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            pickSound.Play();
            score++;
            hp += 12f;
        }
    }

    void FixedUpdate()
    {
        if (startDelay)
        {
            if (Time.time - startTime < 3f) // Başlangıç zamanından itibaren 3 saniye geçmediyse can azalmasını engelle
            {
                return;
            }
            else
            {
                startDelay = false;
            }
        }

        hp -= 10f * Time.deltaTime;
        hp = Mathf.Clamp(hp, 0, 100);
    }

    void Update()
    {
        healthBar.fillAmount = hp / initHealth;
        energyBar.fillAmount = plMove.energy / initEnergy;
        scoreText.text = score.ToString();
    }
}
