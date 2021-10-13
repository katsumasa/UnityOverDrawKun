# UnityOverDrawKun

overdraw check tool.
[English version README](REAMME_EN.md)

## 概要

描画時のオーバードロー（重ね塗り）を計測するツールです。</br>
***マルチディスプレイに対応しているプラットフォームでのみ計測可能です。***
</br></br>
![641b60a898bf20e97083f5adb1294113](https://user-images.githubusercontent.com/29646672/137099718-fa1850a7-1fe7-40b9-a1df-1a6d29cd1b4b.gif)



## インストール

`UnityOverDrawKun` 以下を計測する対象プロジェクトのAssetフォルダー以下へ配置して下さい。</br>

![image](https://user-images.githubusercontent.com/29646672/137099528-e92781a8-657c-4c91-b527-7924f24f08e2.png)



## セッティング

- 上記フォルダに含まれる`OverdrawCamera`を計測するSceneへ配置して下さい。</br>


![d5cf54a0dd8ba2f731fd92e732c46dc6](https://user-images.githubusercontent.com/29646672/137100995-bcd30b40-8cb9-43eb-811d-fc4b212273bc.png)

- `OverdrawCamera.MasterCamera`に計測するCameraを指定して下さい。</br>

![fac434078910df9e378ae8cf2de692f2](https://user-images.githubusercontent.com/29646672/137101051-5e1cc1f1-0076-4200-9adc-8388b672e335.png)

- GameViewを２個以上表示して下さい。Gameビューはビュー上の三からAdd Tabを選択しGameを選択することで増やすことが出来ます。</br>
 
![5fc934de91445b0ae77326436e154d7a](https://user-images.githubusercontent.com/29646672/137101120-b6d2d0ef-908c-4031-8e5a-29e86dc711e2.png)


- `OverdrawCamera`にアタッチされたいるCameraコンポーネントのTarget Display プロパティをDisplay1以外に設定して下さい。</br>
  
![0241bb02078058a3a206b29ba29f6cb4](https://user-images.githubusercontent.com/29646672/137101158-a6191d63-2590-4005-8046-c75f25ed2776.png)


MEMO

```:txt
 現在選択しているプラットフォームでマルチディスプレイが対応していない場合、Target Displayの項目は選択されません。プラットフォームをPC,Mac&Linux Standaloneなどへ変更して下さい。
```

## 計測方法

- Playボタンを押すことでOverdrawCamera.Camera.Target Displayへ指定したGameViewに計測結果が表示されます。
- 計測結果が明るい部分程、オーバードローが発生していることになります。
- インスペクターからOverdrawCameraにアタッチされているOverDrawKun.Recordボタンを押すことで計測結果を保存することが出来ます。</br>
  
![edf74eecdcc6a588c9f515bb4e3b968d](https://user-images.githubusercontent.com/29646672/137101207-f921811a-732c-4f49-8a5b-8a1cd58f1564.png)

 </br>
 計測結果はプロジェクトフォルダ以下にYYTTMMDDHHMMのフォルダを作成し出力されます。
</br>

![4b6bdd8b65e4b1ed0b71d05a4f9e9511](https://user-images.githubusercontent.com/29646672/137101240-0ad277ff-00c6-4900-b04f-bd9d56873d0b.png)



## 計測結果の解析

UnityOverdrawKunWindowで計測結果を解析することが可能です。

![0e28ce0ecf377d4d977aab0c4dbe3e05](https://user-images.githubusercontent.com/29646672/137102502-f118399b-fc8e-46f4-bccb-cccd89860ab3.gif)


### Windowの起動方法

Window->UnityOverdrawKunでWindowが開きます。

### 計測結果の取り込み方法

Window左上のOpenFolderアイコンから計測結果の保存先のディレクトリを開きます。

### 計測結果の確認方法

画面下部のスライドバーをスライドさせることで、各フレームで発生しているオーバードローの度合を確認する事が可能です。

### CSV形式での保存方法

画面上部のSave As CSVボタンから解析結果をCSV形式で保存する事が出来ます。

## その他

フィードバックをお待ちしております。

以上！
