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
        float endValue = toReverse.Evaluate(float.MaxValue);

        for (int i = 0; i < toReverse.length; ++i)
        {
            Keyframe tempKey = new Keyframe(toReverse[i].time, endValue - toReverse[i].value,
                -toReverse[i].inTangent, -toReverse[i].outTangent);
            result.AddKey(tempKey);
        }

        return result;
    }
}
