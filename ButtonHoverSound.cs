using UnityEngine;
using UnityEngine.EventSystems; // Needed for IPointerEnterHandler

[RequireComponent(typeof(AudioSource))]
public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    private AudioSource audioSource;

    void Awake()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // This method is automatically called by Unity when the mouse pointer enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if there's an audio clip assigned to prevent errors
        if (audioSource.clip != null)
        {
            audioSource.Play(); // Play the assigned audio clip
        }
    }
}
