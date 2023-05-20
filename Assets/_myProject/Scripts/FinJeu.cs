using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJeu : MonoBehaviour
{
    // Serializefield =======================================================================================================================================================
    [SerializeField] private TextMeshProUGUI _bestPointage = default;
    [SerializeField] private TextMeshProUGUI _bestTime = default;
    [SerializeField] private TextMeshProUGUI _pointage = default;
    [SerializeField] private TextMeshProUGUI _time = default;
    [SerializeField] private TextMeshProUGUI _bestName = default;
    [SerializeField] private TextMeshProUGUI _bestNameSaisi = default;
    [SerializeField] private GameObject _best = default;
    // variables ============================================================================================================================================================
    private float _temps = 0f;
    // Start ================================================================================================================================================================
    void Start()
    {
        int pointage = PlayerPrefs.GetInt("pointage");
        _pointage.text = "pointage : " + pointage;
        _time.text = "Temps : " + Math.Round(PlayerPrefs.GetFloat("timeJeu"));

        if(PlayerPrefs.GetInt("bestPointage") < pointage)
        {
            _best.SetActive(true);
            //PlayerPrefs.SetInt("bestPointage", pointage);
            //PlayerPrefs.SetString()
        }
        else
        {
            _bestName.text = "Nom : " + PlayerPrefs.GetString("bestName");
            _bestPointage.text = "Pointage : " + PlayerPrefs.GetInt("bestPointage");
            _bestTime.text = "Temps : " + Math.Round(PlayerPrefs.GetFloat("bestTime"));
        }
    }
    // Update ================================================================================================================================================================
    void Update()
    {
        _temps = Time.time - (PlayerPrefs.GetFloat("timePerdu") + PlayerPrefs.GetFloat("timeJeu"));
    }
    // Méthodes public ================================================================================================================================================================
    // Permet de faire fermer le jeu
    public void Quit()
    {
        Application.Quit();
    }
    // Permet de recommencer une nouvelle partie
    public void Recommencer()
    {
        PlayerPrefs.SetFloat("timePerdu",(PlayerPrefs.GetFloat("timePerdu") + PlayerPrefs.GetFloat("timeJeu") + _temps));
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    // Permet de cliquer sur le bouton pour sauvegarder le nom
    public void Save()
    {
        PlayerPrefs.SetString("bestName", _bestNameSaisi.text);
        PlayerPrefs.SetInt("bestPointage", PlayerPrefs.GetInt("pointage"));
        PlayerPrefs.SetFloat("bestTime", PlayerPrefs.GetFloat("timeJeu"));
        _bestTime.text = "Temps : " + Math.Round(PlayerPrefs.GetFloat("bestTime"));
        _bestPointage.text = "Pointage : " + PlayerPrefs.GetInt("bestPointage");
        _bestName.text = "Nom : " + _bestNameSaisi.text;
        _best.SetActive(false);
    }
}
