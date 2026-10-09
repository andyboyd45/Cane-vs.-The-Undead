using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Prefab Settings")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private LayerMask layer;
    [SerializeField] private int cost;
    private GameObject Carrying;
    private Camera cam;
    private MonoBehaviour[] carriedScripts;
    private Collider2D[] carriedColliders;
    private CoinManager coinManager;

    //Calls main camera and coin manager
    void Awake()
    {
        cam = Camera.main;
        GameObject coins = GameObject.Find("Coins");
        coinManager = coins.GetComponent<CoinManager>();
    }

    //Gets the mouse position
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f; 
        return mouseWorldPos;
    }

    //When entity is being carried from UI, it disables collisions and scripts
    //This prevents damage, attacking, and other interactions while being carried
    private void SetCarryingComponents(bool act)
    {
        foreach (MonoBehaviour s in carriedScripts)  if (s != null) s.enabled = act;
        foreach (Collider2D c in carriedColliders)   if (c != null) c.enabled = act;
    }
    
    //Called by EventSystems and is when the mouse is click and dragging the button
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (coinManager == null || coinManager.coinCount < cost) return;

        Carrying = Instantiate(prefab);

        carriedScripts = Carrying.GetComponents<MonoBehaviour>();
        carriedColliders = Carrying.GetComponents<Collider2D>();
        SetCarryingComponents(false);
    }

    //The entity being carried follows the mouse position
    public void OnDrag(PointerEventData eventData)
    {
        if (Carrying != null)
        {
            Carrying.transform.position = GetMouseWorldPosition();
        }
    }

    //Called by EventSystems when the mouse button is released after dragging
    //Attempts to place on a cell
    //If successful it will at the entity to the gridcell and deduct the cost from the player's coin count
    //If unsuccessful it will destroy the entity being carried
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Carrying == null) return;

        Collider2D hit = Physics2D.OverlapPoint(GetMouseWorldPosition(), layer);
        GridCell cell = hit != null ? hit.GetComponent<GridCell>() : null;

        if (cell != null && cell.TryPlaceExisting(Carrying))
        {
            SetCarryingComponents(true);
            coinManager.coinCount -= cost;
        }
        else
        {
            Destroy(Carrying);
        }

        Carrying = null;
    }
}
