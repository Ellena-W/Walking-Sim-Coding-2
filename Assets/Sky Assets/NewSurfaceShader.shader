Shader "Custom/BlendedSkybox"
{
    Properties
    {
        _Skybox1 ("Skybox 1", CUBE) = "" {}
        _Skybox2 ("Skybox 2", CUBE) = "" {}
        _Blend ("Blend", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            samplerCUBE _Skybox1;
            samplerCUBE _Skybox2;
            float _Blend;
            
            struct appdata { float4 vertex : POSITION; };
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 dir : TEXCOORD0;
            };
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.dir = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col1 = texCUBE(_Skybox1, i.dir);
                fixed4 col2 = texCUBE(_Skybox2, i.dir);
                
                // Simple blend between two skyboxes
                fixed4 blendedCol = lerp(col1, col2, _Blend);
                
                return blendedCol;
            }
            ENDCG
        }
    }
    FallBack "RenderFX/Skybox"
}