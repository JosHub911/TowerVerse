using UnityEngine;

public class EndlessRunner : MonoBehaviour
{
    [SerializeField] float vbegin = 5f;
    [SerializeField] float g = -20f;

    Animator animator;
    Vector3 velocity = Vector3.zero;
    Vector3 acceleration = Vector3.zero;

    enum State
    {
        Running,
        Jumping,
    };
    State myState = State.Running;
    float t = 0;
    float tmax = 0.867f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myState == State.Running) { }
        ;


        if (Input.GetKeyDown(KeyCode.Space))
        {

            animator.Play("Jump");
            t = 0;
            velocity = new Vector3(0, vbegin, 0);
            g = -2 * vbegin / tmax;
            acceleration = new Vector3(0, -20, 0);
            myState = State.Jumping;

        }

        if (myState == State.Jumping)
        {
            t += Time.deltaTime;
            if (t < tmax)
            {
                myState = State.Running;
                velocity = Vector3.zero;
                acceleration = Vector3.zero;

            }

        }
        ;

        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }
}
