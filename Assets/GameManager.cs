using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerCanvas;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject playerHealthBarBackground;
    [SerializeField] private GameObject playerHealthBarInner;
    [SerializeField] private int numberOfPatients;
    [SerializeField] private int numberOfActivePatients;
    [SerializeField] private GameObject[] patients;
    [SerializeField] private Life_Manager lifeManager;
    [SerializeField] private GameObject[] prefabsToCreate;
    [SerializeField] private int numberOfPrefabsToCreate;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] navigationPoints;
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Transform[] cprPoints;
    private bool tests_bool = true;
    
    // Start is called before the first frame update
    void Start()
    {
        prefabs = new GameObject[numberOfPrefabsToCreate];
        lifeManager = GameObject.Find("Lifes").GetComponent<Life_Manager>();
        if (instance == null)
        {
            Debug.Log("GameManager instance created");
            instance = this;
        }
        else
        {
            Debug.Log("GameManager instance already exists");
        }

        if (player == null)
        {
            Debug.Log("Player not found");
        }
        else
        {
            Debug.Log("Player found");
        }

        if (playerCamera == null)
        {
            Debug.Log("Player camera not found");
        }
        else
        {
            Debug.Log("Player camera found");
        }

        if (playerCanvas == null)
        {
            Debug.Log("Player canvas not found");
        }
        else
        {
            Debug.Log("Player canvas found");
        }

        if (playerHealthBar == null)
        {
            Debug.Log("Player health bar not found");
        }
        else
        {
            Debug.Log("Player health bar found");
        }

        if (playerHealthBarBackground == null)
        {
            Debug.Log("Player health bar background not found");
        }
        else
        {
            Debug.Log("Player health bar background found");
        }

        if (playerHealthBarInner == null)
        {
            Debug.Log("Player health bar inner not found");
        }
        else
        {
            Debug.Log("Player health bar inner found");
        }    

        if (lifeManager == null)
        {
            Debug.Log("Life manager not found");
        }
        else
        {
            Debug.Log("Life manager found");
        }

        if (prefabsToCreate.Length == 0)
        {
            Debug.Log("No prefabs to create");
        }
        else
        {
            Debug.Log("Prefabs to create found");
        }

        patients = new GameObject[numberOfPatients];

        for (int i = 0; i < numberOfPrefabsToCreate; i++)
        {
            int randomIndex = Random.Range(0, prefabsToCreate.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            // Debug.Log("Creating prefab " + prefabsToCreate[randomIndex].name + " at spawn point " + spawnPoints[randomSpawnPointIndex].name);
            GameObject InstantiatedPrefab = Instantiate(prefabsToCreate[randomIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            prefabs[i] = InstantiatedPrefab;
        }

        for (int i=0 ; i<numberOfPatients ; i++){
            GameObject patient = Instantiate(prefabs[1], cprPoints[i].position, Quaternion.identity);
            patients[i] = patient;
        }
    }

    // Update is called once per frame
    void Update()
    {
        }
    }
    
    private bool CheckPatientsStatus(){
        if(numberOfActivePatients <= numberOfPatients){
            return false;
        }

        return true;
    }

    private GameObject ChooseRandomPatient()
{
    int randomIndex = Random.Range(0, prefabs.Length);
    GameObject randomPatient = prefabs[randomIndex];

    // Check if the patient is already in the patients list
    if (IsPatientInList(randomPatient))
    {
        // Choose another random patient
        return ChooseRandomPatient();
    }

    return randomPatient;
}

private bool IsPatientInList(GameObject patient)
{
    // Check if the patient is already in the patients list by comparing game object IDs
    for (int i = 0; i < patients.Length; i++)
    {
        if (patients[i] != null && patients[i].GetInstanceID() == patient.GetInstanceID())
        {
            return true;
        }
    }

    return false;
}
    
    }



