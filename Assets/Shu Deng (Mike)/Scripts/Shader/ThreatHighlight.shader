Shader "Unlit/ThreatHighlight"
{
    Properties
    {
        [HDR] _FresnelColour("Fresnel Colour", Color) = (1.414675,1.421379,1.416956,0)
        _FresnelPower("Fresnel Power", Float) = 3
        [ToggleUI] _IsTwinkling("IsTwinkling", Float) = 1
        [ToggleUI] _IsHighlighted("IsHighlighted", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha  //Src is the current calculated color
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityShaderVariables.cginc"
            #include "UnityCG.cginc"
            #include "HLSLSupport.cginc"

            CBUFFER_START(UnityPerMaterial)
            float4 _FresnelColour;
            float _FresnelPower;
            float _IsTwinkling;
            float _IsHighlighted;
            CBUFFER_END

            struct SurfaceDescriptionInputs
            {
                float3 WorldSpaceNormal;
                float3 WorldSpaceViewDirection;
            };

            struct GraphVertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct GraphVertexOutput
            {
                float4 position : SV_POSITION;
                float3 WorldSpaceNormal : TEXCOORD0;
                float3 WorldSpaceViewDirection : TEXCOORD1;

            };

            void Unity_FresnelEffect_float(float3 Normal, float3 ViewDir, float Power, out float Out)
            {
                Out = pow((1.0 - saturate(dot(normalize(Normal), normalize(ViewDir)))), Power);
            }

            void Unity_Multiply_float(float4 A, float4 B, out float4 Out)
            {
                Out = A * B;
            }

            void Unity_Remap_float(float In, float2 InMinMax, float2 OutMinMax, out float Out)
            {
                Out = OutMinMax.x + (In - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
            }

            void Unity_Branch_float(float Predicate, float True, float False, out float Out)
            {
                Out = lerp(False, True, Predicate);
            }

            void Unity_Branch_float4(float Predicate, float4 True, float4 False, out float4 Out)
            {
                Out = lerp(False, True, Predicate);
            }

            GraphVertexInput PopulateVertexData(GraphVertexInput v)
            {
                return v;
            }

            float4 PopulateSurfaceData(SurfaceDescriptionInputs IN)
            {
                float4 surface;
                float _FresnelEffect_E7A0EDB3_Out_3;
                Unity_FresnelEffect_float(IN.WorldSpaceNormal, IN.WorldSpaceViewDirection, _FresnelPower, _FresnelEffect_E7A0EDB3_Out_3);
                float4 _Multiply_CCE9D4BB_Out_2;
                Unity_Multiply_float((_FresnelEffect_E7A0EDB3_Out_3.xxxx), _FresnelColour, _Multiply_CCE9D4BB_Out_2);                

                float _Remap_F11A93D1_Out_3;
                Unity_Remap_float(sin(_Time.y * 3), float2 (-1, 1), float2 (0, 1), _Remap_F11A93D1_Out_3);
                float _Branch_CB18BEDC_Out_3;
                Unity_Branch_float(_IsTwinkling, _Remap_F11A93D1_Out_3, 1, _Branch_CB18BEDC_Out_3);
                float4 _Multiply_4220803C_Out_2;
                Unity_Multiply_float(_Multiply_CCE9D4BB_Out_2, (_Branch_CB18BEDC_Out_3.xxxx), _Multiply_4220803C_Out_2);

                Unity_Branch_float4(_IsHighlighted, _Multiply_4220803C_Out_2, float4(0, 0, 0, 0), surface);
                return surface;
            }

            GraphVertexOutput vert(GraphVertexInput v)
            {
                v = PopulateVertexData(v);
                GraphVertexOutput o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.WorldSpaceNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                o.WorldSpaceViewDirection = _WorldSpaceCameraPos - mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
                return o;
            }

            float4 frag(GraphVertexOutput IN) : SV_Target
            {
                SurfaceDescriptionInputs surfaceInput = (SurfaceDescriptionInputs)0;
                surfaceInput.WorldSpaceNormal = IN.WorldSpaceNormal;
                surfaceInput.WorldSpaceViewDirection = IN.WorldSpaceViewDirection;

                float4 surf = PopulateSurfaceData(surfaceInput);
                return all(isfinite(surf)) ? half4(surf.x, surf.y, surf.z, 0.7f) : float4(1.0f, 0.0f, 1.0f, 1.0f);
            }

            ENDHLSL
        }        
    }
}
