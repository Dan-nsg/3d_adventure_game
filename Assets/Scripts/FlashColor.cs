using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;

    private Tween _currTween;

    public string colorParameter = "_EmissionColor";

    private void OnValidate() 
    {
        if(meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if(skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if(meshRenderer != null && !_currTween.IsActive())
            _currTween = meshRenderer.material.DOColor(Color.red, colorParameter, .2f).SetLoops(2, LoopType.Yoyo);

        if(skinnedMeshRenderer != null && !_currTween.IsActive())
            _currTween = skinnedMeshRenderer.material.DOColor(Color.red, colorParameter, .2f).SetLoops(2, LoopType.Yoyo);    
    }
}
