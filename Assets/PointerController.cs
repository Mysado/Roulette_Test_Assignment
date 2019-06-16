﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    public Transform toDrag;
    Vector3 rayStart;

    private BetsController betsController;

    private void Start()
    {
        betsController = GetComponent<BetsController>();
    }
    void Update()
    {
        
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Draggable"))
            {
                toDrag = hit.transform;
                dist = hit.transform.position.z - Camera.main.transform.position.z;
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                offset = toDrag.position - v3;
                dragging = true;
            }
        }
        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            CheckIfOnSlot();
            dragging = false;
        }
    }

    public void CheckIfOnSlot()
    {
        RaycastHit hit;

        rayStart = new Vector3(toDrag.position.x + 0.7f, toDrag.position.y + 0.7f, toDrag.position.z);
        if (Physics.Raycast(rayStart, Vector3.forward, out hit) && (hit.collider.tag == "Slot"))
        {
            betsController.PlaceBet(toDrag.gameObject, hit.collider.gameObject);
        }
        
    }
}
