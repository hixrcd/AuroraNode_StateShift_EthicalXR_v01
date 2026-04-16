using UnityEngine;

public class StateShiftController : MonoBehaviour
{
    // === REFERENCES ===
    public Renderer orbRenderer;
    public Light orbLight;

    // === STATES ===
    public enum State
    {
        Grounded,
        Focused,
        Overloaded
    }

    public State currentState;

    // === PULSE SETTINGS ===
    private float pulseSpeed;
    private float pulseAmount;
    private Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
        SetState(State.Grounded);
    }

    void Update()
    {
        // INPUT
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetState(State.Grounded);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetState(State.Focused);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetState(State.Overloaded);

        // PULSE ANIMATION
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = baseScale + Vector3.one * pulse;
    }

    void SetState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Grounded:
                ApplyState(Color.blue, 1f, 1f, 0.05f);
                break;

            case State.Focused:
                ApplyState(Color.yellow, 2f, 2f, 0.1f);
                break;

            case State.Overloaded:
                ApplyState(Color.red, 4f, 5f, 0.2f);
                break;
        }
    }

    void ApplyState(Color color, float lightIntensity, float speed, float amount)
    {
        if (orbRenderer != null)
            orbRenderer.material.color = color;

        if (orbLight != null)
            orbLight.intensity = lightIntensity;

        pulseSpeed = speed;
        pulseAmount = amount;
    }
}