using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Import this namespace to use the EventSystem

public class ButtonKeyTrigger : MonoBehaviour
{
    [SerializeField]
    private KeyCode[] keysToActivate;

    [SerializeField]
    private SoundClipPool soundClipPool;

    private Button button;
    private AudioSource audioSource;
    private bool soundPlayedThisFrame = false;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Add a listener for the button click that also clears the current selection
        button.onClick.AddListener(() => {
            if (!soundPlayedThisFrame) {
                PlayRandomSound();
            }
            DeselectButton(); // Deselect the button after playing the sound
        });
    }

    void Update()
    {
        soundPlayedThisFrame = false;

        foreach (var key in keysToActivate)
        {
            if (Input.GetKeyDown(key) && !soundPlayedThisFrame)
            {
                PlayRandomSound();
                ExecuteButtonAction();
                DeselectButton(); // Deselect the button after a key press
                soundPlayedThisFrame = true;
                break;
            }
        }
    }

    private void PlayRandomSound()
    {
        if (soundClipPool.soundClips.Length > 0 && !soundPlayedThisFrame)
        {
            int index = Random.Range(0, soundClipPool.soundClips.Length);
            AudioClip clip = soundClipPool.soundClips[index];
            audioSource.PlayOneShot(clip);
            soundPlayedThisFrame = true;
        }
    }

    private void ExecuteButtonAction()
    {
        if (button != null && button.onClick != null)
        {
            button.onClick.Invoke();
        }
    }

    private void DeselectButton()
    {
        // Use the EventSystem to deselect the current button
        EventSystem.current.SetSelectedGameObject(null);
    }
}
