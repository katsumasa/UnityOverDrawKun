# UnityOverDrawKun
overdraw check tool.
## 概要
描画時のオーバードロー（重ね塗り）を計測するツールです。
マルチディスプレイに対応しているプラットフォームでのみ計測可能です。
![alt text](Doc/641b60a898bf20e97083f5adb1294113.gif)
## インストール
`Assets\OverDrawKun` 以下を計測する対象プロジェクトへコピーして使用して下さい。
## セッティング
- 上記フォルダに含まれる`OverdrawCamera`を計測するSceneへ配置して下さい。
- `OverdrawCamera.MasterCamera`に計測するCameraを指定して下さい。
- GameViewを２個以上表示して下さい。Gameビューはビュー上の三からAdd Tabを選択しGameを選択することで増やすことが出来ます。
- `OverdrawCamera`にアタッチされたいるCameraコンポーネントのTarget Display プロパティをDisplay1以外に設定して下さい。
 
 ## 計測方法
 - Playボタンを押すことでOverdrawCamera.Camera.Target Displayへ指定したGameViewに計測結果が表示されます。
 - 計測結果が明るい部分がよりオーバードローが発生していることになります。
 - インスペクターからOverdrawCameraにアタッチされているOverDrawKun.Recordボタンを押すことで計測結果を保存することが出来ます。
 - 計測結果はプロジェクトフォルダ以下にYYTTMMDDHHMMのフォルダを作成し出力されます。
