# What is it

This is a windows WPF project to get system info and show it on one window.

And it will be a part of DeviceInfoSync project. It can transmission info to InfoScreen.

# About DeviceInfoSync project

DeviceInfoSync is a project to show PC/Mac system info on anthor device(Now it is an Android tablet with e-paper screen).

DeviceInfoSync is undone, and will add some fun device support when it finished Android part, like show Info on Raspberry Pi, SPI screen or any screen you want.

There are two part of this:

1. DeviceInfoSyncClient: A client run on PC, show info on PC and transfer info to InfoScreen. (Like CPU usage/ memory usage/ disk status/ Network..etc...)

2. InfoScreen: A terminal device run on info display device (now is an android, maybe will add arm linux device support.), When has DeviceInfoSyncClient running, it will show target pc's status, when it idle, it will show other info (Calendar/Email/Todoist..etc..)

For more infomation about other paltform dev, follow those link:

[InfoScreen_Android](https://github.com/MortalKim/InfoScreen_Android)

[DeviceInfoSyncClient_MacOS](https://github.com/MortalKim/DeviceInfoSyncClient_MacOS)

# ScreenShot

Project is developing, Screenshot coming soon.

# Acknowledgement

DeviceInfoSyncClient_Windows has use code by [SalkCoding/ComputerInfo](https://github.com/SalkCoding/ComputerInfo) project

# License

This work is licensed under GNU GPL v3.0 or later terms.