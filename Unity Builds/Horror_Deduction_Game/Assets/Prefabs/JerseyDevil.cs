using UnityEngine;

public class JerseyDevil : Enemy
{
    public Material jerseyDevilMaterial;
    public GameObject jerseyDevil;
    public Transform jerseyDevilTransform;
    public float spawnRangeMin;
    public float spawnRangeMax;
    public float currentOpacity;
    public float maxOpacity;
    public float opacityIncreaseRate;
    public bool jerseyDevilActive;


    private void Update()
    {
        if(jerseyDevilActive)
        {
            currentOpacity += Time.deltaTime * opacityIncreaseRate;
            Color jdColor = jerseyDevilMaterial.color;
            jdColor.a = currentOpacity;
            jerseyDevilMaterial.color = jdColor;

            if(currentOpacity > maxOpacity)
            {
                EnemyAttack();
            }
        }
    }
    public override void ChangeState()
    {
        if (isActive && view != player.GetCurrentView())
        {
            if (!isVisible)
            {
                CheckVisibility();
            }
        }
    }

    public override void MakeEnemyVisible()
    {
        base.MakeEnemyVisible();
        ChooseLocation();
        jerseyDevil.SetActive(true);
        currentOpacity = 0f;
        jerseyDevilActive = true;
    }

    public void ChooseLocation()
    {
        float randomLocation = Random.Range(spawnRangeMin, spawnRangeMax);
        jerseyDevilTransform.position = new Vector3(randomLocation, jerseyDevilTransform.position.y, jerseyDevilTransform.position.z);
    }

    public override void HitCrucifix()
    {
        ResetEnemy();
    }

    public override void ResetEnemy()
    {
        currentOpacity = 0f;
        jerseyDevil.SetActive(false);
        jerseyDevilActive = false;
    }

}
