# HololensEyeTracking
## Background
A Hololens project to track the speed and stability of users' eyeball movement.

## Build
### Build enviroments : 
* Unity 2019.3.14
* Microsoft Mixed Reality Toolkit (MRTK)
* Visual Studio 2019 

### Build Steps : 
* https://docs.google.com/document/d/12Yos1P32cHMhzrZiSXLxmNy_o4rdJCGI7W753XdnJ0E/edit?usp=sharing

## Platform
* Hololens 2

## SPEC
* 眼神順移
  * 追蹤使用者視線, 當使用者眼球移動視角大於設定時, 顯示立體模型.

* 眼神注視停留
  * 持續掃描使用者視線, 當使用者視線停留在一定範圍內時, 顯示立體模型.
  
* 教學模式.
  * 教學使用者熟悉兩種模式的使用.
  
* 數據紀錄
  * 將兩種模式的使用結果以可讀的方式記錄下來, 供研究分析.
  
* Configuration
  * 將教學與正式使用的設定黨以圖形介面供使用者調整, 並將調整結果紀錄於內部檔案.
  
## Change Log
* v0.0.1 enviorment 建立與架設.
* v0.0.2 eye tracking Proof Of Concept
* v0.0.3 UI & Scene
* v0.0.4 tutorial scenes
* v0.0.5 nomal mode
* v0.0.6 Localization & fix bugs.

## Demo

[![IMAGE ALT TEXT](http://img.youtube.com/vi/i92U5Zxd2pg/0.jpg)](https://www.youtube.com/watch?v=i92U5Zxd2pg "Hololens eye tracking.")

## FAQ
If you encounter the following error messages during execution:
> ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection. (at Assets/Scripts/Localization/LocalizationManager.cs:74)
> 
> NullReferenceException: Object reference not set to an instance of an object (at Assets/Scripts/Localization/LocalizationManager.cs:121)
>

Please follow these steps:

1. Visit this URL to download the Language.csv file. https://drive.google.com/u/0/uc?id=1u13HhO1cNbVguAMKbhQKS5QOkyLdzZ5l&export=download
2. Replace the existing Assets\Resources\Language.csv file with the downloaded Language.csv file.





