using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioBonk : MonoBehaviour
{
    public AudioSource[] sounds;
    public AudioSource noise1;
    public AudioSource noise2;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 8)
        {
            if (collision.gameObject.tag == "cat")
            {
                noise2.Play();
            }
            else
            {
                noise1.Play();
            }
        }
              
    }
}
//References: https://answers.unity.com/questions/52017/2-audio-sources-on-a-game-object-how-use-script-to.html