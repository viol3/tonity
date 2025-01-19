
<h1 align="center">
  <br>
  <a href="#"><img src="https://raw.githubusercontent.com/viol3/tonity/main/Assets/TONity/Textures/tonity.png" alt="TONity" width="512"></a>
  <br>
  TONity
  <br>
</h1>

<h4 align="center">TON Connect UI integration that allows transaction without leaving game app for Unity WebGL.</h4>

<p align="center">
   <a href="#">
    <img src="https://img.shields.io/badge/version-0.0.1-green" alt="version">
  </a>
  <a href="https://tonviewer.com/EQDDDTtu2nTIUK_uhVnXm8iacrZtIQNFH6OOToC4qJgP7yrj">
    <img src="https://img.shields.io/badge/$-donate-blue">
  </a>
  <a href="https://unity.com/releases/editor/archive">
    <img src="https://img.shields.io/badge/unity-2022.3.55f1-white">
  </a>
</p>

<p align="center">
  <a href="#key-features">Key Features</a> •
  <a href="#how-to-use">How To Use</a> •
  <a href="#download">Download</a> •
  <a href="#credits">Credits</a> •
  <a href="#license">License</a>
</p>

![screenshot](https://raw.githubusercontent.com/viol3/tonity/main/tonity_ss.gif)

## Key Features

* Connecting to any TON-compatible wallet with responsible TON Connect UI.
* Proceeding transactions without leaving game app.
* Supports any platform includes Telegram Mini Apps that can run WebGL and Javascript.
* Testnet support
* Samples; A basic UI and Fortune Wheel mechanic.

## How To Use

To clone and run this application, you'll need [Git](https://git-scm.com) and [Unity](https://unity.com/releases/editor/archive) installed on your computer. From your command line:

```bash
# Clone this repository
$ git clone https://github.com/viol3/tonity
```

Drag-drop Tonity prefab into your starter scene and in any script, you can start to connect by calling:

```csharp
// Connect to wallet
$ Tonity.Instance.ConnectWallet()
```

To send TON to another wallet:

```csharp
// Connect to wallet
$ Tonity.Instance.TransferTon("[WALLET_ADDRESS]", [AMOUNT IN FLOAT], "[COMMENT]");
```

If you want to deploy the game into Telegram Mini Apps, you need to setup a manifest.json file such as "WebGL Templates/Tonity/manifest.json". After you uploaded the web files into your hosting, you need to paste your manifest.json link(it should be accessible globally) into index.html file, line 32.

## Download

You can [download](https://github.com/viol3/tonity/releases/tag/v0.0.1) the latest installable plugin version to any Unity Project.


## Credits

This software uses the following packages and assets:

- [TON Connect UI](https://www.npmjs.com/package/@tonconnect/ui)
- [TON Web JS](https://github.com/toncenter/tonweb)
- 2D assets are taken from [FlatIcon](flaticon.com) [Freepik](freepik.com) [Bermuda Games](bermuda.gs)


## Support

<a href="https://buymeacoffee.com/aliveavcisi" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/purple_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

<a href="https://tonviewer.com/EQDDDTtu2nTIUK_uhVnXm8iacrZtIQNFH6OOToC4qJgP7yrj">
    <img src="https://img.shields.io/badge/$-donate-blue">
  </a>

## License

You need to give me a place in your credits page. Allowed to use in any commercial or non-commercial projects. We are not responsible for any damage that may occur while using this project.

---

> GitHub [@viol3](https://github.com/viol3) &nbsp;&middot;&nbsp;
> Twitter [@aliveavcisi](https://x.com/aliveavcisi)
> YouTube [@aliveavcisi](https://www.youtube.com/@aliveavcisi)

