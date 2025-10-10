using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManger : MonoBehaviour
{
    public static WorldSaveGameManger instance;

    [Header("World Index")]
    [SerializeField] private int worldIndex = 1;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        else { instance = this; }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldIndex);
        yield return null;
    }

    public int GetWorldIndex() { return worldIndex; }
}
