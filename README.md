[TOC]

# MGS.Camera

## Summary

- Unity plugin for control camera in scene.

## Ability

- Move camera by mouse pointer drag.
- Camera rotate around target gameobject.
- Camera smooth align to mark gameobject.

## Install

- Unity --> Window --> Package Manager --> "+" --> Add package from git URL...

  ```text
  https://github.com/mogoson/MGS.Camera.git?path=/Assets
  ```

## Usage

- Reference the **Samples** to use the components in your project.

- If a camera attached the AroundCamera or AlignCamera component
    and is configured and selected, the "Current Around" panel will
    display in the lower left quarter of the scene window.
    Use it to set the default position and rotation of camera base on the
    around target in editor mode.

- If a gameobject attached the AreaMover component and is
    configured and selected, the "Current Offset" panel will display in
    the lower left quarter of the scene window.
    Use it to set the default position of gameobject base on the area
    center in editor mode.

- If a gameobject attached the AlignMark component and is selected, the
    "Camera Align Preview" panel will display in the lower right quarter
    of the scene window.
    Reference it to config the parameters of AlignMark component.

## Samples

- Unity --> Window --> Package Manager --> Packages-Mogoson --> Camera--> Samples.

------

Copyright © 2025 Mogoson.	mogoson@outlook.com