using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // serializedField =========================================================================================================================================
    [SerializeField] private Image _fillBar = default;
    [SerializeField] private TextMeshProUGUI _textPointage = default;
    [SerializeField] private TextMeshProUGUI _textTemps = default;
    [SerializeField] private Image _fillFireArrow = default;
    // Variables================================================================================================================================================
    private Player _player;
    private float _vieMax;
    private int _pointage = 0;
    //private bool _canFireArrow = true;
    private float _time = 0f;
    // Start ====================================================================================================================================================
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _fillBar.fillAmount = 1f;
        _fillFireArrow.fillAmount = 0f;
        _vieMax = (float)_player.GetVie();
        _textPointage.text = "Pointage : " +  _pointage;
    }
    // Update ====================================================================================================================================================
    void Update()
    {
        LoseHealthBar();
        MiseAJourText();
        ChargementFireArrow();
        _time = Time.time - PlayerPrefs.GetFloat("timePerdu");
    }
    // Méthodes private =================================================================================================================================================
    //Gère la bar de chargement de la fire arrow
    private void ChargementFireArrow()
    {
        if (Time.time <= _player.GetCanFireFireArrow())
        {
            _fillFireArrow.fillAmount = (_player.GetCanFireFireArrow() - Time.time) / _player.GetTimeFireArrow();
        }
    }
    //Met a jour les differents texts : pointage et temps
    private void MiseAJourText()
    {
        _textPointage.text = "Pointage : " + _pointage;
        _textTemps.text = "Temps : " + Math.Round(_time);
    }
    //// Méthodes public =================================================================================================================================================
    //Retourne le pointage du joueur
    public int GetPointage()
    {
        return _pointage;
    }
    //Rajoute un nombre au pointage
    public void AddPointage(int point)
    {
        _pointage += point;
    }
    //Gère la bar de vie du player pour la réduire lors de dégât
    public void LoseHealthBar()
    {
        float conversion = (float)_player.GetVie();
        _fillBar.fillAmount = conversion / _vieMax;
    }
}
