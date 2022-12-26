using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Axis
{
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
}

public class MouseAxis 
{
    public const string X = "Mouse X";
    public const string Y = "Mouse Y";
}

public class AnimationTag 
{
    public const string ZOOM_IN = "ZoomIn";
    public const string ZOOM_OUT = "ZoomOut";
    public const string SHOOT_TRIGGER = "Shoot";
    public const string AIM_PARAMETER = "Aim";
    public const string WALK_PARAMETER = "Walk";
    public const string RUN_PARAMETER = "Run";
    public const string ATTACK_TRIGGER = "Attack";
    public const string DEAD_TRIGGER = "Dead";
}

public class Tags 
{
    public const string LOOK_ROOT = "Look";
    public const string ZOOM_CAMERA = "FP Camera";
    public const string CROSSHAIR = "Crosshair";
    public const string ARROW_TAG = "ARROW";
    public const string AXE_TAG = "Axe";
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
}

public class GlobalSettings
{
    public static bool IsGamePlaying = false;
    public static bool IsGamePaused = false;
    public static bool IsGameFinished = false;

    public static int PlayerScore = 0;
}
