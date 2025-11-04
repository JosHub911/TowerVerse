using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float lifetime = 3f;

    void Start()

    {

        Destroy(gameObject, lifetime);
    }
}
