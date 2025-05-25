using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelData levelData;

    public GameObject ballPrefab;
    public GameObject beamPrefab;
    public GameObject goalPrefab;
    public GameObject groundPrefab;
    public GameObject starPrefab;
    public GameObject slotPrefab;

    private GameObject ball;
    private GameObject goal;
    private readonly List<GameObject> levelObjects = new List<GameObject>();

    public Button startButton;
    public Button resetButton;
    public Transform slotPanel;

    public bool isSimulationRunning = false;
    public int collectedStars = 0;

    void Start()
    {
        Instance = this;
        startButton.onClick.AddListener(StartSimulation);
        resetButton.onClick.AddListener(ResetLevel);

        LoadLevel();

        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.bodyType = RigidbodyType2D.Kinematic;

    }

    void LoadLevel()
    {
        ClearLevel();

        ball = Instantiate(ballPrefab,
            (Vector3)levelData.ballStartPosition,
            new Quaternion());

        goal = Instantiate(goalPrefab,
            new Vector3(levelData.goalPosition.x, levelData.goalPosition.y, 0),
            new Quaternion());

        foreach (var obj in levelData.initialObjects)
        {
            switch (obj.type)
            {
                case SpawnedObjectType.Ball:
                    levelObjects.Add(Instantiate(ballPrefab));
                    break;
                case SpawnedObjectType.Beam:
                    levelObjects.Add(Instantiate(beamPrefab));
                    break;
                case SpawnedObjectType.Goal:
                    levelObjects.Add(Instantiate(goalPrefab));
                    break;
                case SpawnedObjectType.Ground:
                    levelObjects.Add(Instantiate(groundPrefab));
                    break;
                case SpawnedObjectType.Star:
                    levelObjects.Add(Instantiate(starPrefab));
                    break;
                default:
                    Debug.LogAssertion("unhadled type");
                    break;
            }

            levelObjects.Last().transform.position = (Vector3)obj.position;
            levelObjects.Last().transform.rotation = Quaternion.Euler(0, 0, obj.rotation);
        }

        foreach (var obj in levelData.availableObjects)
        {
            GameObject slot = Instantiate(slotPrefab, slotPanel);
            ObjectSlotUI slotUI = slot.GetComponent<ObjectSlotUI>();
            GameObject objectPrebaf = null;

            switch (obj.type)
            {
                case SpawnedObjectType.Beam:
                    objectPrebaf = beamPrefab;
                    break;
                default:
                    Debug.Log("Incorrect type");
                    break;
            }

            slotUI.Init(objectPrebaf, obj.quantity);
        }
    }

    void ClearLevel()
    {
        Destroy(ball);
        Destroy(goal);

        foreach (GameObject go in levelObjects)
        {
            Destroy(go);
        }
        levelObjects.Clear();
    }

    private void ResetLevel()
    {
        if (!isSimulationRunning) {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawned"))
            {
                obj.GetComponent<DraggableObject>().slot.IncreaseCount();
                Destroy(obj);
            }
        }
    }

    void StartSimulation()
    {
        if (!isSimulationRunning)
        {
            isSimulationRunning = true;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spawned"))
            {
                var draggable = obj.GetComponent<DraggableObject>();
                if (draggable != null)
                {
                    draggable.SetSimulationState(true);
                }
            }

            ball.GetComponent<BallController>().StartMoving();
        }
    }

    internal void CollectStar(GameObject gameObject)
    {
        collectedStars++;
        Destroy(gameObject);
    }
}