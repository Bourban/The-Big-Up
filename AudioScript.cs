using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuidoScript : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    bool isPlaying;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
