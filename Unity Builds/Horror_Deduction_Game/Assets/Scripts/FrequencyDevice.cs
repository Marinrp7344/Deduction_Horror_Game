using Radishmouse;
using UnityEngine;

public class FrequencyDevice : MonoBehaviour
{

    public Enemy targetedEnemy;
    public float frequency;

    [Header("Tuning Features")]
    public float targetFrequency;
    public float playerFrequency;
    public UILineRenderer lineRenderer;
    public float correctnessThreshold;
    public float noise;
    public float errorAmount;
    public bool attackable;

    //public RectTransform dialTransform;
    public FrequencyDial dial;


    public void SetTargetFrequency(Enemy TargetEnemy)
    {
        targetedEnemy = TargetEnemy;
        targetFrequency = targetedEnemy.enemyFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        if (dial == null)
        {
            Debug.LogError("Dial is NULL on " + gameObject.name);
            return;
        }

        playerFrequency = dial.frequency;

        if (targetedEnemy != null)
        {
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is NULL on " + gameObject.name);
                return;
            }

            lineRenderer.gameObject.SetActive(true);
            float error = Mathf.Abs(playerFrequency - targetFrequency);
            errorAmount = error;
            DrawLine(error);
        }
        else
        {
            if (lineRenderer != null)
                lineRenderer.gameObject.SetActive(false);
        }
    }

    public void DrawLine(float error)
    {
        if(error <= correctnessThreshold)
        {
            attackable = true;
            lineRenderer.color = Color.green;

            for (int i = 0; i < lineRenderer.points.Length; i++)
            {
                lineRenderer.points[i].y = 0f;
            }
        }
        else
        {
            attackable = false;
            lineRenderer.color= Color.red;
            float noiseAmount = noise * error; 
            for (int i = 0; i < lineRenderer.points.Length; i++)
            {
                float t = (float)i / (lineRenderer.points.Length - 1);
                float y =
                    Mathf.Sin((t + Time.time) * frequency * Mathf.PI * 2f) +
                    Random.Range(-noiseAmount, noiseAmount);

                lineRenderer.points[i] = new Vector3(lineRenderer.points[i].x, y, 0);
            }
        }
        lineRenderer.SetVerticesDirty();
    }

    public void Attack()
    {
        if(targetedEnemy != null && attackable)
        {
            targetedEnemy.ResetEnemy();
            targetedEnemy = null;
        }
    }


}
