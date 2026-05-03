using UnityEngine;

public class SkyChanger : MonoBehaviour
{
    [Header("Cambiar hijos Sky")]
    public GameObject[] skyObjects;

    [Header("Cambiar sprite de un solo Sky")]
    public SpriteRenderer skyRenderer;
    public Sprite[] skySprites;

    [Header("Tiempo")]
    public float changeInterval = 10f;

    private int currentIndex;

    private void Start()
    {
        FindSkyReferencesIfNeeded();
        ShowSky(0);

        if (changeInterval > 0f)
        {
            InvokeRepeating(nameof(ShowNextSky), changeInterval, changeInterval);
        }
    }

    private void ShowNextSky()
    {
        int count = GetSkyCount();
        if (count <= 1)
        {
            return;
        }

        ShowSky((currentIndex + 1) % count);
    }

    private void ShowSky(int index)
    {
        currentIndex = index;

        if (skyObjects != null && skyObjects.Length > 0)
        {
            for (int i = 0; i < skyObjects.Length; i++)
            {
                if (skyObjects[i] != null)
                {
                    skyObjects[i].SetActive(i == currentIndex);
                }
            }

            return;
        }

        if (skyRenderer != null && skySprites != null && skySprites.Length > 0)
        {
            skyRenderer.sprite = skySprites[currentIndex];
        }
    }

    private int GetSkyCount()
    {
        if (skyObjects != null && skyObjects.Length > 0)
        {
            return skyObjects.Length;
        }

        if (skySprites != null && skySprites.Length > 0)
        {
            return skySprites.Length;
        }

        return 0;
    }

    private void FindSkyReferencesIfNeeded()
    {
        if ((skyObjects == null || skyObjects.Length == 0) && transform.childCount > 0)
        {
            skyObjects = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                skyObjects[i] = transform.GetChild(i).gameObject;
            }
        }

        if (skyRenderer == null)
        {
            skyRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
}
