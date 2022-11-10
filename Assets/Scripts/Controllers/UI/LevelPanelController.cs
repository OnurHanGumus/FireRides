using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Data.UnityObject;
using DG.Tweening;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI scoreTxt, commentTxt, increasedTxt;
    [SerializeField] private Transform comboPanel;
    #endregion
    #region Private Variables
    private List<String> _commentsList;
    private int _comboCounter = 1; //start value is 1


    #endregion
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _commentsList = new List<string>();
        _commentsList = GetData();

    }
    private List<String> GetData() => Resources.Load<CD_Comments>("Data/CD_Comments").CommentsList;

    public void OnScoreUpdateText(ScoreTypeEnums type, int score)
    {
        scoreTxt.text = score.ToString();
    }

    public void OnHitTarget(int value)
    {
        StartCoroutine(Effect());
        increasedTxt.text = value.ToString();
        if (value == 2)
        {
            if (_comboCounter != 5)
            {
                _comboCounter += 1;
            }

            commentTxt.text = _commentsList[_comboCounter - 2];
            increasedTxt.text = _comboCounter.ToString();
            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, _comboCounter);
        }
        else
        {
            commentTxt.text = "     ";
            increasedTxt.text = value.ToString();
            ScoreSignals.Instance.onScoreIncrease?.Invoke(ScoreTypeEnums.Score, 1);
            if (value == 1)
            {
                ResetComboCounter();
            }
        }
    }

    private IEnumerator Effect()
    {
        comboPanel.DOScale(1, 0.5f).SetEase(Ease.Flash);
        yield return new WaitForSeconds(1.5f);
        comboPanel.localScale = Vector3.zero;
    }
    private void ResetComboCounter()
    {
        _comboCounter = 1;
    }

    public void OnResetComboCounter()
    {
        ResetComboCounter();
    }
    public void OnRestartLevel()
    {
        _comboCounter = 1;
    }
}
