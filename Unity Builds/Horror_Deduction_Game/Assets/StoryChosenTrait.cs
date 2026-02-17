using TMPro;
using UnityEngine;

public class StoryChosenTrait : MonoBehaviour
{
    public bool occupied;
    public TextMeshProUGUI displayText;

    public void ClearText()
    {
        displayText.text = string.Empty;
        occupied = false;
    }

    public void OccupyTrait(TextMeshProUGUI text)
    {
        displayText.text = text.text;
        occupied = true;
    }
}
