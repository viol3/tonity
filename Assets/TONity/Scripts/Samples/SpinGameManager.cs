using System.Collections;
using System.Collections.Generic;
using TMPro;
using TonitySDK;
using UnityEngine;
using UnityEngine.UI;

public class SpinGameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _yourWalletText;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _buySpinButton;
    [SerializeField] private SpinWheel _spinWheel;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Tonity.Instance.OnWalletConnectionSuccessed += OnWalletConnectionSuccessed;
        Tonity.Instance.OnWalletDisconnectionSuccessed += OnWalletDisconnectionSuccessed;
        Tonity.Instance.OnTransferTonSuccessed += OnTransferTonSuccessed;
        _spinWheel.OnSpinCompleted += SpinWheel_OnSpinCompleted;
    }

    private void SpinWheel_OnSpinCompleted()
    {
        UpdateUI();
    }

    private void OnTransferTonSuccessed(string result)
    {
        Debug.Log(result);
        _spinWheel.Spin();
        _buySpinButton.interactable = false;
    }

    private void OnWalletConnectionSuccessed(TonAccount tonAccount)
    {
        UpdateUI();
    }

    private void OnWalletDisconnectionSuccessed()
    {
        UpdateUI();
    }

    public void OnBuySpinButtonCLick()
    {
        if (_spinWheel.IsSpinning())
        {
            return;    
        }
        Tonity.Instance.TransferTon("0QBpcKrDazgqsghNEGdddB-apCa6G4gR1vE5jce0hiKg3usF", 0.05f, "alivehunter:1spin");
    }

    public void OnConnectWalletButtonClick()
    {
        Tonity.Instance.ConnectWallet();
    }

    void UpdateUI()
    {
        if(Tonity.Instance.IsWalletConnected())
        {
            string accountAddress = Tonity.Instance.GetAccountAddress();
            Debug.Log(accountAddress);
            accountAddress = accountAddress.Substring(0, 5) + "..." + accountAddress.Substring(accountAddress.Length - 3, 3);
            _yourWalletText.text = "Your Wallet\r\n" + accountAddress;
            _yourWalletText.gameObject.SetActive(true);
            _connectButton.gameObject.SetActive(false);
            _buySpinButton.gameObject.SetActive(true);
            _buySpinButton.interactable = !_spinWheel.IsSpinning();
        }
        else
        {
            _yourWalletText.gameObject.SetActive(false);
            _connectButton.gameObject.SetActive(true);
            _buySpinButton.gameObject.SetActive(false);
        }
    }
}
