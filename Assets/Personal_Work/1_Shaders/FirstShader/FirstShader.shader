Shader "Unlit/FirstShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //basic properties & variables creation
        /*
        _SecondTexName("2ND TEXTURE", 2D) = "black" {}
        _ColorTint("Tint", Color) = (1,0.5,1,1)
        _Vector3Position("Char Pos", Vector) = (0,0,0,0)
        */
    }
    SubShader
    {
        //Render Queue = https://docs.unity3d.com/Manual/SL-ShaderReplacement.html
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            /* REMOVING FOG FOR TESTING
            // make fog work
            #pragma multi_compile_fog
            */

            #include "UnityCG.cginc"

            // Writing Shaders: https://docs.unity3d.com/Manual/ShadersOverview.html
            // Shader Code: https://wiki.unity3d.com/index.php/Shader_Code
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f //VERTEX TO FRAGMENT
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1) REMOVING FOG FOR TESTING
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            //float4 _Tint;


            //Vertex for all the verticies on the specific object & will be called 8 times for a cube (corners).
            //for example if a model had 2 million verticies it would be called that amount each frame
            //it isnt expensive because it runs on the GPU & GPU's are specialized specifically for this.
            //position the vertex in the object? this can also be used to deform the mesh.
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);



                //UNITY_TRANSFER_FOG(o,o.vertex); REMOVING FOG FOR TESTING
                return o;
            }

            //Pixel shader, this will render each pixel on the screen
            //example it renders a 1920x1080 screen in 2,073,600‬ pixels each frame (just multiply)
            //it isnt expensive because it runs on the GPU & GPU's are specialized specifically for this.
            //this will light the pixel etc?
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                

                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col); REMOVING FOG FOR TESTING
                return col;
            }
            ENDCG
        }
    }
}
