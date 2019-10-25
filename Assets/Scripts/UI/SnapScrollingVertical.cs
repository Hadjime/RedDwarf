//This script from tutorial by AndroidHelper.
//Link to the channel: https://www.youtube.com/c/huaweisonichelpAHRU

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SnapScrollingVertical : MonoBehaviour
{
    [Header("Controllers")]
    [Range(0, 500)]     public int panOffset;
    [Range(0f, 20f)]    public float returnSpeed;
    [Range(0f, 10f)]    public float scaleOffset;
    [Range(1f, 20f)]    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private List<GameObject> weaponCards;
    private int cardCount;
    private GameObject[] instPans;
    private RectTransform[] rectTranPanels;
    private Transform[] transformPanels;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedCardID;
    private bool isScrolling;

    private void Start()
    {
        contentRect = GetComponent<RectTransform>();
        weaponCards = new List<GameObject>();
        weaponCards = GetComponent<DownloadItemInShop>().GetWeaponCards();
        cardCount = weaponCards.Count;

        transformPanels = new Transform[cardCount];
        rectTranPanels = new RectTransform[cardCount];

        pansScale = new Vector2[weaponCards.Count];
        for (int i = 0; i < cardCount; i++)
        {
            transformPanels[i] = weaponCards[i].GetComponent<Transform>();
            rectTranPanels[i] = weaponCards[i].GetComponent<RectTransform>();
        }
        Vector2 pos = new Vector2(0, rectTranPanels[0].sizeDelta.y / 2);
        contentRect.anchoredPosition = pos;
    }

    private void FixedUpdate()
    {
        //OldVariantScroll();
        //MyVariantScrollHorizontal();
        MyVariantScrollAxisVertical();
    }

    private void MyVariantScrollAxisVertical()
    {
        //if (!isScrolling)   scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        var fixedDeltaTime = Time.fixedDeltaTime;
        for (int i = 0; i < cardCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - Mathf.Abs(transformPanels[i].localPosition.y));
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedCardID = i;
            }
            //Debug.Log("Select ID = " + selectedPanID);
            //Debug.Log("Local Pos = " + instPans[i].transform.localPosition.y);
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(transformPanels[i].localScale.x, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(transformPanels[i].localScale.y, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            transformPanels[i].localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, Mathf.Abs(transformPanels[selectedCardID].localPosition.y), returnSpeed * fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    private void MyVariantScrollHorizontal()
    {
        //if (!isScrolling)   scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        var fixedDeltaTime = Time.fixedDeltaTime;
        for (int i = 0; i < cardCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.y - Mathf.Abs(transformPanels[i].localPosition.y));
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedCardID = i;
            }
            //Debug.Log("Select ID = " + selectedPanID);
            //Debug.Log("Local Pos = " + -instPans[selectedPanID].transform.localPosition.x);
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            pansScale[i].x = Mathf.SmoothStep(transformPanels[i].localScale.x, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(transformPanels[i].localScale.y, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            transformPanels[i].localScale = pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, -transformPanels[selectedCardID].localPosition.x, returnSpeed * fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}