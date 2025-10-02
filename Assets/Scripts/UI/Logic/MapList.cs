using System.Collections.Generic;
using System.Linq;
using MindlessSDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapList : MonoBehaviour
{
    [Header("List")]
    public Transform ListContent;
    public List<Object> ListItems;
    [Header("References")]
    public Button ButtonPrefab;
    void OnEnable()
    {
        ListItems.ToList().ForEach(Destroy);

        RequestMaps();
    }

    private void RequestMaps()
    {
        var levels = AssetWarehouse.Instance.LevelCrates.FindAll(a => !a.Redacted);
        foreach (var level in levels)
        {
            var button = Instantiate(ButtonPrefab, ListContent);
            button.GetComponentInChildren<TMP_Text>().text = level.Title;
            button.onClick.AddListener(()=>LoadLevel(level));
        }
    }

    private void LoadLevel(LevelCrate level)
    {
        SceneManager.Instance.LoadLevel(level);
    }
}
