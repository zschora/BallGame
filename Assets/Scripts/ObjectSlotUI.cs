using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSlotUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    public int availableCount;
    public GameObject objectPrebaf;
    public Button slotButton;

    public void Init(GameObject objectPrebaf, int avalibleCount)
    {
        slotButton = GetComponent<Button>();
        this.availableCount = avalibleCount;
        this.objectPrebaf = objectPrebaf;
        slotButton.onClick.AddListener(this.TrySpawn);
        UpdateText();
    }

    public void TrySpawn()
    {
        if (!LevelManager.Instance.isSimulationRunning && CanSpawn())
        {
            Spawn();
            availableCount--;
            UpdateText();
        }
    }

    public void IncreaseCount()
    {
        availableCount++;
        UpdateText();
    }

    public void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        countText.text = availableCount.ToString();
    }

    public bool CanSpawn()
    {
        return availableCount > 0;
    }

    private void Spawn()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        pos.z = 0;
        GameObject newObject = Instantiate(objectPrebaf, pos, Quaternion.identity);

        newObject.AddComponent<DraggableObject>();
        newObject.GetComponent<DraggableObject>().slot = this;
        newObject.tag = "Spawned";
        Debug.Log("Object spawned");
    }
}
