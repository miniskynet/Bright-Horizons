//this class will not be used in the current iteration of the game due to 
//increased complexity of the gameplay loop

using UnityEngine;

public class Sticky_Platform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if player object collides with a platform type object
        //change the parent of the player object
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //nullify the player objects parent 
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }


}
