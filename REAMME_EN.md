# UnityOverDrawKun

overdraw check tool.

## Summary

Tool that allows to measures the overdraw when rendering. 
It can be measured only on platforms that support multiple displays.</br>
![img](Doc/641b60a898bf20e97083f5adb1294113.gif)

## Install

Place the `UnityOverDrawKun` under Asset folder like the image below:</br>
![img](Doc/943aa0cf3a2442674d4d3e0e9cac8132.png)

## Setting

- Place the `Overdraw Camera` inside in folder to the Scene you wish to measure.</br>
![img](Doc/d5cf54a0dd8ba2f731fd92e732c46dc6.png)
- Specify the camera to be measured in `OverdrawCamera.MasterCamera`.</br>
![img](Doc/fac434078910df9e378ae8cf2de692f2.png)
- Please display two or more GameViews. The Game view can be created by selecting Add Tab > Game.</br>
![img](Doc/5fc934de91445b0ae77326436e154d7a.png)
- Set the Target Display property of the Camera component attached to `OverdrawCamera` other than Display1.</br>
 ![img](Doc/0241bb02078058a3a206b29ba29f6cb4.png)

## Measurement Method

- By pressing the Play button, the measurement result will be displayed in the GameView specified in OverdrawCamera.Camera.Target Display.
- The brighter measurement result look dsiplays in the screen is where overdrawing has occured. 
- You can save the measurement result by pressing the OverDrawKun.Record button attached to the Overdraw Camera from the inspector.</br>
  
![img](Doc/edf74eecdcc6a588c9f515bb4e3b968d.png)
 </br>
 The measurement result gets outputted by creating a YYTTMMDDHHMM folder under the project folder.
</br>

 ![img](Doc/4b6bdd8b65e4b1ed0b71d05a4f9e9511.png)
 ![img](Doc/93f364a778ff61ffe72481e286919eae.png)

## Analysis of measurement results

It is possible to analyze the measurement result with UnityOverdrawKunWindow.

![img](Doc/f30e2a070ace58930d838263cc411df7.gif)

### Window starting method

Open Window->UnityOverdrawKun.

### How to capture measurement results

Open the directory where the measurement results are saved from the Open Folder icon on the upper left of the window.

### How to check the measurement result

By sliding the slide bar at the bottom of the screen, you can check the degree of overdraw that are occuring each frame.

### How to save in CSV format

Press Save As CSV button at the top of the screen.

## Other
Comments and feedback are welcome!</br>
