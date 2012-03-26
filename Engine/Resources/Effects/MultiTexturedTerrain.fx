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
int PatchIdX;
int PatchIdY;
int PatchIdZ;
int PatchSize;

// --- XNA cursor parameters ---
bool ShowCursor;
float2 CursorPosition;
int CursorSize;

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

// --- Technique: MultiTexturedTerrain ---
struct MTVertexToPixel
{
	float4 Position : POSITION;
    float4 Color : COLOR0;
    float3 Normal : TEXCOORD0;
    float3 TextureCoords : TEXCOORD1;
    float4 LightDirection : TEXCOORD2;
	float Depth : TEXCOORD3;
	float4 PositionCopy : TEXCOORD4;
};

struct MTPixelToFrame
{
    float4 Color : COLOR0;
};

MTVertexToPixel MultiTexturedTerrainVS(float4 inPos : POSITION, float3 inNormal : NORMAL, float3 inTexCoords : TEXCOORD0)
{
    MTVertexToPixel output = (MTVertexToPixel)0;
	float4x4 preViewProjection = mul(View, Projection);
	float4x4 preWorldViewProjection = mul(World, preViewProjection);
    
    output.Position = mul(inPos, preWorldViewProjection);
    output.Normal = mul(normalize(inNormal), World);
    output.TextureCoords = inTexCoords;
    output.LightDirection.xyz = -LightDirection;
    output.LightDirection.w = 1;
	output.Depth = output.Position.z / output.Position.w;
	output.PositionCopy.x = (PatchIdX * PatchSize) + inPos.x;
	output.PositionCopy.y = (PatchIdY * PatchSize) + inPos.y;
	output.PositionCopy.z = PatchIdZ * 4;
    
    return output;
}

MTPixelToFrame MultiTexturedTerrainPS(MTVertexToPixel psIn)
{
    MTPixelToFrame output = (MTPixelToFrame)0;        
    
	float lightingFactor = 1;
	
	if (EnableLighting)
	{
		lightingFactor = saturate(saturate(dot(psIn.Normal, psIn.LightDirection)) + Ambient);
	}
	
	float blendDistance = 0.99f;
	float blendWidth = 0.005f;
	float blendFactor = clamp((psIn.Depth - blendDistance) / blendWidth, 0, 1);
	
	float4 farColor = 0;
	float4 nearColor = 0;
	float2 nearTextureCoords = psIn.TextureCoords * 3;
	
	farColor += tex2D(TextureSampler, psIn.TextureCoords);
	nearColor += tex2D(TextureSampler, nearTextureCoords);
    
	output.Color = lerp(nearColor, farColor, blendFactor) * lightingFactor;
	
	if (ShowCursor)
	{
		float dist = distance(psIn.PositionCopy, CursorPosition);
		if (dist <= CursorSize / 2.0f)
		{
			output.Color.rg += 0.25f;
		}
	}
	
	output.Color.a = Transparency;
    
    return output;
}

float distance(float4 world, float2 cursor)
{
	return sqrt(pow(abs(world.x - cursor.x), 2) + pow(abs(world.y - cursor.y), 2));
}

technique MultiTexturedTerrain
{
    pass Pass0
    {
        VertexShader = compile vs_3_0 MultiTexturedTerrainVS();
        PixelShader = compile ps_3_0 MultiTexturedTerrainPS();
    }
}
