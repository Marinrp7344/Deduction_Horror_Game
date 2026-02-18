using UnityEngine;

public class RadarUI_MonsterIndicator : MonoBehaviour
{
    public EnemyRadarCounterpart monsterCounterPart;
    public RectTransform rectTransform;
    public bool hasParent;
    public Transform parent;
    // Update is called once per frame
    void Update()
    {
        if (!hasParent)
        {
            //transform.SetParent(parent);
            hasParent = true;
        }

        rectTransform.localPosition = monsterCounterPart.currentPosition * 2f;
    }
}
