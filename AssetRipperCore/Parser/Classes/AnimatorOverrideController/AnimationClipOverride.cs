﻿using AssetRipper.Converters.Project;
using AssetRipper.Parser.Asset;
using AssetRipper.Parser.Classes.Misc;
using AssetRipper.IO.Asset;
using AssetRipper.YAML;
using System.Collections.Generic;

namespace AssetRipper.Parser.Classes.AnimatorOverrideController
{
	public struct AnimationClipOverride : IAssetReadable, IYAMLExportable, IDependent
	{
		public void Read(AssetReader reader)
		{
			OriginalClip.Read(reader);
			OverrideClip.Read(reader);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(OriginalClipName, OriginalClip.ExportYAML(container));
			node.Add(OverrideClipName, OverrideClip.ExportYAML(container));
			return node;
		}

		public IEnumerable<PPtr<Object.Object>> FetchDependencies(DependencyContext context)
		{
			yield return context.FetchDependency(OriginalClip, OriginalClipName);
			yield return context.FetchDependency(OverrideClip, OverrideClipName);
		}

		public const string OriginalClipName = "m_OriginalClip";
		public const string OverrideClipName = "m_OverrideClip";

		public PPtr<AnimationClip.AnimationClip> OriginalClip;
		public PPtr<AnimationClip.AnimationClip> OverrideClip;
	}
}
