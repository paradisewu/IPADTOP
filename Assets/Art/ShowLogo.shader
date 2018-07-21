// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|custl-1852-RGB,alpha-5328-OUT;n:type:ShaderForge.SFN_Tex2d,id:1852,x:32590,y:32834,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:80224173c595f6c4997ff2f449806fac,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5328,x:32872,y:33077,varname:node_5328,prsc:2|A-1852-A,B-7493-OUT;n:type:ShaderForge.SFN_TexCoord,id:4639,x:31902,y:32998,varname:node_4639,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Sin,id:8010,x:32102,y:33063,varname:node_8010,prsc:2|IN-4639-V;n:type:ShaderForge.SFN_Slider,id:9970,x:31870,y:33308,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_9970,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.4,cur:6,max:6;n:type:ShaderForge.SFN_Exp,id:3099,x:32243,y:33299,varname:node_3099,prsc:2,et:0|IN-9970-OUT;n:type:ShaderForge.SFN_Power,id:7955,x:32292,y:33150,varname:node_7955,prsc:2|VAL-1964-OUT,EXP-3099-OUT;n:type:ShaderForge.SFN_If,id:7493,x:32723,y:33256,varname:node_7493,prsc:2|A-7955-OUT,B-8018-OUT,GT-3357-OUT,EQ-3357-OUT,LT-1724-OUT;n:type:ShaderForge.SFN_Vector1,id:8018,x:32477,y:33256,varname:node_8018,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Vector1,id:3357,x:32491,y:33318,varname:node_3357,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:1724,x:32491,y:33389,varname:node_1724,prsc:2,v1:1;n:type:ShaderForge.SFN_RemapRange,id:1964,x:32276,y:32922,varname:node_1964,prsc:2,frmn:1,frmx:0,tomn:0,tomx:1|IN-8010-OUT;proporder:1852-9970;pass:END;sub:END;*/

Shader "Shader Forge/ShowLogo" {
	Properties{
		_MainTex("MainTex", 2D) = "white" {}
		_Offset("Offset", Range(0.4, 6)) = 6
		[HideInInspector]_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
	}
		SubShader{
			Tags {
				"IgnoreProjector" = "True"
				"Queue" = "Transparent"
				"RenderType" = "Transparent"
			}
			Pass {
				Name "FORWARD"
				Tags {
					"LightMode" = "ForwardBase"
				}
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#define UNITY_PASS_FORWARDBASE
				#include "UnityCG.cginc"
				#pragma multi_compile_fwdbase
				#pragma multi_compile_fog
				#pragma only_renderers d3d9 d3d11 glcore gles 
				#pragma target 3.0
				uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
				uniform float _Offset;
				struct VertexInput {
					float4 vertex : POSITION;
					float2 texcoord0 : TEXCOORD0;
				};
				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv0 : TEXCOORD0;
					UNITY_FOG_COORDS(1)
				};
				VertexOutput vert(VertexInput v) {
					VertexOutput o = (VertexOutput)0;
					o.uv0 = v.texcoord0;
					o.pos = UnityObjectToClipPos(v.vertex);
					UNITY_TRANSFER_FOG(o,o.pos);
					return o;
				}
				float4 frag(VertexOutput i) : COLOR {
					////// Lighting:
									float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
									float3 finalColor = _MainTex_var.rgb;
									float node_7493_if_leA = step(pow((sin(i.uv0.g)*-1.0 + 1.0),exp(_Offset)),0.01);
									float node_7493_if_leB = step(0.01,pow((sin(i.uv0.g)*-1.0 + 1.0),exp(_Offset)));
									float node_3357 = 0.0;
									fixed4 finalRGBA = fixed4(finalColor,(_MainTex_var.a*lerp((node_7493_if_leA*1.0) + (node_7493_if_leB*node_3357),node_3357,node_7493_if_leA*node_7493_if_leB)));
									UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
									return finalRGBA;
								}
								ENDCG
							}
		}
			FallBack "Diffuse"
									CustomEditor "ShaderForgeMaterialInspector"
}
