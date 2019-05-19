==========================================================================
  Copyright © 2017-2019 Mogoson. All rights reserved.
  Name: MGS-Camera
  Author: Mogoson   Version: 1.0.0   Date: 5/19/2019
==========================================================================
  [Summary]
    Unity plugin for control camera in scene.
--------------------------------------------------------------------------
  [Demand]
    Translate camera by mouse pointer drag.
    Camera rotate around target gameobject.
    Camera smooth align to mark gameobject.
--------------------------------------------------------------------------
  [Environment]
    Unity 5.0 or above.
    .Net Framework 3.5 or above.
--------------------------------------------------------------------------
  [Achieve]
    MouseTranslate : Translate gameobject by mouse pointer drag.

    AroundCamera : Camera rotate around target gameobject.

    AroundAlignCamera : Camera rotate around target gameobject and
    align to mark gameobject.

    MouseFocus : Align camera to gameobject on mouse pointer double
    click it.

    AlignMark : Mark gameobject as align target and config align
    parameters.
--------------------------------------------------------------------------
  [Usage]
    Reference the demos to use the components in your project.

    If a camera attached the AroundCamera or AroundAlignCamera component
    and is configured and selected, the "Current Around" panel will
    display in the lower left quarter of the scene window.
    Use it to set the default position and rotation of camera base on the
    around target in editor mode.

    If a gameobject attached the MouseTranslate component and is
    configured and selected, the "Current Offset" panel will display in
    the lower left quarter of the scene window.
    Use it to set the default position of gameobject base on the area
    center in editor mode.

    If a gameobject attached the AlignMark component and is selected, the
    "Camera Align Preview" panel will display in the lower right quarter
    of the scene window.
    Reference it to config the parameters of AlignMark component.
--------------------------------------------------------------------------
  [Demo]
    Demos in the path "MGS-Camera/Scenes" provide reference to you.
--------------------------------------------------------------------------
  [Resource]
    https://github.com/mogoson/MGS-Camera.
--------------------------------------------------------------------------
  [Contact]
    If you have any questions, feel free to contact me at mogoson@outlook.com.
--------------------------------------------------------------------------