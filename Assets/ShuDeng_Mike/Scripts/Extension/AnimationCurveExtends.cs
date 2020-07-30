using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationCurveExtends
{
    // Summary:
    //     Return the reversed version of the animationcurve.
    //     This is used to reverse the changing of the property described by the animationcurve.
    public static AnimationCurve ReverseAnimationCurve(this AnimationCurve toReverse)
    {
        AnimationCurve result = new AnimationCurve();
        float endTime = toReverse.keys[toReverse.length - 1].time;

        for (int i = 0; i < toReverse.length; ++i)
        {
            Keyframe tempKey = new Keyframe(endTime - toReverse[i].time, toReverse[i].value,
                - toReverse[i].outTangent, - toReverse[i].inTangent);
            result.AddKey(tempKey);
        }

        return result;
    }
}
