using UnityEngine;
using TMPro;
using System;

public class UpdateCollectibleCount : MonoBehaviour
{
    private TextMeshProUGUI collectibleText;

    [Header("Win UI")]
    [SerializeField] private GameObject winPanel;

    [Header("Win VFX")]
    [SerializeField] private GameObject winVfxPrefab;
    [SerializeField] private Transform vfxSpawnPoint;
    [SerializeField] private float destroyVfxAfter = 5f;

    private bool hasSeenCollectibles = false;
    private bool winTriggered = false;

    void Start()
    {
        collectibleText = GetComponent<TextMeshProUGUI>();

        if (collectibleText == null)
        {
            Debug.LogError("UpdateCollectibleCount script requires a TextMeshProUGUI component on the same GameObject.");
            return;
        }

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        UpdateCollectibleDisplay();
    }

    void Update()
    {
        int totalCollectibles = GetTotalCollectibles();

        if (totalCollectibles > 0)
        {
            hasSeenCollectibles = true;
        }

        collectibleText.text = $"Collectibles remaining: {totalCollectibles}";

        if (hasSeenCollectibles && totalCollectibles <= 0 && !winTriggered)
        {
            TriggerWin();
        }
    }

    private int GetTotalCollectibles()
    {
        int totalCollectibles = 0;

        Type collectibleType = Type.GetType("Collectible");
        if (collectibleType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(
                collectibleType,
                FindObjectsSortMode.None
            ).Length;
        }

        Type collectible2DType = Type.GetType("Collectible2D");
        if (collectible2DType != null)
        {
            totalCollectibles += UnityEngine.Object.FindObjectsByType(
                collectible2DType,
                FindObjectsSortMode.None
            ).Length;
        }

        return totalCollectibles;
    }

    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = GetTotalCollectibles();
        collectibleText.text = $"Collectibles remaining: {totalCollectibles}";
    }

    private void TriggerWin()
    {
        winTriggered = true;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        if (winVfxPrefab != null)
        {
            Vector3 spawnPosition = transform.position;

            if (vfxSpawnPoint != null)
            {
                spawnPosition = vfxSpawnPoint.position;
            }

            GameObject vfx = Instantiate(winVfxPrefab, spawnPosition, Quaternion.identity);
            Destroy(vfx, destroyVfxAfter);
        }

        Debug.Log("WIN! All collectibles collected.");
    }
}