using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
   TextMeshPro label;
   Vector2Int cords = new Vector2Int();
   GridManager gridManager;

   private void Awake()
   {
        gridManager = FindFirstObjectByType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();

        DisplayCords();
    }

    private void Update()
    {
        DisplayCords();
        transform.name = cords.ToString();
    }

    private void DisplayCords()
    {
        if (!gridManager) { return; }
        cords.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

        label.text = $"{cords.x}, {cords.y}";
    } 
}
