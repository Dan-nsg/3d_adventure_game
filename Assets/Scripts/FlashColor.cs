using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;

    private Tween _currTween;

    private void Start() 
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");    
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if(!_currTween.IsActive())
            _currTween = meshRenderer.material.DOColor(Color.red, "_EmissionColor", .2f).SetLoops(2, LoopType.Yoyo);
    }
}
