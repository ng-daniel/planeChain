using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class playerHealth : MonoBehaviour, IDamageable
{

    [SerializeField] int health;
    [SerializeField] float IFrameTime;
    [SerializeField] bool immune;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int i)
    {
        if (immune)
        {
            print("hit while immune");
            return;
        }
        health--;
        StartCoroutine(HitCoroutine());
        print(health);
        if (health == 0)
        {
            PlayerDead();
        }
    }
    IEnumerator HitCoroutine()
    {
        print("PLAYER IMMUNE");
        immune = true;
        yield return new WaitForSeconds(IFrameTime);
        immune = false;
        print("PLAYER NOT IMMUNE");
    }
    public void PlayerDead()
    {
        print("player dead");
        Destroy(gameObject);

    }

}
