using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //[SerializeField] private AudioSource backGroundMusic;
    [SerializeField] private GameObject sfxMusic;
    [SerializeField] private GameObject motorBike;
    private GameObject newAudio;
    public List<AudioClip> audioClips;
    public AudioClip backGroundClip;    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {                
         //backGroundMusic.clip = backGroundClip;
         //backGroundMusic.Play();       
    }
    public void PlaySFX(int indexAudio,Transform transform)
    {        
        for(int i =0; i < audioClips.Count; i++)
        {
            if (i == indexAudio)
            {
                GameObject tempAudio = Instantiate(sfxMusic, transform);
                tempAudio.GetComponent<AudioSource>().PlayOneShot(audioClips[indexAudio]);
                Destroy(tempAudio, audioClips[indexAudio].length);
            }
        }
        
    }    
    public void MotorBikeMusicPlay(Transform transform)
    {
        newAudio = Instantiate(motorBike,transform.position,Quaternion.identity,transform);
        newAudio.GetComponent<AudioSource>().Play();        
    }           
    public void StopAudioMotorBike(Transform transform)
    {
        Transform motobike = transform.Find("newAudio");
        AudioSource audio =  motobike.GetComponent<AudioSource>();
        audio.Stop();
    }
}
