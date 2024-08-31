using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseIndicator : MonoBehaviour
{
    public Image icon;
    public Transform Target;
    public Vector3 Offset;

    private Transform player;

    private void Awake()
    {
        player = Camera.main.transform;
    }

    private void Update()
    {
        IndicateTargetPosition();
    }

    private void IndicateTargetPosition()
    {
        if (!Target)
            return;

        float minX = icon.GetPixelAdjustedRect().width / 2f;
        float maxX = Screen.width - minX;

        float minY = icon.GetPixelAdjustedRect().height / 2f;
        float maxY = Screen.height - minX;

        Vector2 pos = Camera.main.WorldToScreenPoint(Target.position) + Offset;

        if(Vector3.Dot((Target.position - player.position), player.forward) < 0)
        {
            pos.x = (pos.x < Screen.width / 2f) ? maxX : minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        icon.transform.position = pos;
    }
}
