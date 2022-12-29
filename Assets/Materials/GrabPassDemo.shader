Shader "Unlit/GrapPassDemo"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

		GrabPass { "_BgTexture" }

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
				float4 screenUV : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			//sampler2D _MainTex;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = ComputeGrabScreenPos(o.vertex);
				return o;
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;

			sampler2D __BgTexture;

			float4 frag(v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2Dproj(_BackgroundTexture, i.uv);
				//return fixed4(0.0, 0.0, 1.0, 1.0);
				return float4(1.0,0.0,0.0,1.0);
			//return col;
			}

			ENDCG
		}
	}
		Fallback "VertexLit"
}
