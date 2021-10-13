# UnityOverDrawKun

overdraw check tool.

## Summary

Tool that allows to measures the overdraw when rendering. 
It can be measured only on platforms that support multiple displays.</br>
![641b60a898bf20e97083f5adb1294113](https://user-images.githubusercontent.com/29646672/137099718-fa1850a7-1fe7-40b9-a1df-1a6d29cd1b4b.gif)


## Install

Place the `UnityOverDrawKun` under Asset folder like the image below:</br>
![image](https://user-images.githubusercontent.com/29646672/137099528-e92781a8-657c-4c91-b527-7924f24f08e2.png)


## Setting

- Place the `Overdraw Camera` inside in folder to the Scene you wish to measure.</br>
![d5cf54a0dd8ba2f731fd92e732c46dc6](https://user-images.githubusercontent.com/29646672/137100995-bcd30b40-8cb9-43eb-811d-fc4b212273bc.png)


- Specify the camera to be measured in `OverdrawCamera.MasterCamera`.</br>
![fac434078910df9e378ae8cf2de692f2](https://user-images.githubusercontent.com/29646672/137101051-5e1cc1f1-0076-4200-9adc-8388b672e335.png)


- Please display two or more GameViews. The Game view can be created by selecting Add Tab > Game.</br>
 ![5fc934de91445b0ae77326436e154d7a](https://user-images.githubusercontent.com/29646672/137101120-b6d2d0ef-908c-4031-8e5a-29e86dc711e2.png)


- Set the Target Display property of the Camera component attached to `OverdrawCamera` other than Display1.</br>
 ![0241bb02078058a3a206b29ba29f6cb4](https://user-images.githubusercontent.com/29646672/137101158-a6191d63-2590-4005-8046-c75f25ed2776.png)


## Measurement Method

- By pressing the Play button, the measurement result will be displayed in the GameView specified in OverdrawCamera.Camera.Target Display.
- The brighter measurement result look dsiplays in the screen is where overdrawing has occured. 
- You can save the measurement result by pressing the OverDrawKun.Record button attached to the Overdraw Camera from the inspector.</br>
  
![edf74eecdcc6a588c9f515bb4e3b968d](https://user-images.githubusercontent.com/29646672/137101207-f921811a-732c-4f49-8a5b-8a1cd58f1564.png)

 </br>
 The measurement result gets outputted by creating a YYTTMMDDHHMM folder under the project folder.
</br>

![4b6bdd8b65e4b1ed0b71d05a4f9e9511](https://user-images.githubusercontent.com/29646672/137101240-0ad277ff-00c6-4900-b04f-bd9d56873d0b.png)



## Analysis of measurement results

It is possible to analyze the measurement result with UnityOverdrawKunWindow.

![0e28ce0ecf377d4d977aab0c4dbe3e05](https://user-images.githubusercontent.com/29646672/137102502-f118399b-fc8e-46f4-bccb-cccd89860ab3.gif)


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
