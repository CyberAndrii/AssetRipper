﻿using AssetRipper.Extensions;
using System.Text;

namespace AssetRipper.YAML.Extensions
{
	public static class ArrayYAMLExtensions
	{
		public static YAMLNode ExportYAML(this byte[] _this)
		{
			StringBuilder sb = new StringBuilder(_this.Length * 2);
			for (int i = 0; i < _this.Length; i++)
			{
				sb.AppendHex(_this[i]);
			}
			return new YAMLScalarNode(sb.ToString(), true);
		}
	}
}
