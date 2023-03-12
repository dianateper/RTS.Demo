using System;
using CodeBase.PlayerData;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
   public class SelectUnitView : MonoBehaviour
   {
      [SerializeField] private Button _selectButton;
      [SerializeField] private TMP_Text _unitName;
      [SerializeField] private TMP_Text _unitCost;
      [SerializeField] private Image _image;

      private Unit _unit;
      private IPlayerStats _playerStats;

      public event Action<Unit> OnUnitSelect;
      
      public SelectUnitView Construct(Unit unit, IPlayerStats playerStats)
      {
         _unitName.text = unit.Name;
         _unitCost.text = unit.Cost.ToString();
         _image.sprite = unit.Sprite;
         _unit = unit;
         _playerStats = playerStats;
         _selectButton.onClick.AddListener(SelectType);
         _selectButton.interactable = _playerStats.Gold >= _unit.Cost;
         _playerStats.OnUnitStatsChanged += UpdateButtonsInteractive;
         return this;
      }

      private void OnDestroy()
      {
         _playerStats.OnUnitStatsChanged -= UpdateButtonsInteractive;
      }

      private void UpdateButtonsInteractive() => _selectButton.interactable = _playerStats.Gold >= _unit.Cost;

      private void SelectType() => OnUnitSelect?.Invoke(_unit);
   }
}
