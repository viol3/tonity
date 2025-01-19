mergeInto(LibraryManager.library, 
{
	GetAccountAddressInternal: function(testnet)
	{
		var tonConnectUI = unityInstanceRef.tonConnectUI
		if(!tonConnectUI.connected)
		{
			console.log("ton sdk is not connected")
			return "";
		}
		try
		{
			const address = new unityInstanceRef.TonWeb.utils.Address(tonConnectUI.account.address);
			var nonBouncableAddress = address.toString(true, true, false, testnet)
			console.log(nonBouncableAddress)
			var buffer = _malloc(lengthBytesUTF8(nonBouncableAddress) + 1);
			stringToUTF8(nonBouncableAddress, buffer, lengthBytesUTF8(nonBouncableAddress) + 1);
			//writeStringToMemory(nonBouncableAddress, buffer);
			//return buffer;
			return buffer;
			
		}
		catch(e)
		{
			unityInstanceRef.SendMessage("Tonity", "OnAddressFailedInternal", JSON.stringify(e)); 
			return "";
		}
		
		//return stringToUTF8(nonBouncableAddress, buffer, bufferSize);
	},
	
	IsWalletConnectedInternal:  function () 
    {
		return unityInstanceRef.tonConnectUI.connected;
    },
	
    ConnectWalletInternal: async function () 
    {
		var tonConnectUI = unityInstanceRef.tonConnectUI
		if(tonConnectUI.connected)
		{
			unityInstanceRef.SendMessage("Tonity", "OnWalletConnectionSuccessedInternal", JSON.stringify(tonConnectUI.account));  
			return;
		}
		try
		{
			await tonConnectUI.connectWallet();
			if(!tonConnectUI.connected)
			{
				unityInstanceRef.SendMessage("Tonity", "OnWalletConnectionFailedInternal", JSON.stringify(""));
				return;
			}
			unityInstanceRef.SendMessage("Tonity", "OnWalletConnectionSuccessedInternal", JSON.stringify(tonConnectUI.account));
		}
		catch(e)
		{
			console.log(e)
			unityInstanceRef.SendMessage("Tonity", "OnWalletConnectionFailedInternal", JSON.stringify(e)); 
		}
		  
    },
	
	DisconnectWalletInternal: async function () 
    {
		var tonConnectUI = unityInstanceRef.tonConnectUI
		if(!tonConnectUI.connected)
		{  
			return;
		}
		try
		{
			await tonConnectUI.disconnect();
			unityInstanceRef.SendMessage("Tonity", "OnWalletDisconnectionSuccessedInternal");
		}
		catch(e)
		{
			unityInstanceRef.SendMessage("Tonity", "OnWalletDisconnectionFailedInternal", JSON.stringify(e)); 
		}
		
    },
	
	TransferTonInternal: async function(targetAddress, amountToPay, payloadRaw)
	{
		const destinationAddress = UTF8ToString(targetAddress)
		const payloadString = UTF8ToString(payloadRaw)
		var tonConnectUI = unityInstanceRef.tonConnectUI
		var TonWeb = unityInstanceRef.TonWeb
		let a = new TonWeb.boc.Cell();
		a.bits.writeUint(0, 32);
		a.bits.writeString(payloadString);
		let payloadFinal = TonWeb.utils.bytesToBase64(await a.toBoc());
		const transaction = 
		{
			validUntil: Math.floor(Date.now() / 1000) + 60, // 60 sec
			messages: 
			[{
				address: destinationAddress,
				amount: amountToPay,
			    payload: payloadFinal // just for instance. Replace with your transaction payload or remove
			}]
		}

		try 
		{
			
			const result = await tonConnectUI.sendTransaction(transaction);
			console.log(result)
			unityInstanceRef.SendMessage("Tonity", "OnTransferTonSuccessedInternal", JSON.stringify(result)); 
			// you can use signed boc to find the transaction 
			// const someTxData = await myAppExplorerService.getTransaction(result.boc);
			// alert('Transaction was sent successfully', someTxData);
		} 
		catch (e) 
		{
			console.log("Error occured while payment of " + amountToPay + " with '" + payloadString + "' payload to " +  destinationAddress);
			console.log(e);
			unityInstanceRef.SendMessage("Tonity", "OnTransferTonFailedInternal", JSON.stringify(e)); 
		}
	},
	
	OpenNewTab : function(url)
    {
        url = Pointer_stringify(url);
        window.open(url,'_blank');
    }
});