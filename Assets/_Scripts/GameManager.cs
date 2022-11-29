using System;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Buttons.")] 
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _continueButton;

    [Header("Canvases.")] 
    [SerializeField] private Canvas _gamePlayCanvas;
    [SerializeField] private Canvas _uiCanvas;

    [Header("Tap the screen canvas.")] 
    [SerializeField] private Button _tapCanvasButton;
    [SerializeField] private GameObject _itemHolder;
    
    [Header("Mergable item related")]
    [SerializeField] private Item[] _mergingItems;
    [SerializeField] private TMP_Text _numberOfMergesText;
    [SerializeField] private int _numberOfMerges = 0;
    
    [Header("Extras")]
    [SerializeField] private Canvas _pauseCanvas;

    [Header("Variables")]
    private int index = 0;
    private Transform setParentTransform => _itemHolder.transform;
    private List<Item> _itemsSpawned = new List<Item>();

    private void Awake()
    {
        _pauseButton.onClick.RemoveAllListeners();
        _continueButton.onClick.RemoveAllListeners();
        _tapCanvasButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        _tapCanvasButton.enabled = true;
        _pauseCanvas.enabled = false;
        _pauseButton.onClick.AddListener(delegate { PauseGame(); GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "PAUSED"); _tapCanvasButton.enabled = false;});
        _continueButton.onClick.AddListener(delegate { ResumeGame(); GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "CONTINUED"); _tapCanvasButton.enabled = true;});
        _tapCanvasButton.onClick.AddListener(delegate
        {
            Debug.Log("This shit working?");
            Spawn();
        });
    }

    private void Spawn()
    {
        // Simulate procedural generation to make this indefinite
        // if (_itemsSpawned.GetAllRepeated(x => new { x.name }).ToList().Count >= 10)
        // {

        // Destroy and prepare for spawning
        // foreach (var x in _itemsSpawned.GetAllRepeated(x => new { x.name }).ToList())
        // {
        //     Debug.Log($"DESTROYING AND REMOVING: { x.name }");
        //     Debug.Log($"NAME OF THE OBJECT: {x.name}");
        //     Destroy(x.gameObject);
        //     _itemsSpawned.Remove(x);
        // }
        // if 10 item 1
        
        if (_itemsSpawned.FindAll(x => x.name.Contains("Item_1")).Count >= 10)
        {
            foreach (var a in _itemsSpawned.FindAll(x => x.name.Contains("Item_1")))
            {
                Destroy(a.gameObject);
            }

            _numberOfMerges++;
            
            _numberOfMergesText.text = _numberOfMerges.ToString();
            _itemsSpawned.RemoveAll(x => x.name.Contains("Item_1"));
            _itemsSpawned.Add(Instantiate(_mergingItems[1], setParentTransform));
        }

        // if 10 item 2
        if (_itemsSpawned.FindAll(x => x.name.Contains("Item_2")).Count >= 10)
        {
            foreach (var a in _itemsSpawned.FindAll(x => x.name.Contains("Item_2")))
            {
                Destroy(a.gameObject);
            }
            
            _numberOfMerges++;
            _numberOfMergesText.text = _numberOfMerges.ToString();
            _itemsSpawned.RemoveAll(x => x.name.Contains("Item_2"));
            _itemsSpawned.Add(Instantiate(_mergingItems[2], setParentTransform));
        }

        // if 10 item 3
        if (_itemsSpawned.FindAll(x => x.name.Contains("Item_3")).Count >= 10)
        {
            foreach (var a in _itemsSpawned.FindAll(x => x.name.Contains("Item_3")))
            {
                Destroy(a.gameObject);
            }
            
            _numberOfMerges++;
            _numberOfMergesText.text = _numberOfMerges.ToString();
            _itemsSpawned.RemoveAll(x => x.name.Contains("Item_3"));
            _itemsSpawned.Add(Instantiate(_mergingItems[3], setParentTransform));
        }

        // if 10 item 4
        if (_itemsSpawned.FindAll(x => x.name.Contains("Item_4")).Count >= 10)
        {
            foreach (var a in _itemsSpawned.FindAll(x => x.name.Contains("Item_4")))
            {
                Destroy(a.gameObject);
            }
            
            _numberOfMerges++;
            _numberOfMergesText.text = _numberOfMerges.ToString();
            _itemsSpawned.RemoveAll(x => x.name.Contains("Item_4"));
            _itemsSpawned.Add(Instantiate(_mergingItems[4], setParentTransform));
        }

        // if 10 item 5
        if (_itemsSpawned.FindAll(x => x.name.Contains("Item_5")).Count >= 10)
        {
            GameOver();
            _tapCanvasButton.enabled = false;
        }
        
        _itemsSpawned.Add(Instantiate(_mergingItems[0], setParentTransform));
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Number of merges", _numberOfMerges);
        // }
    }

    private void GameOver()
    {
        if (index >= 5)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "WON");
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    private void PauseGame()
    {
        _pauseCanvas.enabled = true;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "Paused.");
        Time.timeScale = 0;
    }
    
    private void ResumeGame()
    {
        _pauseCanvas.enabled = false;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Undefined, "Resumed.");
        Time.timeScale = 1;
    }
}

//The extention static class to check adn extract duplicates
public static class Extention
{
    public static IEnumerable<T> GetMoreThanOnceRepeated<T>(this IEnumerable<T> extList, Func<T, object> groupProps)
        where T : class
    {
        //Return only the second and next reptition
        return extList
            .GroupBy(groupProps)
            .SelectMany(z => z.Skip(1)); //Skip the first occur and return all the others that repeats
    }

    public static IEnumerable<T> GetAllRepeated<T>(this IEnumerable<T> extList, Func<T, object> groupProps)
        where T : class
    {
        //Get All the lines that has repeating
        return extList
            .GroupBy(groupProps)
            .Where(z => z.Count() > 1) // Filter only the distinct one
            .SelectMany(z => z); // All in where has to be returned
    }
}