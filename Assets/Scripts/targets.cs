using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targets : MonoBehaviour
{
    public GameManager manager;


    public ParticleSystem system;
    protected ParticleSystem explode;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        Destroy(gameObject);
        manager.AddPoint(collision);
        explode = Instantiate(system, transform.position, Quaternion.identity);
        
    }
}
