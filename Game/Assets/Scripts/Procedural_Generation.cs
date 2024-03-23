using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private Transform itemList;

    void Start()
    {
       ItemGeneration(); 
    }

    private void ItemGeneration()
    {
        for (int i = 0; i < 10; ++i) {
            Vector3 position = new Vector3(Random.Range(0, 90),2,0);
            Instantiate(item, position, Quaternion.identity, itemList);
    }
    }


}
