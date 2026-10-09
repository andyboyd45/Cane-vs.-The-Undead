using UnityEngine;

public class SoliderAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] AttackClips;
    [SerializeField] private AudioClip[] DeathClips;
    private AudioSource audioSource;

    //Entity audio is played on SFX audio source
    //Grabs the SFX audio source from scene
    void Awake()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
    }

    //Plays attack audio clips
    //Randomly selects an attack clip to play
    void playAttack()
    {
        int rand = Random.Range(0, AttackClips.Length);
        audioSource.PlayOneShot(AttackClips[rand]);
    }

    //Plays death audio clips
    //Randomly selects a death clip to play(if there is multiple)
    void playDeath()
    {
        int rand = Random.Range(0, DeathClips.Length);
        audioSource.PlayOneShot(DeathClips[rand]);
    }
}
