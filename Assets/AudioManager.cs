using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip kill;    // Reference to the kill audio clip
    public AudioClip arrow;   // Reference to the arrow audio clip
    public AudioClip block;   // Reference to the block audio clip
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //Play for killing enemy
    public void PlayKillSound()
    {
        if (kill != null)
        {
            audioSource.PlayOneShot(kill);
        }
    }

    //Play for arrow spawn
    public void PlayArrowSound()
    {
        if (arrow != null)
        {
            audioSource.PlayOneShot(arrow);
        }
    }

    //Play for arrow block
    public void PlayBlockSound()
    {
        if (block != null)
        {
            audioSource.PlayOneShot(block);
        }
    }
}
