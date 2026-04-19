using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Video;


public class Director : MonoBehaviour
{
    [SerializeField] public List<Monster_Data> monsters;
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
    [SerializeField] private List<Evidence_Data> possibleEvidence;
    [SerializeField] private GameObject folder;
    [SerializeField] private Vector3 folderSpawnPosition;
    [SerializeField] public Camera_Animator cameraAnimator;
    [SerializeField] private GameObject currentFolder;
    [SerializeField] private Enemy_Director enemyDirector;

    public VideoPlayer videoPlayer;
    public GameObject audioSource;

    private readonly System.Random rand = new System.Random();

    public EvidenceData chosenTrait = new EvidenceData();
    public List<EvidenceData> unchosenTraits = new List<EvidenceData>();
    public List<Evidence_Data> randomList = new List<Evidence_Data>();
    public int maxDifferenceStrength;
    public int currentDifferenceStrength;

    [SerializeField] private List<Evidence_Data> storiesList;
    [SerializeField] private List<ImageClassifier> imagesList;
    [SerializeField] private List<ImageClassifier> fillerImagesList;
    [SerializeField] private GameObject imagePrefab;
    public void Start()
    {
        GenerateGuesses();
        GenerateEvidence();
        GenerateNewEvidence();
        ClearGuessingMenu();
        DisableGuessingMenu();
    }

    public List<Monster_Data> GetReadyMonsters()
    {
        List<Monster_Data> readyMonsters = new List<Monster_Data>();

        foreach (Monster_Data monster in monsters)
        {
            if (monster.monsterReady)
            {
                readyMonsters.Add(monster);
            }
        }

        return readyMonsters;
    }

    public void GenerateNewEvidence()
    {

        List<Monster_Data> monsterList = GetReadyMonsters();

        if (monsterList.Count == 0)
        {
            Debug.LogError("No monsters are marked as ready!");
            return;
        }

        int randomMonster = UnityEngine.Random.Range(0, monsterList.Count);
        currentMonster = monsterList[randomMonster];


        //List<Evidence_Data> newEvidence = FindValidEvidence(monsterList[randomMonster]);
        List<Evidence_Data> newEvidence = new List<Evidence_Data>();
        newEvidence.Add(GenerateStoryList(monsterList[randomMonster]));

        Evidence_Data imageData = GenerateImageList(monsterList[randomMonster]);
        if(imageData != null)
        {
            newEvidence.Add(imageData);

        }

        Debug.Log(newEvidence.Count);
        CreateUnchosenList();
        List<Evidence_Data> extraStories = GetAlternateStories();

        foreach (Evidence_Data story in extraStories)
        {
            newEvidence.Add(story);
        }

        newEvidence = GenerateRandomLoop(newEvidence);

        GameObject newFolder = Instantiate(folder, folderSpawnPosition, Quaternion.identity);
        Folder folderScript = newFolder.GetComponent<Folder>();
        folderScript.videoPlayer = videoPlayer;
        folderScript.audioSource = audioSource;
        folderScript.evidence = newEvidence;
        folderScript.cameraAnimator = cameraAnimator;
        folderScript.director = this;
        folderScript.InitializeEvidence();
        currentFolder = newFolder;
    }

    public void CreateUnchosenList()
    {
        unchosenTraits = new List<EvidenceData> ();
        foreach(EvidenceData data in currentMonster.storyList)
        {
            if (data.evidenceRelevant == true) 
            {
                if (data.evidenceName != chosenTrait.evidenceName)
                {
                    unchosenTraits.Add(data);
                }
            }
        }
    }

    public List<Evidence_Data> GetAlternateStories()
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(storiesList);
        List<Evidence_Data> validEvidence = new List<Evidence_Data>();

        foreach(Evidence_Data evidence in randomizedPossibleEvidence)
        {
            if(evidence.evidenceType == Evidence_Data.Evidence.Story)
            {
                int i = 0;
                bool isValid = false;
                int incrementalDifferenceStrength = 0;
                foreach (EvidenceData data in evidence.storyList)
                {
                    
                    if (data.evidenceRelevant)
                    {
                        if (data.evidenceRelevant == currentMonster.storyList[i].evidenceRelevant)
                        {
                            if(data.evidenceName == chosenTrait.evidenceName)
                            {
                                //Debug.Log("Data Name: " + data.evidenceName + "\nChosen Trait Name: " + chosenTrait.evidenceName);
                                isValid = true;
                            }
                            else
                            {
                                incrementalDifferenceStrength += 1;
                            }
                        }
                    }
                    i++;

                    
                }

                //Debug.Log(incrementalDifferenceStrength);
                if (isValid == true && (incrementalDifferenceStrength + currentDifferenceStrength) < maxDifferenceStrength)
                {
                    validEvidence.Add(evidence);
                    currentDifferenceStrength += incrementalDifferenceStrength;
                }
            }

            //Debug.Log(validEvidence.Count);
            

            if(validEvidence.Count >= 2) 
            { 
                break; 
            }
        }

