# UnityOverDrawKun

overdraw check tool.

## 概要

描画時のオーバードロー（重ね塗り）を計測するツールです。
マルチディスプレイに対応しているプラットフォームでのみ計測可能です。</br>
![img](Doc/641b60a898bf20e97083f5adb1294113.gif)

## インストール

`UnityOverDrawKun` 以下を計測する対象プロジェクトのAssetフォルダー以下へ配置して下さい。</br>
![img](Doc/943aa0cf3a2442674d4d3e0e9cac8132.png)

## セッティング

- 上記フォルダに含まれる`OverdrawCamera`を計測するSceneへ配置して下さい。</br>
![img](Doc/d5cf54a0dd8ba2f731fd92e732c46dc6.png)
- `OverdrawCamera.MasterCamera`に計測するCameraを指定して下さい。</br>
![img](Doc/fac434078910df9e378ae8cf2de692f2.png)
- GameViewを２個以上表示して下さい。Gameビューはビュー上の三からAdd Tabを選択しGameを選択することで増やすことが出来ます。</br>
![img](Doc/5fc934de91445b0ae77326436e154d7a.png)
- `OverdrawCamera`にアタッチされたいるCameraコンポーネントのTarget Display プロパティをDisplay1以外に設定して下さい。</br>
 ![img](Doc/0241bb02078058a3a206b29ba29f6cb4.png)

## 計測方法

- Playボタンを押すことでOverdrawCamera.Camera.Target Displayへ指定したGameViewに計測結果が表示されます。
- 計測結果が明るい部分程、オーバードローが発生していることになります。
- インスペクターからOverdrawCameraにアタッチされているOverDrawKun.Recordボタンを押すことで計測結果を保存することが出来ます。</br>
  
![img](Doc/edf74eecdcc6a588c9f515bb4e3b968d.png)
 </br>
 計測結果はプロジェクトフォルダ以下にYYTTMMDDHHMMのフォルダを作成し出力されます。
</br>

 ![img](Doc/4b6bdd8b65e4b1ed0b71d05a4f9e9511.png)
 ![img](Doc/93f364a778ff61ffe72481e286919eae.png)

## 計測結果の解析

UnityOverdrawKunWindowで計測結果を解析することが可能です。

![img](Doc/f30e2a070ace58930d838263cc411df7.gif)

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
