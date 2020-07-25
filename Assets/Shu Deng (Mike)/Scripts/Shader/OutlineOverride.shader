Shader "Hidden/OutlineOverride"
{
    SubShader
    {
        Pass
        {
            HLSLPROGRAM

            #pragma vertex Vert
            #pragma fragment Frag
            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 position : POSITION;
            };

            struct VertexOutput
            {
                float4 positionCS : SV_POSITION;
            };

            VertexOutput Vert(VertexInput input)
            {
                VertexOutput output;
                output.positionCS = UnityObjectToClipPos(input.position);
                return output;
            }

            float3 Frag(VertexOutput vertexOutput) : SV_Target
            {
                // positionCS.z is in range (0, 1) and outline buffer's color format is RGB
                // We use blue component to pass Z-vlaue
                float3 color = float3(1, 1, vertexOutput.positionCS.z);
                return color;
            }

            ENDHLSL
        }        
    }
    FallBack Off
}
