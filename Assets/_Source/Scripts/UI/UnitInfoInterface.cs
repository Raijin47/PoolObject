using UnityEngine;
using UnityEngine.UI;

public class UnitInfoInterface : MonoBehaviour
{
    [SerializeField] private UnitManager _unitManager;
    [SerializeField] private Text _distanceText;
    [SerializeField] private Text _speedText;
    [SerializeField] private Text _intervalText;
    public void Init()
    {
        _distanceText.text = _unitManager.Distance.ToString();
        _speedText.text = _unitManager.Speed.ToString();
        _intervalText.text = _unitManager.IntervalSpawn.ToString();
    }
}