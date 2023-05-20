using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // SerializeField ==================================================================================================================================================
    [SerializeField] private GameObject _instruction = default;
    // Start =============================================================================================================================================================
    void Start()
    {
        PlayerPrefs.SetFloat("timePerdu", 0f);
        PlayerPrefs.Save();
    }
    // Update =============================================================================================================================================================
    void Update()
    {
        
    }
    // Méthodes Public =============================================================================================================================================================
    //Permet de lancer le jeu en allant à la première scène
    public void Play()
    {
        PlayerPrefs.SetFloat("timePerdu",Time.time);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
    //Affiche les instructions du jeu
    public void Instruction()
    {
        _instruction.SetActive(true);
    }
    //Ferme les instructions
    public void CloseInstruction()
    {
        _instruction.SetActive(false);
    }
    //Permet de quitter
    public void Quit()
    {
        Application.Quit();
    }
}
