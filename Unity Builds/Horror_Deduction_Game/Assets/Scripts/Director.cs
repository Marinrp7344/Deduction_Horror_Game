using UnityEngine;
using System.Collections.Generic;

public class Director : MonoBehaviour
{
    [SerializeField] private List<Monster_Data> monsters;
    [SerializeField] private Evidence_Data guessingData;
    [SerializeField] private Monster_Data currentMonster;
    [SerializeField] private Monster_Data currentGuess;
    [SerializeField] private MonsterGuess currentButtonGuess;
    [SerializeField] private List<MonsterGuess> possibleGuesses;
    [SerializeField] private List<Evidence_Guess_UI> evidenceGuesses;
    [SerializeField] private GameObject possibleGuessesParent;
    [SerializeField] private GameObject guessPrefab;
    [SerializeField] private GameObject evidencePrefab;

    [SerializeField] private GameObject storyView;
    [SerializeField] private GameObject policeReportView;
    [SerializeField] private GameObject videoView;
    [SerializeField] private GameObject audioView;
    [SerializeField] private GameObject imageView;

    [SerializeField] private GameObject guessingMenu;

    public void Start()
    {
        GenerateGuesses();
        GenerateEvidence();
    }
    public void GenerateNewEvidence()
    {

    }

    public void GenerateEvidence()
    {
        foreach(EvidenceData evidence in guessingData.storyList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(storyView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.policeReportList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(policeReportView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.videoList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(videoView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }


        foreach (EvidenceData evidence in guessingData.audioList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(audioView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

        foreach (EvidenceData evidence in guessingData.imageList)
        {
            GameObject checkbox = Instantiate(evidencePrefab, transform.position, Quaternion.identity);
            checkbox.transform.SetParent(imageView.transform);
            Evidence_Guess_UI checkboxEvidence = checkbox.GetComponent<Evidence_Guess_UI>();
            checkboxEvidence.guessingValue = evidence;
            checkboxEvidence.director = this;
            checkboxEvidence.text.text = evidence.evidenceName;
            evidenceGuesses.Add(checkboxEvidence);
        }

    }

    public void FilterPossibleGuesses()
    {
        foreach(MonsterGuess monsterGuess in possibleGuesses)
        {
            bool guessPossible = true;
            for (int i = 0; i < guessingData.storyList.Count; i++)
            {
                if(guessingData.storyList[i].evidenceRelevant == true)
                {
                    if(monsterGuess.monster.storyList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.policeReportList.Count; i++)
            {
                if (guessingData.policeReportList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.policeReportList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.videoList.Count; i++)
            {
                if (guessingData.videoList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.videoList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.audioList.Count; i++)
            {
                if (guessingData.audioList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.audioList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }

            for (int i = 0; i < guessingData.imageList.Count; i++)
            {
                if (guessingData.imageList[i].evidenceRelevant == true)
                {
                    if (monsterGuess.monster.imageList[i].evidenceRelevant == false)
                    {
                        guessPossible = false;
                    }
                }
            }


            if (guessPossible == false)
            {
                monsterGuess.CrossoutButton();
            }
            else
            {
                monsterGuess.DisableCrossoutButton();
            }
        }
    }

    public void GenerateGuesses()
    {
        foreach(Monster_Data monster in monsters)
        {
            GameObject guess = Instantiate(guessPrefab, transform.position, Quaternion.identity);
            guess.transform.SetParent(possibleGuessesParent.transform);
            MonsterGuess guessScript = guess.GetComponent<MonsterGuess>();
            guessScript.director = this;
            guessScript.monster = monster;
            guessScript.monsterName.text = monster.monsterName;
            possibleGuesses.Add(guessScript);
        }
    }

    public void UpdateGuess(Monster_Data monster, MonsterGuess guessUI)
    {
        if(currentButtonGuess != null)
        {
            currentButtonGuess.UnguessButton();
        }
        currentGuess = monster;
        currentButtonGuess = guessUI;
        currentButtonGuess.GuessButton();
    }

    public void SubmitGuess()
    {

    }

    public void ActivateGuessingMenu()
    {
        guessingMenu.SetActive(true);
    }

    public void DisableGuessingMenu()
    {
        guessingMenu.SetActive(false);
    }
}
