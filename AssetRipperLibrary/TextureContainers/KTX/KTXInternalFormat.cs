namespace AssetRipperLibrary.TextureContainers.KTX
{
	public enum KTXInternalFormat : uint
	{
		ETC1_RGB8_OES							= 0x8D64,

		COMPRESSED_RGB_PVRTC_4BPPV1_IMG			= 0x8C00,
		COMPRESSED_RGB_PVRTC_2BPPV1_IMG			= 0x8C01,
		COMPRESSED_RGBA_PVRTC_4BPPV1_IMG		= 0x8C02,
		COMPRESSED_RGBA_PVRTC_2BPPV1_IMG		= 0x8C03,

		ATC_RGB_AMD								= 0x8C92,
		ATC_RGBA_EXPLICIT_ALPHA_AMD				= 0x8C93,
		ATC_RGBA_INTERPOLATED_ALPHA_AMD			= 0x87EE,

		COMPRESSED_RGB8_ETC2					= 0x9274,
		COMPRESSED_SRGB8_ETC2					= 0x9275,
		COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9276,
		COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9277,
		COMPRESSED_RGBA8_ETC2_EAC				= 0x9278,
		COMPRESSED_SRGB8_ALPHA8_ETC2_EAC		= 0x9279,
		COMPRESSED_R11_EAC						= 0x9270,
		COMPRESSED_SIGNED_R11_EAC				= 0x9271,
		COMPRESSED_RG11_EAC						= 0x9272,
		COMPRESSED_SIGNED_RG11_EAC				= 0x9273,

		COMPRESSED_RED_RGTC1					= 0x8DBB,
		COMPRESSED_RG_RGTC2						= 0x8DBD,
		COMPRESSED_RGB_BPTC_UNSIGNED_FLOAT		= 0x8E8F,
		COMPRESSED_RGBA_BPTC_UNORM				= 0x8E8C,

		R16F									= 0x822D,
		RG16F									= 0x822F,
		RGBA16F									= 0x881A,
		R32F									= 0x822E,
		RG32F									= 0x8230,
		RGBA32F									= 0x8814,
	}
}
