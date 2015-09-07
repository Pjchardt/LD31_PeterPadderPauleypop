Shader "Invert" {

    Properties {

        _OverlayTex ("Overlay (RGB)", 2D) = "white" {}

    }

    

    SubShader {

        Tags { "RenderType"="Transparent" "Queue"="Transparent+1000"}
		Blend SrcAlpha OneMinusSrcAlpha

        LOD 200

        

        GrabPass {}

        

        Pass {

            CGPROGRAM

            

            #pragma vertex vert

            #pragma fragment frag

            

            #include "UnityCG.cginc"

            

            sampler2D _GrabTexture;

            sampler2D _OverlayTex;

            

            struct appdata_t {

                float4 vertex : POSITION;

                float4 texcoord : TEXCOORD0;

            };

            

            struct v2f {
			
			#ifdef SHADER_API_PSSL
				float4 vertex : SV_POSITION;
			#else
				float4 vertex : POSITION;
			#endif
                float2 uv : TEXCOORD0;

                float4 projPos : TEXCOORD1;

            };

            

            float4 _OverlayTex_ST;

            

            v2f vert( appdata_t v ){

                v2f o;

                o.vertex = mul( UNITY_MATRIX_MVP, v.vertex );

                o.uv = TRANSFORM_TEX( v.texcoord, _OverlayTex );

                o.projPos = ComputeGrabScreenPos( o.vertex );

                return o;

            }

            
			
		#ifdef SHADER_API_PSSL
            half4 frag( v2f i ) : SV_Target
		#else
            half4 frag( v2f i ) : COLOR
		#endif
			{

                i.projPos /= i.projPos.w;

                half4 base = tex2D( _GrabTexture, float2( i.projPos.xy ));
				

                half4 finalColor;
                finalColor.r = 1 - base.r;
                finalColor.g = 1 - base.g;
                finalColor.b = 1 - base.b;
                finalColor.a = base.a;

				return finalColor;
            }

            

            ENDCG

        }

    }

}
