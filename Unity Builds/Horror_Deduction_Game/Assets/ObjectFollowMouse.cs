using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectFollowMouse : MonoBehaviour
{
    public Transform invObject;
    public float height;
    public float width;
    public float ratio;
    public int roundingFactor;
    public float maxRotationHeight;

    private void Update()
    {
        height = Screen.height;
        width = Screen.width;
        ratio = width / height;
        invObject.localRotation = Quaternion.Euler(MousePositionToRotation());
    }

    public Vector2 MousePositionToRotation()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 screenCoords = mousePosition - new Vector2(width/2, height/2);
        float maxRotationWidth =  maxRotationHeight * ratio;
        Vector2 rotationLimits = new Vector2(maxRotationHeight, maxRotationWidth);
        Vector2 coordsToRotation = new Vector2(height,width) / rotationLimits;
        Vector2 rotation = screenCoords / coordsToRotation;
        rotation = new Vector2(RoundRotation(rotation.x), RoundRotation(rotation.y));
        rotation = new Vector2((int)rotation.y * -1, (int)rotation.x);
        Debug.Log("Rotation: " + rotation);
        return rotation;
    }

    public int RoundRotation(float value)
    {
        return (int)Mathf.Round(value / roundingFactor) * roundingFactor;
    }
}
