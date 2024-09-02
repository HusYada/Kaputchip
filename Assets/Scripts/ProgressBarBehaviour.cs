using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarBehaviour : MonoBehaviour
{
    // Public float for 0 to 1 for progress.
    // Public renderer material for switching material.
    [Range(0f, 1f), SerializeField]
    private float progressValue = 0f;

    public Transform ProgressBar;
    public Renderer BarRenderer;

    public void SetValue(float value)
    {
        progressValue = Mathf.Clamp(value, 0f, 1f);
        UpdateProgress();
    }

    public void SetMaterial(Material mat) => BarRenderer.material = mat;

    private void UpdateProgress()
    {
        ProgressBar.localScale = new Vector3(progressValue, 1f, 1f);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateProgress();
    }
#endif
}
