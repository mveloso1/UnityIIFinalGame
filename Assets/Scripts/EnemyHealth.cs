using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public Image healthbar;
    public GameObject healthBarObj;
    public float enemyHealth = 100;
    public float currentHealth;
    public GameObject portal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = enemyHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth >= 0)
        {
            currentHealth -= damage;
            healthbar.fillAmount = currentHealth / enemyHealth;
        }

        if (currentHealth <= 0)
        {
            //dead
            Dead();
        }
    }

    void Dead()
    {
        Destroy(healthBarObj);
        Destroy(gameObject);
        portal.SetActive(true);
        //SceneManager.LoadScene(2);

    }
}
