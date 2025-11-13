using UnityEngine;

public class PickupItem : MonoBehaviour
{
    ItemCollector collector;

    public AudioClip collectSound;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       collector = GameObject.Find("CoinHUD").GetComponent<ItemCollector>(); 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            collector.ItemCollect();
            Destroy(gameObject);
        }
    }
}
