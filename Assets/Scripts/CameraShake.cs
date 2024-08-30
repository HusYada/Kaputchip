using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool cameraShaking = false;
    public float duration = 1f;
    public float shakeMagnitude = 0.5f;
    public AnimationCurve curve;

    void Update() {
        if(cameraShaking){
            cameraShaking = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking(){
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + strength * shakeMagnitude * Random.insideUnitSphere;
            yield return null;
        }

        transform.position = startPosition;
    }
}
