using UnityEngine;

public class UnitLocator : MonoBehaviour
{
    [SerializeField] private UnitBase _unitBase;
    public UnitBase UnitBase => _unitBase;
}