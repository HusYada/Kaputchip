// https://docs.unity3d.com/ScriptReference/Color.Lerp.html
// https://docs.unity3d.com/ScriptReference/Random-value.html
using UnityEngine;

public class spray : MonoBehaviour
{
    Color lerpedColor, c1, c2;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        c1 = new Color(Random.value, Random.value, Random.value);
        c2 = new Color(Random.value, Random.value, Random.value);
    }

    void Update()
    {
        lerpedColor = Color.Lerp(c1, c2, Mathf.PingPong(Time.time, 1));
        rend.material.color = lerpedColor;
    }

    //add destroy coroutine
}