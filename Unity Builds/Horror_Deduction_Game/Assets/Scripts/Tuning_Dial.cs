using UnityEngine;
using UnityEngine.InputSystem;

public class Tuning_Dial : MonoBehaviour
{
    public RectTransform dialPosition;
    public Vector2 mousePosition;
    public bool changingValue;
    public bool hovering;
    public float angleDeg;

    public float frequency;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (changingValue)
        {
            DecidePlayerFrequency();

            if(frequency < 30)
            {
                dialPosition.localRotation = Quaternion.Euler(0f, 0f, 30f);
                frequency = 30f;
            }
            else if(frequency > 330)
            {
                dialPosition.localRotation = Quaternion.Euler(0f, 0f, 330f);
                frequency = 330f;
            }
            else
            {
                dialPosition.localRotation = Quaternion.Euler(0f, 0f, 360f - angleDeg);
            }

            
        }
    }

    public void DecidePlayerFrequency()
    {
        mousePosition = Mouse.current.position.ReadValue();

        float x = dialPosition.position.x - mousePosition.x;
        float y = dialPosition.position.y - mousePosition.y;

        float angleRad = Mathf.Atan2(x, y);
        angleDeg = (Mathf.Rad2Deg * angleRad) + 180f;

        frequency = 360f - angleDeg;
        Debug.Log("Angel in Degrees: " + angleDeg);

    }

    public void ChangingValue()
    {
        changingValue = true;
    }

    public void StopChangingValue()
    {
        changingValue = false;
    }

}
