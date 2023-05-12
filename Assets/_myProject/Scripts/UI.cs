using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // serializedField =========================================================================================================================================
    [SerializeField] private Image _fillBar = default;

    // Variables================================================================================================================================================
    private Player _player;
    // Start ====================================================================================================================================================
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _fillBar.fillAmount = 1f;
    }

    // Update ====================================================================================================================================================
    void Update()
    {
        //LoseHealthBar();
    }

    // Méthodes =================================================================================================================================================
    public void LoseHealthBar()
    {
        _fillBar.fillAmount = _player.GetVie() / 5;
    }
}
