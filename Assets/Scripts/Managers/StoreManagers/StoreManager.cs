using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Extentions;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Enums;
using System.Collections;
using TMPro;
using DG.Tweening;

namespace Managers
{
    public class StoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<TextMeshProUGUI> levelTxt;
        [SerializeField] private List<TextMeshProUGUI> upgradeTxt;
        [SerializeField] private List<int> itemLevels;


        #endregion
        private PriceData _data;
        #endregion



        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _data = GetData();
        }
        private PriceData GetData() => Resources.Load<CD_Prices>("Data/CD_Prices").priceData;
        private void Start()
        {
            GetItemLevels();
            UpdateTexts();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

        }

        private void UnsubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        public void SelectItem(int id)
        {
            if (itemLevels[id] > 0) //çoktan satýn alýnmýþsa
            {
                SaveSignals.Instance.onSelectTarget?.Invoke(id, SaveLoadStates.TargetId,SaveFiles.SaveFile);
            }
        }

        public void UpgradeItem(int id)
        {
            if (itemLevels[id] == 1)
            {
                return;
            }

            if (ScoreSignals.Instance.onGetMoney() > _data.Prices[id])
            {
                ScoreSignals.Instance.onScoreDecrease?.Invoke(ScoreTypeEnums.Money, _data.Prices[id]); //paramýz azaldý

                itemLevels[id] = 1; //item leveli arttý
                SaveSignals.Instance.onChangeOpenedItems?.Invoke(itemLevels);
                SaveSignals.Instance.onSelectTarget?.Invoke(id, SaveLoadStates.TargetId, SaveFiles.SaveFile);

                UpdateTexts();
            }
        }

        private void GetItemLevels()
        {
            List<int> levels = new List<int>();
            levels = SaveSignals.Instance.onGetOpenedItems(SaveLoadStates.BuyedItemList,SaveFiles.SaveFile);
            if (levels.Count.Equals(0))
            {
                levels = new List<int>() { 1, 0, 0 };
            }

            itemLevels = levels;
        }

        private void UpdateTexts()
        {
            for (int i = 0; i < itemLevels.Count; i++)//textleri initialize et
            {
                if (itemLevels[i] == 0)
                {
                    levelTxt[i].text = "LOCKED";
                    upgradeTxt[i].text = "Buy\n" + _data.Prices[i];
                }
                else
                {
                    levelTxt[i].text = "Opened";
                    upgradeTxt[i].text = "Buyed";
                }
    
            }
        }

        public void CloseBtn()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StorePanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.CollectablePanel);

        }
    }
}