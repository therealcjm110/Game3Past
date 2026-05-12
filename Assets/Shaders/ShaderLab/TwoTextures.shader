Shader "Custom/TwoTextures" {
  Properties {
    _Texture1 ("Texture 1", 2D) = "white" {}
    _Texture2 ("Texture 2", 2D) = "white" {}
  }

  SubShader {
    Tags {"Queue"="Transparent" }
    Blend SrcAlpha OneMinusSrcAlpha
    
    Pass {
      CGPROGRAM
      
      #pragma vertex vert
      #pragma fragment frag
      #include "UNITYCG.cginc"
      
      struct appdata {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
      };
      
      struct v2f {
        float4 position: SV_POSITION;
        float2 uv : TEXCOORD0;
        float2 uv2 : TEXCOORD1;
      };
      
      sampler2D _Texture1;
      sampler2D _Texture2;
      float4 _Texture2_ST;
      
      v2f vert(appdata i) {
        v2f o;
        o.position = UnityObjectToClipPos(i.vertex);
        o.uv = i.uv;
        o.uv2 = TRANSFORM_TEX(i.uv, _Texture2);
        return o;
      }
      
      fixed4 frag(v2f i) : COLOR {
        fixed4 texel1 = tex2D(_Texture1, i.uv);
        fixed4 texel2 = tex2D(_Texture2, i.uv2);
        fixed4 texel = texel1 * texel2;

        return texel;
      }
      
      ENDCG
    }
  }
}