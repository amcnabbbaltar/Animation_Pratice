using UnityEngine;

public class SpeedBlend1D : MonoBehaviour
{
    Animator anim;

    [Header("Tuning")]
    public float dampTime = 0.12f;
    public bool useRawInput = true; // raw = snappy, smooth = softer

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = useRawInput ? Input.GetAxisRaw("Horizontal") : Input.GetAxis("Horizontal");
        float y = useRawInput ? Input.GetAxisRaw("Vertical")   : Input.GetAxis("Vertical");

        // Movement amount (0..1 after clamp)
        float inputMagnitude = new Vector2(x, y).magnitude;
        inputMagnitude = Mathf.Clamp01(inputMagnitude);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Map to blend space:
        // idle = 0
        // walk = ~0.5
        // run  = ~1
        float targetSpeed = 0f;

        if (inputMagnitude > 0.01f)
            targetSpeed = isRunning ? 1f : 0.5f;

        // Smooth the parameter to avoid snapping
        anim.SetFloat("Speed", targetSpeed, dampTime, Time.deltaTime);
    }
}
