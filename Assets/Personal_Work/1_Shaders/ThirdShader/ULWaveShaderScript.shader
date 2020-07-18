// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "SWaveShader/WaveShaderScript"
{//l5dbSS9BcqY https://docs.unity3d.com/462/Documentation/Manual/SL-BuiltinValues.html
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); //rdy2render

                //===================================================================================================================================================
                //this is for the world position, now to get the world pos
                //we need to mult current view matrix (from obj space) mult by the OBJ 2 WORLD matrix
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;//.xyz because float3 and returns xyz not xyzw (float4)
                o.vertex.y += sin(worldPos.x + _Time.w);
                //begin creating a sine wave time is delta time basically and using worldpos in x dir using the y vertex for sine waves
                //notice we are using v.vertex instead of v2f's "o" its because "o" is already modified and in screen space its already rdy 2 b rendered
                //its not the obj matrix its only the screen space BUT "v" has the original values from the original matrix so we use "v" instead of "o"
                //===================================================================================================================================================
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
