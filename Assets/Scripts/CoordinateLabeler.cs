using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.grey;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        //Will only work in edit mode
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ToggleLabels();
        ChangeColour();
    }
    void ChangeColour()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.getNode(coordinates);
         
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
        
    }
    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt( transform.parent.position.x / gridManager.UnityGridSize );
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize );

        label.text = coordinates.x + "," + coordinates.y;
    }
    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
