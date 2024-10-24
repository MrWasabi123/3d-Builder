using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsGlobal : MonoBehaviour
{
    public AudioClip bill;
    public AudioClip click;
    public AudioClip coin;
    public AudioClip discount;
    public AudioClip drop;
    public AudioClip stock;
    public AudioClip spawnblock;
    private AudioSource playMusic;
    public AudioClip leveldone;
    public AudioClip menuConfirm;
    public AudioClip menuCancel;
    public AudioClip defeat;
    public AudioClip transition;
    public GameObject Vslider;
    // Start is called before the first frame update
    void Start()
    {
        playMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(string sN){
        if(sN=="click"){
            playMusic.PlayOneShot(click, 1.0f);
        }
        if(sN=="drop"){
            playMusic.PlayOneShot(drop, 1.0f);
        }
        if(sN=="spawnblock"){
            playMusic.PlayOneShot(spawnblock, 1.0f);
        }

        if(sN=="bill"){
            playMusic.PlayOneShot(bill, 1.0f);
        }
        if(sN=="coin"){
            playMusic.PlayOneShot(coin, 1.0f);
        }
        if(sN=="discount"){
            playMusic.PlayOneShot(discount, 1.0f);
        }
        if(sN=="stock"){
            playMusic.PlayOneShot(stock, 1.0f);
        }
        if(sN=="leveldone"){
            playMusic.PlayOneShot(leveldone, 1.0f);
        }
        if(sN=="menuConfirm"){
            playMusic.PlayOneShot(menuConfirm, 1.0f);
        }
        if(sN=="menuCancel"){
            playMusic.PlayOneShot(menuCancel, 1.0f);
        }
        if(sN=="defeat"){
            playMusic.PlayOneShot(defeat, 1.0f);
        }
        if(sN=="transition"){
            playMusic.PlayOneShot(transition, 1.0f);
        }
    }

    public void setVolume(){
        float volume = Vslider.GetComponent<Slider>().value;
        playMusic.volume = volume;
    }
}
