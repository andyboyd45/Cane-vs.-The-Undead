using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music Audio Source")]
    [SerializeField] private AudioSource MusicSource;
    [Header("Music Clips")]
    [SerializeField] private AudioClip MusicClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Plays background music on loop
    void Start()
    {
        MusicSource.clip = MusicClip;
        MusicSource.loop = true;
        MusicSource.Play();
    }

}
