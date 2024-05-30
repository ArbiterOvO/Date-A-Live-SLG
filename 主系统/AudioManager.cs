using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource ;
    public Slider mainMusicSlider;
    public Slider effectMusicSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource=gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setVolume()
    {
        audioSource.volume=mainMusicSlider.value;
    }
}
