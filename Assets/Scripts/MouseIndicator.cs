using UnityEngine;
using UnityEngine.UI;

public class MouseIndicator : MonoBehaviour
{
    public Image icon;
    public RectTransform Psuedo, Directional, IconDisplay, ArrowDisplay;
    public Transform Target;
    public float ArrowDistance = 120f, IconDistance = 80f;

    public Sprite[] iconSprites;

    private Transform player;
    private bool isInside = false;
    //private AudioSource aud;

    private void Awake()
    {
        player = Camera.main.transform;
    }

    private void Update()
    {
        IndicateTargetPosition();
        DirectionalIndicator();
        Display();
    }

    public void SetIcon(Sprite img) => icon.sprite = img;
    public void SetIcon(int index) => icon.sprite = iconSprites[index];

    private void IndicateTargetPosition()
    {
        if (!Target)
            return;

        float minX = icon.GetPixelAdjustedRect().width / 2f;
        float maxX = Screen.width - minX;

        float minY = icon.GetPixelAdjustedRect().height / 2f;
        float maxY = Screen.height - minX;

        Vector2 pos = Camera.main.WorldToScreenPoint(Target.position);

        pos = Limitation(pos, new Vector2(minX, minY), new Vector2(maxX, maxY));

        Psuedo.transform.position = pos;
    }

    private Vector2 Limitation(Vector2 pos, Vector2 min, Vector2 max)
    {
        if (Vector3.Dot((Target.position - player.position), player.forward) < 0)
        {
            pos.x = (pos.x < Screen.width / 2f) ? max.x : min.x;
        }

        
        // Limint inside screen
        //pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        //pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        

        bool insideX = (pos.x > min.x && pos.x < max.x) ? true : false;
        bool insideY = (pos.y > min.y && pos.y < max.y) ? true : false;

        isInside = (insideX && insideY) ? true : false;

        return pos;
    }

    private void DirectionalIndicator()
    {
        
        Vector2 direction = Psuedo.position - Directional.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Directional.rotation = Quaternion.Euler(0, 0, angle);

        IconDisplay.localPosition = Vector3.up * IconDistance;
        ArrowDisplay.localPosition = Vector3.up * ArrowDistance;
        icon.transform.position = IconDisplay.position;
    }

    private void Display()
    {
        if(isInside)
        {
            icon.transform.localScale = Vector3.zero;
            ArrowDisplay.localScale = Vector3.zero;
        }
        else
        {
            icon.transform.localScale = Vector3.one;
            ArrowDisplay.localScale = Vector3.one;
        }
    }
}
