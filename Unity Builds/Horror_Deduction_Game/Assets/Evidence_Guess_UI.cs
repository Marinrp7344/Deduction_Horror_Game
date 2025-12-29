using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Evidence_Guess_UI : MonoBehaviour
{
    public EvidenceData guessingValue;
    public Director director;
    public Toggle checkbox;
    public Text text;

    public void UpdateConnectedValue(bool guessingData)
    {
        //guessingValue{ get => guessingData; set => guessingData; }
    }

    public void UpdateGuessingData()
    {
        guessingValue.evidenceRelevant = checkbox.isOn;
        director.FilterPossibleGuesses();
    }
}
