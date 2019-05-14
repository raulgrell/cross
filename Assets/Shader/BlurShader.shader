Shader "Custom/CameraShader"
{
	Properties
	{
		_MainTex("Source", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
		_GreyScale("Grey Scale", Int) = 0
		_Blured("Blured", Int) = 0
		_BlurAmount("BlurAmount", Range(0,0.005)) = 0.0001
	}
		SubShader
		{
			Cull Off
			Blend SrcAlpha OneMinusSrcAlpha
			Fog { Color(0,0,0,0) }
			Lighting Off
			ZWrite Off
			ZTest Always

			BindChannels
			{
				Bind "Vertex", vertex
				Bind "texcoord", texcoord
				Bind "Color", color
			}

			Pass
			{
			CGPROGRAM
			#pragma vertex MyVertexProgram	
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			int _GreyScale;
			int _Blured;
			float _BlurAmount;


			struct VertexData
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct VertexToFragment
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};
			VertexToFragment MyVertexProgram(VertexData vert)
			{
				VertexToFragment v2f;
				v2f.position = UnityObjectToClipPos(vert.position);
				v2f.uv = vert.uv;
				return v2f;
			}
			float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET{

				float4 color = tex2D(_MainTex, v2f.uv);
				float4 tempColor = (0,0,0,0);

				if (_Blured != 0) {
					half4 texcol = half4(0.0, 0.0, 0.0, 0.0);
					float remaining = 1.0f;
					float coef = 1.0;
					float fI = 0;
					for (int j = 0; j < 3; j++) {
						fI++;
						coef *= 0.32;
						texcol += tex2D(_MainTex, float2(v2f.uv.x, v2f.uv.y - fI * _BlurAmount)) * coef;
						texcol += tex2D(_MainTex, float2(v2f.uv.x - fI * _BlurAmount, v2f.uv.y)) * coef;
						texcol += tex2D(_MainTex, float2(v2f.uv.x + fI * _BlurAmount, v2f.uv.y)) * coef;
						texcol += tex2D(_MainTex, float2(v2f.uv.x, v2f.uv.y + fI * _BlurAmount)) * coef;

						remaining -= 4 * coef;
					}
					texcol += tex2D(_MainTex, float2(v2f.uv.x, v2f.uv.y)) * remaining;
					tempColor += texcol;
				}

				if (_GreyScale != 0) {
					tempColor += color.r;
				}

				tempColor += color * _Color;


				return tempColor;
			}
			ENDCG
			}
		}
}
