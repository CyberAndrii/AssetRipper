﻿using AssetRipper.Parser.Classes.Shader.Enums;
using AssetRipper.Parser.Files;
using System.IO;
using System.Runtime.Versioning;

namespace AssetRipperLibrary.Exporters.Shaders.DirectX
{
	[SupportedOSPlatform("windows")]
	public struct DXDataHeader
	{
		/// <summary>
		/// Not D3D9
		/// </summary>
		public static bool HasHeader(GPUPlatform graphicApi) => graphicApi != GPUPlatform.d3d9;
		/// <summary>
		/// 5.4.0 and greater
		/// </summary>
		public static bool HasGSInputPrimitive(Version version) => version.IsGreaterEqual(5, 4);

		public static int GetDataOffset(Version version, GPUPlatform graphicApi)
		{
			if (HasHeader(graphicApi))
			{
				return HasGSInputPrimitive(version) ? 6 : 5;
			}
			else
			{
				return 0;
			}
		}

		public void Read(BinaryReader reader, Version version)
		{
			Unknown1 = reader.ReadByte();
			Textures = reader.ReadByte();
			CBs = reader.ReadByte();
			Samplers = reader.ReadByte();
			UAVs = reader.ReadByte();
			if (HasGSInputPrimitive(version))
			{
				GSInputPrimitive = (DXInputPrimitive)reader.ReadByte();
			}
		}

		public void Write(BinaryWriter writer, Version version)
		{
			writer.Write(Unknown1);
			writer.Write(Textures);
			writer.Write(CBs);
			writer.Write(Samplers);
			writer.Write(UAVs);
			if (HasGSInputPrimitive(version))
			{
				writer.Write((byte)GSInputPrimitive);
			}
		}

		/// <summary>
		/// Always 1
		/// </summary>
		public byte Unknown1 { get; set; }
		public byte Textures { get; set; }
		public byte CBs { get; set; }
		public byte Samplers { get; set; }
		public byte UAVs { get; set; }
		public DXInputPrimitive GSInputPrimitive { get; set; }
	}
}
