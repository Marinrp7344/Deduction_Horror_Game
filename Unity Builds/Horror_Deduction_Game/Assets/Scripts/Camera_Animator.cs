using UnityEngine;
using System.Collections.Generic;
public class Camera_Animator : MonoBehaviour
{
    [SerializeField] private List<PlayerPointOfViews> positions;
    [SerializeField] private PlayerPointOfViews currentPosition;
    [SerializeField] private PlayerPointOfViews targetPosition;
    [SerializeField] private int currentView;
    [SerializeField] private bool animationDone;
    [SerializeField] private bool switchingViews;
    [SerializeField] private ViewsButtons viewsButtons;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float positionChangeSpeed;
    [SerializeField] private float angleChangeSpeed;
    [SerializeField] private bool positionReached = false;
    [SerializeField] private bool angleReached = false;

    private void Start()
    {
        //cameraTransform.position = positions[0].position;
        //cameraTransform.eulerAngles = positions[0].angle;
        //currentView = 0;
        //viewsButtons.ChangeButtons(currentView);
    }

    private void Awake()
    {
        cameraTransform.position = positions[0].position;
        cameraTransform.eulerAngles = positions[0].angle;
        currentView = 0;
        viewsButtons.ChangeButtons(currentView);
    }

    private void Update()
    {
        if(switchingViews && !animationDone)
        {
            bool reachedPosition = MoveTowardsNewPosition();
            if(reachedPosition)
            {
                switchingViews = false;
                animationDone = true;
                viewsButtons.ChangeButtons(currentView);
                animationDone = false;
                positionReached = false;
                angleReached = false;
            }
        }
    }

    private bool MoveTowardsNewPosition()
    {
        
        // Smooth position
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition.position, positionChangeSpeed * Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(targetPosition.angle);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, angleChangeSpeed * Time.deltaTime
        );

        float positionDistance = Mathf.Abs(Vector3.Distance(targetPosition.position, cameraTransform.position));
        float angleDistance = Mathf.Abs(Vector3.Distance(targetPosition.angle, cameraTransform.eulerAngles));

        Debug.Log("Position: " + positionDistance);
        Debug.Log("Angle: " + angleDistance);
        if (positionDistance < 0.1f && positionReached != true)
        {
            positionReached = true;
        }

        if((angleDistance < 0.2f || (angleDistance > 359.8f && angleDistance <= 360.2f)) && angleReached != true)
        {
            angleReached = true;
        }

        if(positionReached && angleReached)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void SwitchView(int view)
    {
        viewsButtons.DisableButtons();
        switchingViews = true;
        currentPosition = positions[currentView];
        currentView = view;
        targetPosition = positions[currentView];
    }

    public int GetCurrentView()
    {
        return currentView;
    }

    public bool GetAnimationDone()
    {
        return animationDone;
    }

    public bool GetSwitchingViews()
    {
        return switchingViews;
    }

}

[System.Serializable]
public class PlayerPointOfViews
{
    public Vector3 position;
    public Vector3 angle;
    public int viewIndex;
    public List<int> connectedViews;
}