        return validEvidence;
    }


    public Evidence_Data GenerateStoryList(Monster_Data monster)
    {
        List<Evidence_Data> randomizedPossibleEvidence = GenerateRandomLoop(storiesList);
        currentDifferenceStrength = 0;
        Evidence_Data validStory = null;

        foreach (Evidence_Data story in randomizedPossibleEvidence)
        {
            List<EvidenceData> chosenEvidenceTraits = new List<EvidenceData>();
            bool isValid = false;
            int i = 0;

            foreach (EvidenceData data in monster.storyList)
            {
                if (story.storyList[i].evidenceRelevant == true)
                {
                    if (data.evidenceRelevant == true)
                    {
                        chosenEvidenceTraits.Add(data);
                        isValid = true;
                        currentDifferenceStrength += 1;
                        Debug.Log("Current Strength: " + currentDifferenceStrength);
                    }
                }
                i++;
            }

            if (isValid)
            {
                int randomTrait = UnityEngine.Random.Range(0, chosenEvidenceTraits.Count);
                chosenTrait = chosenEvidenceTraits[randomTrait];
                validStory = story;
                break;
            }

            
        }
        return validStory;
    }

    public Evidence_Data GenerateImageList(Monster_Data monster)
    {
        List<ImageClassifier> randomizedImageList = GenerateRandomImageLoop(imagesList);
        int monsterImageTraitIndex = monster.imageList.Count;
        List<int> indexPositions = new List<int>();
        for (int k = 0; k < monsterImageTraitIndex; k++)
        {
            indexPositions.Add(k);
        }

        indexPositions = GenerateRandomIntegerLoop(indexPositions);

        int chosenTraitIndex = -1;
        ImageDataPoint chosenImage = null;
        ImageClassifier chosenClassifier = null;

        foreach (int index in indexPositions)
        {
            if (monster.imageList[index].evidenceRelevant == true)
            {
                chosenTraitIndex = index;
                break;

            }
        }

        foreach(ImageClassifier image in randomizedImageList)
        {
            bool isValid = false;
            foreach(ImageDataPoint data in image.imageDataPoints)
            {
                if(data.evidenceName == monster.imageList[chosenTraitIndex].evidenceName)
                {
                    Debug.Log("Names Matched");
                    if (data.active == true)
                    {
                        Debug.Log("Classifier data is active");
                        if (monster.imageList[chosenTraitIndex].evidenceRelevant == true)
                        {
                            isValid = true;
                            chosenImage = data;
                            image.chosen = true;
                            chosenClassifier = image;
                            Debug.Log("Found Image");
                            break;
                        }
                    }
                    
                }
            }

            if(isValid)
            {
                break;
            }
        }

        Evidence_Data imageData = ScriptableObject.CreateInstance<Evidence_Data>();
        imageData.image = new ImageInfo();
        imageData.evidencePrefab = imagePrefab;
        imageData.evidenceType = Evidence_Data.Evidence.Image;
        int i = 0;
        foreach(EvidenceData evidence in imageData.imageList)
        {
            if(evidence.evidenceName == monster.imageList[chosenTraitIndex].evidenceName)
            {
                imageData.imageList[i].evidenceRelevant = true;
                i++;
                break;
            }
        }

        if(chosenClassifier != null && chosenImage != null)
        {
            imageData.image.chosenImage = chosenImage;
            imageData.image.image1 = chosenClassifier;
        }
        else
        {
            return null;
        }
        
        List<ImageClassifier> randomizedFillerImageList = GenerateRandomImageLoop(fillerImagesList);
        int j = 0;
        foreach(ImageClassifier image in randomizedFillerImageList)
        {
            if (j == 0)
            {
                imageData.image.image2 = image;
            }
            else if (j == 1)
            {
                imageData.image.image3 = image;
                break;
            }
            j++;
        }

        return imageData;
    }

   

    public List<Evidence_Data> GenerateRandomLoop(List<Evidence_Data> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            Evidence_Data value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }

    public List<EvidenceData> GenerateRandomTraitLoop(List<EvidenceData> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            EvidenceData value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }

    public List<ImageClassifier> GenerateRandomImageLoop(List<ImageClassifier> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            ImageClassifier value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }

    public List<int> GenerateRandomIntegerLoop(List<int> listToShuffle)
    {

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            int value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
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
        Folder currentFolderScript = currentFolder.GetComponent<Folder>();
        currentFolderScript.DestroyEvidence();
        Destroy(currentFolder);

        if (currentGuess == currentMonster)
        {
            monsters.Remove(currentMonster);
            enemyDirector.CheckIfMonsterActive(currentMonster);
        }
        else
        {
            if(!enemyDirector.MonsterActive(currentMonster))
            {
                enemyDirector.SpawnMonster(currentMonster);
            }
            else
            {
                enemyDirector.PlayerGuessedWrongTwice(currentMonster);
            }
        }

        currentFolder = null;
        ClearGuessingMenu();
        FilterPossibleGuesses();
        GenerateNewEvidence();
        
    }

    public void ActivateGuessingMenu()
    {
        guessingMenu.SetActive(true);
    }

    public void DisableGuessingMenu()
    {
        guessingMenu.SetActive(false);
    }

    public void CloseVideoPlayer()
    {
        cameraAnimator.SwitchView(0);
        videoPlayer.Stop();
    }

    public void ClearGuessingMenu()
    {
        foreach(Evidence_Guess_UI guess in evidenceGuesses)
        {
            guess.ClearToggle();
        }

        foreach(EvidenceData story in guessingData.storyList)
        {
            story.evidenceRelevant = false;
        }

        foreach (EvidenceData image in guessingData.imageList)
        {
            image.evidenceRelevant = false;
        }

        foreach (EvidenceData audio in guessingData.audioList)
        {
            audio.evidenceRelevant = false;
        }


    }
}
