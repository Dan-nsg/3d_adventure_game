using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    private bool _endGame = false;

    public int currentLevel = 1;

    private void Awake() 
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }

    private void OnTriggerEnter(Collider other) 
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            ShowEndGame();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;

        foreach (GameObject obj in endGameObjects)
        {
            obj.SetActive(true);
            obj.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
        }

        SaveManager.Instance.SaveLastLevel(currentLevel);
    }
}