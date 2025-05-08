using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicSwitcher : MonoBehaviour
{
    public AudioClip[] playlist; // Drag your 8 clips here
    private AudioSource audioSource;
    private int currentIndex = 0;

    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (playlist.Length > 0 && audioSource.clip == null)
        {
            audioSource.clip = playlist[0];
            audioSource.Play();
        }
    }

    void Update()
    {
        // For Windows: spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchTrack();
        }

        // For Mobile: double tap
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            float timeSinceLastTap = Time.time - lastTapTime;

            if (timeSinceLastTap <= doubleTapDelay)
            {
                SwitchTrack(); // Double tap detected
                lastTapTime = 0f;
            }
            else
            {
                lastTapTime = Time.time;
            }
        }
    }

    void SwitchTrack()
    {
        if (playlist.Length == 0) return;

        currentIndex = (currentIndex + 1) % playlist.Length;
        audioSource.clip = playlist[currentIndex];
        audioSource.Play();

        Debug.Log("Switched to track: " + audioSource.clip.name);
    }
}