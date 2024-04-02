using UnityEngine;
using UnityEngine.UI;

public class Item_Collector : MonoBehaviour
{
    private int collectibles = 0;
    [SerializeField] private Text collectiblesText;
    [SerializeField] private AudioSource collectionSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player collides with object of type collectable,
        //destroy the game object and increment the high score
        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            collectibles++;
            collectiblesText.text = "Collectibles : " + collectibles;
        }
    }
}
