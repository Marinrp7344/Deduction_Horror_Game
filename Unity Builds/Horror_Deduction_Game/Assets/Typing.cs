using UnityEngine;

public class Typing : MonoBehaviour
{
    public AudioSource source;
    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
