using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyGenerator : MonoBehaviour
{
    private SpriteRenderer _placeHolder;

    [SerializeField] AnimatorOverrideController _override;

    private Animator _animator;

    //private SpriteRenderer _hairRenderer;
    //private SpriteRenderer _bodyRenderer;

    //[SerializeField] private Sprite[] hairSprites;

    private void Start()
    {
        _placeHolder = GetComponent<SpriteRenderer>();
        _placeHolder.enabled = false;

        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _override;

        //SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
        //_hairRenderer = renderers[0];
        //_bodyRenderer = renderers[1];

        //ChooseRandomHair();
        
    }

    //private void ChooseRandomHair()
    //{
    //    _hairRenderer.sprite = hairSprites[Random.Range(0, hairSprites.Length)];
    //}

}
