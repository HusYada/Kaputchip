using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public GameObject fractured;
    public float breakForce = 100f;
    public CameraShake cameraShake;
    public float delay = 1f; 
    [SerializeField] ParticleSystem BreakParticle = null;
    [SerializeField] ParticleSystem BombParticle = null;
    public AudioSource audioSource;
    public AudioClip breakSound;



    void Start() {
        //StartCoroutine(BreakTheThingWithDelay());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(BreakTheThingWithDelay());
        }
    }

    IEnumerator BreakTheThingWithDelay() {
        yield return new WaitForSeconds(delay);

        GameObject frac = Instantiate(fractured, transform.position, transform.rotation);

        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>()) {
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }

        // CameraShake
        //if (cameraShake != null) {
            //cameraShake.cameraShaking = true;
        //}
        BreakParticleAndSound();

        Destroy(gameObject);
    }

    void BreakParticleAndSound(){
        //Play Particle
        BreakParticle.Play();
        BombParticle.Play();
        //Play Sound
        if (audioSource != null) {
            if (breakSound != null) {
                audioSource.PlayOneShot(breakSound);
            }
        }
    }
}

