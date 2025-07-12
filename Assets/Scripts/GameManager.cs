using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> prefabs; // Prefab objelerin listesi
    public float minX, maxX; // X ekseni için minimum ve maksimum konumlar
    public float spawnY; // Y ekseni için sabit konum
    public float fallSpeed = 5f; // Düşme hızı



    public TextMeshProUGUI score_text;
    public TextMeshProUGUI bestscore_text;
    
    public Collect cll;
    public GameObject gameover;

    public int bestScore;





    void Start()
    {
        Time.timeScale = 1f;
        // Belirli bir süre boyunca fonksiyonu tekrarlayarak obje spawn etme işlemini başlatıyoruz
        StartCoroutine(SpawnObjectCoroutine());
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

    }


    void Update()
    {


        

        score_text.text = "Your Score: " + cll.score.ToString();
        bestscore_text.text = "Your Best Score: " + bestScore.ToString(); 

        if(cll.hp <= 0)
        {   
            Time.timeScale = 0f;
            gameover.SetActive(true);
            if (cll.score > bestScore)
            {
            // Eğer şuanki skor, en iyi skoru geçtiyse, bestScore'i güncelleme
            bestScore = cll.score;

            // PlayerPrefs ile bestScore'u kaydetme
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); // Değişiklikleri kaydetme
            }
        }

    }
    

    IEnumerator SpawnObjectCoroutine()
    {
        // İlk 3 saniye boyunca spawn etmeyi durduruyoruz
        yield return new WaitForSeconds(3f);

        // Sonsuz bir döngü içinde obje spawn etme işlemini gerçekleştiriyoruz
        while (true)
        {
            // Listedeki prefab objelerden rastgele birini seçiyoruz
            GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];

            // Sabit bir Y (Yatay) düzlemde belirlediğimiz X (yatay) aralığında nesne oluşturuyoruz
            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnY, 0);
            GameObject spawnedObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

            // Nesneye ait Rigidbody2D bileşenini alıyoruz
            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            // Nesnenin yerçekimi hızını sıfırlıyoruz
            rb.gravityScale = 0;

            // Nesneye düşme hızını uyguluyoruz
            rb.velocity = new Vector2(0, -fallSpeed);

            // 1 saniye bekleme
            yield return new WaitForSeconds(1f);
        }
    }




    
}
