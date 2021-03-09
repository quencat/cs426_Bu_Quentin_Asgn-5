using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class borderCollision : MonoBehaviour
{
    public Color red;
    Color original;
    public float seconds = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        original = transform.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.GetComponent<Renderer>().material.color = red;
            StartCoroutine(redLight());
        };
        
    }
    IEnumerator redLight()
    {
        yield return new WaitForSeconds(seconds);
        transform.GetComponent<Renderer>().material.color = original;
    }
}
