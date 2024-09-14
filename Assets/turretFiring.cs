using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretFiring : MonoBehaviour
{

    public GameObject bullet;
    [SerializeField] float rangeRadius;
    bool detectingPlayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Vector3 playerPos;
    [SerializeField] float fireTime;
    bool isFiring;

    // Update is called once per frame
    void Update()
    {
        detectingPlayer = Physics2D.OverlapCircle(transform.position, rangeRadius, playerLayer);
        if (detectingPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerPos = player.transform.position;
            if (!isFiring)
            {
                isFiring = true;
                StartCoroutine(FireCoroutine());
            }
        }
        else
        {
            playerPos = Vector3.zero;
        }
    }
    void Fire()
    {
        Vector3 aimDirection = (playerPos - transform.position).normalized;
        bulletScript bScript = Instantiate(bullet, transform.position, transform.rotation).GetComponent<bulletScript>();
        bScript.SetDirection(aimDirection);
    }
    IEnumerator FireCoroutine()
    {
        while (detectingPlayer)
        {
            Fire();
            yield return new WaitForSeconds(fireTime);
            yield return null;
        }
        isFiring = false;
    }
}
