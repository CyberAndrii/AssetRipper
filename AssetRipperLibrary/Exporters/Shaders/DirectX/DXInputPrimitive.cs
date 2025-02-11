﻿using System.Runtime.Versioning;

namespace AssetRipperLibrary.Exporters.Shaders.DirectX
{
	[SupportedOSPlatform("windows")]
	public enum DXInputPrimitive
	{
		None		= 0,
		Points		= 1,
		Lines		= 2,
		Triangles	= 3,
		LineAdj		= 6,
		TriangleAdj	= 7,
	}
}
