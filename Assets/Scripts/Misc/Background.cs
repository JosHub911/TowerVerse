using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Renderer bgBackground;
    [SerializeField] float speed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bgBackground.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);

    }
}
