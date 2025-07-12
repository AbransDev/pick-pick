using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    private Animator anim; // Animator bileşeni

    
  


    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Destroy");

            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            AnimationClip Destroy = System.Array.Find(clips, clip => clip.name == "Destroy");
            float animSuresi = Destroy.length;
            StartCoroutine(DestroyTime(animSuresi));


        }

        if (other.CompareTag("Zemin"))
        {
            Destroy(gameObject);
           
        }
    }

     private IEnumerator DestroyTime(float sure)
    {
        yield return new WaitForSeconds(sure);

        Destroy(gameObject);
    }

}