using UnityEngine;
using UnityEngine.UI;

namespace _Source.Scripts.UI.StoreMenu
{
    public class ShipSelectionButton : MonoBehaviour
    {
        [SerializeField] private ShipItemView[] _ships;
        [SerializeField] private ShipsStoreByTime _shipsByTime;
        [SerializeField] private Color _backGroundColor = new Color(1f, 1f, 1f, 118f / 255f);
        [SerializeField] private Color _backGroundColorSelect = Color.white;

        private Image[] _backgroundImages;
        private Image _backgroundImageByTime;

        private void Start()
        {
            _backgroundImages = new Image[_ships.Length];

            SetDefaultColor();
        }

        private void OnEnable()
        {
            OnSubscribe();
        }

        private void OnDisable()
        {
            OnUnsubscribe();
        }

        private void SetDefaultColor()
        {
            for (int i = 0; i < _ships.Length; i++)
            {
                if (_ships[i] == null)
                    continue;

                _backgroundImages[i] = _ships[i].GetComponentInChildren<Image>();

                if (_backgroundImages[i] != null)
                {
                    _backgroundImages[i].color = _backGroundColor;
                }
            }

            if (_shipsByTime != null)
            {
                _backgroundImageByTime = _shipsByTime.GetComponentInChildren<Image>();
                if (_backgroundImageByTime != null)
                {
                    _backgroundImageByTime.color = _backGroundColor;
                }
            }
        }

        public void Select(int numberShip)
        {
            for (int i = 0; i < _ships.Length; i++)
            {
                if (_backgroundImages[i] == null)
                    continue;

                _backgroundImages[i].color = (i == numberShip)
                    ? _backGroundColorSelect
                    : _backGroundColor;
            }

            if (_backgroundImageByTime != null)
            {
                _backgroundImageByTime.color = (_shipsByTime.ShipNumber == numberShip)
                    ? _backGroundColorSelect
                    : _backGroundColor;
            }
        }

        private void OnSubscribe()
        {
            foreach (ShipItemView ship in _ships)
            {
                ship.IsSelection += Select;
            }

            if (_shipsByTime != null)
                _shipsByTime.IsSelection += Select;
        }

        private void OnUnsubscribe()
        {
            foreach (ShipItemView ship in _ships)
            {
                ship.IsSelection -= Select;
            }

            if (_shipsByTime != null)
                _shipsByTime.IsSelection -= Select;
        }
    }
}