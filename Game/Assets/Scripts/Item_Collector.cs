using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int collectibles = 0;
    [SerializeField] private Text collectiblesText;
    [SerializeField] private AudioSource collectionSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            collectibles++;
            collectiblesText.text = "Collectibles : " + collectibles;         
        }
    }
}
