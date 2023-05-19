using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GestionSpeaker : MonoBehaviour
{

    [SerializeField] private GameObject _image = default;
    // Variables =============================================================================================================================================================
    private AudioSource _audioSource;
    //private UI _ui;
    // Start =================================================================================================================================================================
    void Start()
    {
        _audioSource = FindObjectOfType<Speaker>().GetComponent<AudioSource>();
        //_ui = FindObjectOfType<UI>();
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            _audioSource.Stop();
            _image.SetActive(false);
        }
    }
    // Update =================================================================================================================================================================
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            MusiqueOff();
        }
    }
    // Méthodes private =================================================================================================================================================================
    public void MusiqueOff()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            _audioSource.Play();
            PlayerPrefs.SetInt("Muted", 1);
            PlayerPrefs.Save();
            _image.SetActive(false);
        }
        else
        {
            _audioSource.Pause();
            PlayerPrefs.SetInt("Muted", 0);
            PlayerPrefs.Save();
            _image.SetActive(true);
        }
    }

}
