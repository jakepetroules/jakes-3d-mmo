// --- XNA matrix parameters ---
float4x4 View;
float4x4 Projection;
float4x4 World;

// --- XNA lighting parameters ---
float3 LightDirection;
float Ambient;
bool EnableLighting;

// --- XNA other parameters ---
float Transparency;

// --- XNA textures ---
texture Texture;

// --- Samplers ---
sampler TextureSampler = sampler_state
{
	Texture = <Texture>;
	MagFilter = LINEAR;
	MinFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = WRAP;
	AddressV = WRAP;
};

// --- Technique: TexturedFluid ---
struct TVertexToPixel
{
	float4 Position : POSITION;
	float4 Color : COLOR0;
	float LightingFactor : TEXCOORD0;
	float2 TextureCoords : TEXCOORD1;
};

struct TPixelToFrame
{
	float4 Color : COLOR0;
};

TVertexToPixel TexturedVS(float4 inPos : POSITION, float3 inNormal : NORMAL, float2 inTexCoords : TEXCOORD0)
{
	TVertexToPixel output = (TVertexToPixel)0;
	float4x4 preViewProjection = mul(View, Projection);
	float4x4 preWorldViewProjection = mul(World, preViewProjection);
	
	output.Position = mul(inPos, preWorldViewProjection);
	output.TextureCoords = inTexCoords;
	output.LightingFactor = 1;
	
	if (EnableLighting)
	{
		float3 normal = normalize(mul(normalize(inNormal), World));
		output.LightingFactor = saturate(saturate(dot(normal, LightDirection)) + Ambient);
	}
	
	return output;
}

TPixelToFrame TexturedPS(TVertexToPixel psIn)
{
	TPixelToFrame output = (TPixelToFrame)0;
	
	output.Color = tex2D(TextureSampler, psIn.TextureCoords);
	output.Color *= psIn.LightingFactor;
	output.Color.a = Transparency;
	
	return output;
}

technique TexturedFluid
{
	pass Pass0
	{
		VertexShader = compile vs_3_0 TexturedVS();
		PixelShader = compile ps_3_0 TexturedPS();
	}
}
