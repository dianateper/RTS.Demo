using System;
using CodeBase.PlayerLogic;
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
      private IPlayerBase _playerBase;

      public event Action<Unit> OnUnitSelect;
      
      public SelectUnitView Construct(Unit unit, IPlayerBase playerBase)
      {
         _unitName.text = unit.Name;
         _unitCost.text = unit.Cost.ToString();
         _image.sprite = unit.Sprite;
         _unit = unit;
         _playerBase = playerBase;
         _playerBase.PlayerStats.OnGoldChanged += UpdateButtonsInteractive;
         _selectButton.onClick.AddListener(SelectType);
         _selectButton.interactable = _playerBase.PlayerStats.Gold >= _unit.Cost;
         return this;
      }

      private void OnDestroy()
      {
         _playerBase.PlayerStats.OnGoldChanged -= UpdateButtonsInteractive;
      }

      private void UpdateButtonsInteractive() => _selectButton.interactable = _playerBase.PlayerStats.Gold >= _unit.Cost;

      private void SelectType() => OnUnitSelect?.Invoke(_unit);
   }
}
