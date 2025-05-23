Shader "Custom/CurvedShaderMobile"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _CurveStrength("Curvature Strength", Float) = 0.001
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float4 color : COLOR;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 color : TEXCOORD2;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _CurveStrength;

                v2f vert(appdata v)
                {
                    v2f o;

                    // Convert to clip space
                    o.vertex = UnityObjectToClipPos(v.vertex);

                    // Apply curvature based on distance
                    float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);
                    o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;

                    // Texture UVs
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                    // Pass vertex color
                    o.color = v.color;

                    // Apply fog coordinates
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 texColor = tex2D(_MainTex, i.uv);
                    fixed4 finalColor = texColor * i.color;

                    UNITY_APPLY_FOG(i.fogCoord, finalColor);
                    return finalColor;
                }
                ENDCG
            }
        }
}