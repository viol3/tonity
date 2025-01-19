using System.Collections;
using System.Collections.Generic;
using TMPro;
using TonitySDK;
using UnityEngine;
using UnityEngine.UI;

public class GameTestUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _debugText;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _disconnectButton;
    [SerializeField] private Button _addressButton;
    [SerializeField] private Button _payButton;
    private void Start()
    {
        Tonity.Instance.OnWalletConnectionSuccessed += OnWalletConnectionSuccessed;
        Tonity.Instance.OnWalletDisconnectionSuccessed += OnWalletDisconnectionSuccessed;
        Tonity.Instance.OnTransferTonSuccessed += OnTransferTonSuccessed;
    }

    private void OnTransferTonSuccessed(string result)
    {
        _debugText.text = result;
    }

    private void OnWalletConnectionSuccessed(TonAccount account)
    {
        _connectButton.interactable = false;
        _disconnectButton.interactable = true;
        _addressButton.interactable = true;
        _payButton.interactable = true;
    }

    private void OnWalletDisconnectionSuccessed()
    {
        _connectButton.interactable = true;
        _disconnectButton.interactable = false;
        _addressButton.interactable = false;
        _payButton.interactable = false;
    }

    public void OnPayButtonClick()
    {
        Tonity.Instance.TransferTon("0QBpcKrDazgqsghNEGdddB-apCa6G4gR1vE5jce0hiKg3usF", 0.1f, "alivehunter:5spin");
    }

    public void OnAccountButtonClick()
    {
        TonAccount tonAccount = Tonity.Instance.GetRawAccount();
        if (tonAccount != null)
        {
            _debugText.text = JsonUtility.ToJson(tonAccount);
        }
        else
        {
            _debugText.text = "null";
        }
    }

    public void OnConnectWalletButtonClick()
    {
        Tonity.Instance.ConnectWallet();
    }

    public void OnDisconnectWalletButtonClick()
    {
        Tonity.Instance.DisconnectWallet();
    }

    public void OnAddressButtonClick()
    {
        _debugText.text = Tonity.Instance.GetAccountAddress();
    }

    public void OnIsConnectedButtonClick()
    {
        _debugText.text = Tonity.Instance.IsWalletConnected().ToString();
    }
}
