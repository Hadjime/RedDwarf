//This script from tutorial by AndroidHelper.
//Link to the channel: https://www.youtube.com/c/huaweisonichelpAHRU

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using InternalAssets.Scripts.Inventory;

public class SnapScrolling : MonoBehaviour
{
    [Header("Controllers")]
    [Range(0, 500)]     public int panOffset;
    [Range(0f, 20f)]    public float returnSpeed;
    [Range(0f, 10f)]    public float scaleOffset;
    [Range(1f, 20f)]    public float scaleSpeed;
    [Header("Other Objects")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;

    private List<GameObject> _weaponCards;
    private int _cardCount;
    private GameObject[] _instPans;
    private RectTransform[] _rectTranPanels;
    private Transform[] _transformPanels;
    private Vector2[] _pansScale;

    private RectTransform _contentRect;
    private Vector2 _contentVector;

    private int _selectedCardId;
    private bool _isScrolling;

    private void Start()
    {
        _contentRect = GetComponent<RectTransform>();
        _weaponCards = new List<GameObject>();
        _weaponCards = GetComponent<DownloadItemInShop>().GetListWeaponCards();
        _cardCount = _weaponCards.Count;

        _transformPanels = new Transform[_cardCount];
        _rectTranPanels = new RectTransform[_cardCount];

        _pansScale = new Vector2[_weaponCards.Count];
        for (int i = 0; i < _cardCount; i++)
        {
            _transformPanels[i] = _weaponCards[i].GetComponent<Transform>();
            _rectTranPanels[i] = _weaponCards[i].GetComponent<RectTransform>();
        }
        Vector2 pos = new Vector2(0, _rectTranPanels[0].sizeDelta.y / 2);
        _contentRect.anchoredPosition = pos;
    }

    private void FixedUpdate()
    {
        //OldVariantScroll();
        //MyVariantScrollHorizontal();
        ScrollAxisVertical();
    }
    public int GetSelectedCardId()
    {
        return _selectedCardId;
    }
    private void ScrollAxisVertical()
    {
        //if (!isScrolling)   scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        var fixedDeltaTime = Time.fixedDeltaTime;
        for (int i = 0; i < _cardCount; i++)
        {
            float distance = Mathf.Abs(_contentRect.anchoredPosition.y - Mathf.Abs(_transformPanels[i].localPosition.y));
            if (distance < nearestPos)
            {
                nearestPos = distance;
                _selectedCardId = i;
            }
            //Debug.Log("Select ID = " + selectedPanID);
            //Debug.Log("Local Pos = " + instPans[i].transform.localPosition.y);
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            _pansScale[i].x = Mathf.SmoothStep(_transformPanels[i].localScale.x, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            _pansScale[i].y = Mathf.SmoothStep(_transformPanels[i].localScale.y, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            _transformPanels[i].localScale = _pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 400 && !_isScrolling) scrollRect.inertia = false;
        if (_isScrolling || scrollVelocity > 400) return;
        _contentVector.y = Mathf.SmoothStep(_contentRect.anchoredPosition.y, Mathf.Abs(_transformPanels[_selectedCardId].localPosition.y), returnSpeed * fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }

    private void ScrollAxisHorizontal()
    {
        //if (!isScrolling)   scrollRect.inertia = false;

        float nearestPos = float.MaxValue;
        var fixedDeltaTime = Time.fixedDeltaTime;
        for (int i = 0; i < _cardCount; i++)
        {
            float distance = Mathf.Abs(_contentRect.anchoredPosition.y - Mathf.Abs(_transformPanels[i].localPosition.y));
            if (distance < nearestPos)
            {
                nearestPos = distance;
                _selectedCardId = i;
            }
            //Debug.Log("Select ID = " + selectedPanID);
            //Debug.Log("Local Pos = " + -instPans[selectedPanID].transform.localPosition.x);
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
            _pansScale[i].x = Mathf.SmoothStep(_transformPanels[i].localScale.x, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            _pansScale[i].y = Mathf.SmoothStep(_transformPanels[i].localScale.y, scale + 0.3f, scaleSpeed * fixedDeltaTime);
            _transformPanels[i].localScale = _pansScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !_isScrolling) scrollRect.inertia = false;
        if (_isScrolling || scrollVelocity > 400) return;
        _contentVector.x = Mathf.SmoothStep(_contentRect.anchoredPosition.x, -_transformPanels[_selectedCardId].localPosition.x, returnSpeed * fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }

    public void Scrolling(bool scroll)
    {
        _isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}