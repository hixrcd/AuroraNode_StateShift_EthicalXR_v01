using UnityEngine;
using TMPro;

public class StateShiftController : MonoBehaviour
{
    // === REFERENCES ===
    public Renderer orbRenderer;
    public Light orbLight;
    public TextMeshProUGUI stateLabel;

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
    private float baseLightIntensity;
    public float groundedPulseSpeed = 0.6f;
    public float focusedPulseSpeed = 1.2f;
    public float overloadedPulseSpeed = 2.8f;

    void Start()
    {
        baseScale = transform.localScale;

        if (orbLight != null)
        {
            baseLightIntensity = orbLight.intensity;
        }

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

        // LIGHT PULSE
        if (orbLight != null)
        {
            orbLight.intensity = baseLightIntensity + Mathf.Abs(pulse) * 1f;
        }
        // EMISSION PULSE
        if (orbRenderer != null)
        {
            Color emissionColor = orbRenderer.material.color * (baseLightIntensity + Mathf.Abs(pulse));
            orbRenderer.material.SetColor("_EmissionColor", emissionColor);
        }
    }

    void SetState(State newState)
    {
        currentState = newState;
        if (stateLabel != null)
        {
            stateLabel.text = newState.ToString();
        }
        switch (newState)
        {
            case State.Grounded:
                pulseSpeed = groundedPulseSpeed;
                ApplyState(Color.blue, 1.2f, 0.35f, 0.06f);
                break;

            case State.Focused:
                pulseSpeed = focusedPulseSpeed;
                ApplyState(Color.green, 1.8f, 0.55f, 0.10f);
                break;

            case State.Overloaded:
                pulseSpeed = overloadedPulseSpeed;
                ApplyState(Color.red, 2.5f, 0.9f, 0.16f);
                break;
        }
    }

    void ApplyState(Color color, float lightIntensity, float speed, float amount)
    {
        if (orbRenderer != null)
        {
            orbRenderer.material.color = color;
            orbRenderer.material.EnableKeyword("_EMISSION");
            orbRenderer.material.SetColor("_EmissionColor", color * lightIntensity);
        }
        if (orbLight != null)
        {
            orbLight.intensity = lightIntensity;
            baseLightIntensity = lightIntensity;
        }

        pulseSpeed = speed;
        pulseAmount = amount;
    }
}