Shader "Custom/CustomiseCharacter"
{
    Properties
    {
        _RedTint("Replacement1", Color) = (1, 0, 0, 1)
        _RedTint("Red", Color) = (0, 1, 0, 1)
        _GreenTint("Green", Color) = (0, 0, 1, 1)
        _BlueTint("Blue", Color) = (0, 0, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType"="Transparent"
        }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _RedTint;
            fixed4 _GreenTint;
            fixed4 _BlueTint;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // if (col == fixed4(1, 1, 1, 1))
                // {
                //     col = fixed4(1, 1, 1, 0);
                // }
                fixed3 noOutline = length(abs(col.rbg - (1, 1, 1))) < 0.1 ? (0, 0, 0) : col.rgb;

                fixed3 redChan = noOutline.r * _RedTint;
                fixed3 greenChan = noOutline.g * _GreenTint;
                fixed3 blueChan = noOutline.b * _BlueTint;

                col.rgb = redChan + greenChan + blueChan;
                return col;
            }
            ENDCG
        }
    }
}