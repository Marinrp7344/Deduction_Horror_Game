using UnityEngine;

public class Poltergeist_Object : MonoBehaviour
{
    public bool affectedByPoltergeist;
    public Vector3 startingPoint;
    public Vector3 endingPoint;
    public Vector3 targetPosition;
    public float moveSpeed;

    public bool goingDown;

    private void Update()
    {
        if(affectedByPoltergeist)
        {
            if(goingDown)
            {
                GoingDown();
            }
            else
            {
                GoingUp();
            }
        }
    }

    public void GoingDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, endingPoint, moveSpeed * Time.deltaTime);
        float positionDistance = Mathf.Abs(Vector3.Distance(endingPoint, transform.position));

        if (positionDistance < .1f)
        {
            goingDown = false;
        }
    }

    public void GoingUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, startingPoint, moveSpeed * Time.deltaTime);
        float positionDistance = Mathf.Abs(Vector3.Distance(startingPoint, transform.position));

        if (positionDistance < .1f)
        {
            goingDown = true;
        }
    }

    public void ResetPosition()
    {
        transform.position = targetPosition;
    }
}
