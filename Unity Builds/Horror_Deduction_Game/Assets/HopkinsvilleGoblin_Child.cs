using UnityEngine;

public class HopkinsvilleGoblin_Child : Enemy
{
    [SerializeField] private HopkinsvilleGoblin goblinDirector;
    [SerializeField] public int goblinID;
    [SerializeField] public bool goblinActive;
    [SerializeField] private GameObject goblinBody;
    [SerializeField] public int occupiedView;

    public override void HitBullet()
    {
        goblinDirector.GoblinHit(this);
    }

    public void DeactivateGoblin()
    {
        goblinBody.SetActive(false);
        goblinActive = false;
    }

    public void ActivateGoblin()
    {
        goblinBody.SetActive(true);
        goblinActive = true;
    }
}
