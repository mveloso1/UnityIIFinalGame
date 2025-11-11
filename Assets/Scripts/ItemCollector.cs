using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ItemCollector : MonoBehaviour
{
    public int itemsCollected, itemsInLevel;
    public TMP_Text itemHUD;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemHUD.text = $"Skulls {itemsCollected}/{itemsInLevel}";
    }

    public void ItemCollect()
    {
        itemsCollected++;
        itemHUD.text = $"Skulls {itemsCollected}/{itemsInLevel}";

        if (itemsCollected >= itemsInLevel)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        itemHUD.text = $"You collected all the skulls!";
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
