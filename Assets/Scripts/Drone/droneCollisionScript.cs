using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneCollisionScript : MonoBehaviour
{

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            GameManager.Instance.incrementScore(1);
            Destroy(gameObject);
        }
    }
}
