using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float hiz = 5.0f; // Karakterin hızı
    public float ziplamaGucu = 10.0f; // Zıplama gücü 
    private float zaman = 0f; // Zamanı takip etmek için kullanacağımız değişken
    public float energy = 100f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool ziplamaYapabilir = true; // Zıplama yapıp yapamayacağını kontrol etmek için kullanılan flag

    private Animator animator; // Animator bileşeni

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Animator bileşenini al
    }

    void FixedUpdate()
    {
        float yatay = Input.GetAxis("Horizontal"); // Yatay giriş al
        float dikey = Input.GetAxis("Vertical"); // Dikey giriş al

        Vector2 hareket = new Vector2(yatay * hiz, rb.velocity.y); // Yatay hareketi ve mevcut dikey hızı hesapla
        rb.velocity = hareket; // Rigidbody2D bileşenine hareketi uygula

        // Koşma animasyonunu çalıştır
        if (yatay != 0)
        {
            animator.SetBool("Kosuyor", true); // "Kosuyor" parametresini true olarak ayarla
        }
        else
        {
            animator.SetBool("Kosuyor", false); // "Kosuyor" parametresini false olarak ayarla
        }



        // Yatay girişe göre karakterin yüzünü döndür
        if (yatay > 0)
        {
            spriteRenderer.flipX = false; // Sağa dönük yüzü
        }
        else if (yatay < 0)
        {
            spriteRenderer.flipX = true; // Sola dönük yüzü
        }

        // Her saniye energyyi arttırın
        energy += 25f * Time.deltaTime;


        // energyyi 100 ile sınırlandırın
        energy = Mathf.Clamp(energy, 0, 100);
    }


    void Update()
    {   
        

        if (Input.GetKeyDown(KeyCode.W) && ziplamaYapabilir)
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu); // Zıplama gücü ile yukarı doğru hızı ayarla
            ziplamaYapabilir = false; // Zıplama yapma yeteneğini kapat
            animator.SetBool("Up", true); // "Up" adlı bool değişkeni true olarak ayarla
        }
        
        if (rb.velocity.y < 0 && !ziplamaYapabilir)
        {
        // Düşme başladığında "Up" parametresini false ve "Down" parametresini true yap
        animator.SetBool("Up", false); // "Up" adlı bool değişkeni false olarak ayarla
        animator.SetBool("Down", true); // "Down" adlı bool değişkeni true olarak ayarla
        }



        if (Input.GetKeyDown(KeyCode.Space) && energy >= 50)
        {
            zaman = Time.time; // Şu anki zamanı kaydediyoruz
            hiz = 20f;
            energy -= 50f;
            ziplamaYapabilir = true;
            animator.SetBool("Up", false); // "Up" adlı bool değişkeni false olarak ayarla
            animator.SetBool("Down", false); // "Down" adlı bool değişkeni false olarak ayarla
            animator.SetTrigger("Dash");
        }

        // Yarım saniye (0.5 saniye) geçtiyse
        if (Time.time - zaman >= 0.2f)
        {
            hiz = 5f; // Hızı sıfırlıyoruz
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Yere temas ettiğinde zıplama yapma yeteneğini aç
        if (collision.gameObject.CompareTag("Zemin"))
        {
            ziplamaYapabilir = true;
            animator.SetBool("Up", false); // "Up" adlı bool değişkeni false olarak ayarla
            animator.SetBool("Down", false); // "Down" adlı bool değişkeni false olarak ayarla

        }

        // Yere temas ettiğinde zıplama yapma yeteneğini aç
        if (collision.gameObject.CompareTag("Duvar"))
        {
            ziplamaYapabilir = true;
            animator.SetBool("Up", false); // "Up" adlı bool değişkeni false olarak ayarla
            animator.SetBool("Down", false); // "Down" adlı bool değişkeni false olarak ayarla

        }
    }
}



