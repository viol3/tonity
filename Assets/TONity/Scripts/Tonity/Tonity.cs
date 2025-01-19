using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace TonitySDK
{
    public class Tonity : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void ConnectWalletInternal();

        [DllImport("__Internal")]
        private static extern void DisconnectWalletInternal();

        [DllImport("__Internal")]
        private static extern bool IsWalletConnectedInternal();

        [DllImport("__Internal")]
        private static extern string GetAccountAddressInternal(bool testnet);
        [DllImport("__Internal")]
        private static extern void TransferTonInternal(string targetAddress, uint amountToPay, string payload);
        [DllImport("__Internal")]
        private static extern void OpenNewTab(string url);

        [SerializeField] private bool _testnet = true;

        private TonAccount _tonAccount;

        public event Action<TonAccount> OnWalletConnectionSuccessed;
        public event Action<string> OnWalletConnectionFailed;

        public event Action OnWalletDisconnectionSuccessed;
        public event Action<string> OnWalletDisconnectionFailed;

        public event Action<string> OnTransferTonSuccessed;
        public event Action<string> OnTransferTonFailed;

        public event Action<string> OnAddressFailed;

        public event Action<string> OnStatusChanged;

        public static Tonity Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ConnectWallet()
        {
            if (_tonAccount != null)
            {
                Debug.LogWarning("Already connected to a wallet.");
                return;
            }
            ConnectWalletInternal();
        }

        public void DisconnectWallet()
        {
            if (_tonAccount == null)
            {
                Debug.LogWarning("There is no connection to a wallet.");
                return;
            }
            DisconnectWalletInternal();
        }

        public TonAccount GetRawAccount()
        {
            return _tonAccount;
        }

        public void TransferTon(string targetAddress, float amountToPay, string payload)
        {
            if (!IsWalletConnected())
            {
                Debug.LogWarning("A wallet should be connected to proceed transfer.");
                return;
            }
            TransferTonInternal(targetAddress, (uint)Mathf.RoundToInt(amountToPay * 1000000000), payload);
        }

        public string GetAccountAddress()
        {
            if (!IsWalletConnected())
            {
                Debug.Log("Wallet not connected");
                return null;
            }
            return GetAccountAddressInternal(_testnet);
        }

        public bool IsWalletConnected()
        {
            return IsWalletConnectedInternal();
        }

        private void OnWalletConnectionSuccessedInternal(string tonAccountJSON)
        {
            Debug.Log("Ton Account : " + tonAccountJSON);
            _tonAccount = JsonUtility.FromJson<TonAccount>(tonAccountJSON);
            OnWalletConnectionSuccessed?.Invoke(_tonAccount);
        }

        private void OnWalletConnectionFailedInternal(string error)
        {
            //handle error
            _tonAccount = null;
            OnWalletConnectionFailed?.Invoke(error);
        }

        private void OnWalletDisconnectionSuccessedInternal()
        {
            _tonAccount = null;
            OnWalletDisconnectionSuccessed?.Invoke();
        }

        private void OnWalletDisconnectionFailedInternal(string error)
        {
            //handle error
            OnWalletDisconnectionFailed?.Invoke(error);
        }

        private void OnTransferTonSuccessedInternal(string result)
        {
            Debug.Log("Transfer Ton Successed => " + result);
            OnTransferTonSuccessed?.Invoke(result);
        }

        private void OnTransferTonFailedInternal(string error)
        {
            //handle error
            OnTransferTonFailed?.Invoke(error);
        }

        private void OnAddressFailedInternal(string error)
        {
            //handle error
            OnAddressFailed?.Invoke(error);
        }

        private void OnStatusChangedInternal(string walletAndWalletInfo)
        {
            Debug.Log("On Status Changed => " + walletAndWalletInfo);
            OnStatusChanged?.Invoke(walletAndWalletInfo);
            if (!IsWalletConnectedInternal() && _tonAccount != null)
            {
                OnWalletDisconnectionSuccessedInternal();
            }
        }


    }
}