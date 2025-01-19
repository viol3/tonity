using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    [SerializeField] private float _initialSpin = 100f;
    [SerializeField] private float _spinDecrease = 5f;
    [Space]
    [SerializeField] private RectTransform _spinRect;

    public event System.Action OnSpinCompleted;

    private bool _spinning = false;
    private float _spinAmount = 0;

    private void Update()
    {
        UpdateSpin();
    }

    void UpdateSpin()
    {
        if (_spinning)
        {
            _spinRect.localEulerAngles += Vector3.forward * _spinAmount * Time.deltaTime;
            _spinAmount -= Time.deltaTime * _spinDecrease * Random.Range(0.1f, 1.8f);
            if (_spinAmount <= 0f)
            {
                _spinning = false;
                _spinAmount = 0f;
                OnSpinCompleted?.Invoke();
            }
        }
    }

    public bool IsSpinning()
    {
        return _spinning;
    }

    public void Spin()
    {
        if(_spinning)
        {
            return;
        }
        _spinAmount = _initialSpin;
        _spinning = true;
    }
}
