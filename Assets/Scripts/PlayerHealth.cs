using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Image healthbar;
    public float playerHealth = 100;
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = playerHealth;
    }

    public void TakeDamage (float damage)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damage;
            healthbar.fillAmount = currentHealth/playerHealth;
        }

        if (currentHealth <= 0)
        {
            //dead
            PlayerDead();

        }
    }

    void PlayerDead()
    {
        SceneManager.LoadScene(1);
    }
}
