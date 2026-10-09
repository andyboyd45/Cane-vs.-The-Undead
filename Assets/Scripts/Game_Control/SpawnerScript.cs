using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public int spawntime = 0;
    // Update is called once per frame
    //Updating spawntimer every fixed update call
    void FixedUpdate()
    {
        spawntime += 1;
    }
}
