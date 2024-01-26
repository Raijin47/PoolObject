using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UnitManager _unitManager;
    [SerializeField] private UnitInfoInterface _unitInfo;
    void Start()
    {
        _unitManager.Init();
        _unitInfo.Init();
    }
}