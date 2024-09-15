using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionEffectScript : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public Animator anim;
    public int size;
    public string parent;

    public float time;
    public GameObject audioSource;

    void Awake()
    {
        anim.SetInteger("size", size);
        StartCoroutine(death());
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(audioSource, transform.position, transform.rotation);
        Quaternion quat = Quaternion.identity;
        quat.eulerAngles = new Vector3(0, 0, 45);
        transform.rotation = quat;

    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
