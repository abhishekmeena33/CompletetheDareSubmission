using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 100;
    float fps=370f;
    private float currentLives;
    private int poisonTouches = 0; // Counter for poison touches

    private void Start()
    {
        currentLives = (float)maxLives;
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentLives <= 0)
        {
            RestartGame();
            return;
        }
        currentLives -= damageAmount/fps; 
    }

    void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Posion"))
        {
            PoisonTouched();
        }
    }

    private void PoisonTouched()
    {
        poisonTouches++;
        Debug.Log("Poion Touched");
    }
    void Update(){
        TakeDamage(poisonTouches);
    }
}
