using UnityEngine;

public class HeadControl : MonoBehaviour
{
    public GameObject player; 
    public Animator anim;
    public mouse mos; 
    public float speed;
    public float eyemin, eyemax;
    public Vector3 eyespd;
    public GameObject eyeR;
    public GameObject eyeL;
    public bool ikActive = true; 
    public bool notpickup = true; 

    void Start()
    {
        if (notpickup)
        {
            anim = GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        if (mos.behaviour == 1 && eyeR.transform.localScale.y > eyemin)
        {
            eyeL.transform.localScale -= eyespd;
            eyeR.transform.localScale -= eyespd;
        }
        if (mos.behaviour != 1 && eyeR.transform.localScale.y < eyemax)
        {
            eyeL.transform.localScale += eyespd;
            eyeR.transform.localScale += eyespd;
        }
    }

    void Update()
    {
        if (!ikActive)
        {
            transform.LookAt(player.transform);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (anim && notpickup)
        {
            if (ikActive)
            {
                if (player != null)
                {
                    anim.SetLookAtWeight(2);
                    anim.SetLookAtPosition(player.transform.position);
                }
            }
        }
    }
}
