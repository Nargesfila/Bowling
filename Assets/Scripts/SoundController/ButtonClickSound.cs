using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource; 

    public void PlayClickSound()
    {
        audioSource.Play(); 
    }
}
