using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsHandler
{
    
}
public enum ECollectible
{
    SpeedCollectible = 0,
    MultiplierCollectible,
    SlowDepleteCollectible,
};

// for gem balls identification
public enum EGemBall
{
    BLUE = 0,
    BROWN,
    GREEN,
    RED,
    WHITE,
    YELLOW,
    ENUM_SIZE,
    NONE
};
public enum EGemBallMobility
{
    STATIC = 0,
    MOVING,
    ENUM_SIZE,
    NONE
};