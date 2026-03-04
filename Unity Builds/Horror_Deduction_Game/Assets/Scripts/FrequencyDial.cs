using UnityEngine;
using UnityEngine.InputSystem;
public class FrequencyDial : MonoBehaviour
{
    public Transform dialPosition;
    public Vector2 mousePosition;
    public bool changingValue;
    public bool hovering;

    public float frequency;
    public Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changingValue)
        {
            DecidePlayerFrequency();
        }
    }

    public void DecidePlayerFrequency()
    {
        mousePosition = Mouse.current.position.ReadValue();
        Vector2 dialScreenPosition = mainCamera.WorldToScreenPoint(dialPosition.position);
        float x = dialScreenPosition.x - mousePosition.x;
        float y = dialScreenPosition.y - mousePosition.y;

        float angleRad = Mathf.Atan2(x, y);
        float angleDeg = (Mathf.Rad2Deg * angleRad) + 180f;

        dialPosition.localRotation = Quaternion.Euler(0f,0f, 360f - angleDeg);
        frequency = 360f - angleDeg;
        Debug.Log("Angel in Degrees: " +  angleDeg);

    }

    public void ChangingValue()
    {
        changingValue = true;
    }

    public void StopChangingValue()
    {
        changingValue = false;
    }

    public void HoveringDial()
    {
        hovering = true;
    }

    public void NotHoveringDial()
    {
        hovering = false;
    }
}
