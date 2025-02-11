using AssetRipper.Converters.Classes;
using AssetRipper.Converters.Project;
using AssetRipper.Layout;
using AssetRipper.Layout.Classes;
using AssetRipper.Parser.Asset;
using AssetRipper.Parser.Classes.AnimationClip;
using AssetRipper.Parser.Classes.Misc;
using AssetRipper.Parser.Classes.Misc.Serializable;
using AssetRipper.Parser.Classes.Utils.Extensions;
using AssetRipper.IO.Asset;
using AssetRipper.IO.Extensions;
using AssetRipper.YAML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssetRipper.Parser.Classes.Animation
{
	public sealed class Animation : Behaviour
	{
		public Animation(AssetLayout layout) : base(layout)
		{
			AnimationLayout classLayout = layout.Animation;
			if (classLayout.HasAnimations)
			{
				Animations = Array.Empty<PPtr<AnimationClip.AnimationClip>>();
			}
			else
			{
				AnimationsPaired = Array.Empty<Tuple<string, PPtr<AnimationClip.AnimationClip>>>();
			}
			PlayAutomatically = true;
		}

		public Animation(AssetInfo assetInfo) : base(assetInfo) { }

		public override Object.Object Convert(IExportContainer container)
		{
			return AnimationConverter.Convert(container, this);
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			AnimationLayout layout = reader.Layout.Animation;
			DefaultAnimation.Read(reader);
			if (layout.HasAnimations)
			{
				Animations = reader.ReadAssetArray<PPtr<AnimationClip.AnimationClip>>();
			}
			else
			{
				AnimationsPaired = reader.ReadTupleStringTArray<PPtr<AnimationClip.AnimationClip>>();
			}

			WrapMode = (WrapMode)reader.ReadInt32();
			PlayAutomatically = reader.ReadBoolean();
			AnimatePhysics = reader.ReadBoolean();
			if (layout.HasAnimateOnlyIfVisible)
			{
				AnimateOnlyIfVisible = reader.ReadBoolean();
			}
			if (layout.IsAlign)
			{
				reader.AlignStream();
			}

			if (layout.HasCullingType)
			{
				CullingType = (AnimationCullingType)reader.ReadInt32();
			}
			if (layout.HasUserAABB)
			{
				UserAABB.Read(reader);
			}
		}

		public override void Write(AssetWriter writer)
		{
			base.Write(writer);

			AnimationLayout layout = writer.Layout.Animation;
			DefaultAnimation.Write(writer);
			if (layout.HasAnimations)
			{
				Animations.Write(writer);
			}
			else
			{
				AnimationsPaired.Write(writer);
			}

			writer.Write((int)WrapMode);
			writer.Write(PlayAutomatically);
			writer.Write(AnimatePhysics);
			if (layout.HasAnimateOnlyIfVisible)
			{
				writer.Write(AnimateOnlyIfVisible);
			}
			if (layout.IsAlign)
			{
				writer.AlignStream();
			}

			if (layout.HasCullingType)
			{
				writer.Write((int)CullingType);
			}
			if (layout.HasUserAABB)
			{
				UserAABB.Write(writer);
			}
		}

		public override IEnumerable<PPtr<Object.Object>> FetchDependencies(DependencyContext context)
		{
			foreach (PPtr<Object.Object> asset in base.FetchDependencies(context))
			{
				yield return asset;
			}

			AnimationLayout layout = context.Layout.Animation;
			yield return context.FetchDependency(DefaultAnimation, layout.AnimationName);

			if (layout.HasAnimationsPaired)
			{
				foreach (PPtr<Object.Object> asset in context.FetchDependencies(AnimationsPaired.Select(t => t.Item2), layout.AnimationsName))
				{
					yield return asset;
				}
			}
			else
			{
				foreach (PPtr<Object.Object> asset in context.FetchDependencies(Animations, layout.AnimationsName))
				{
					yield return asset;
				}
			}
		}

		public bool IsContainsAnimationClip(AnimationClip.AnimationClip clip)
		{
			foreach (PPtr<AnimationClip.AnimationClip> clipPtr in Animations)
			{
				if (clipPtr.IsAsset(File, clip))
				{
					return true;
				}
			}
			return false;
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			AnimationLayout layout = container.ExportLayout.Animation;
			node.AddSerializedVersion(layout.Version);
			node.Add(layout.AnimationName, DefaultAnimation.ExportYAML(container));
			if (layout.HasAnimations)
			{
				node.Add(layout.AnimationsName, Animations.ExportYAML(container));
			}
			else
			{
				node.Add(layout.AnimationsName, AnimationsPaired.ExportYAML(container));
			}

			node.Add(layout.WrapModeName, (int)WrapMode);
			node.Add(layout.PlayAutomaticallyName, PlayAutomatically);
			node.Add(layout.AnimatePhysicsInvariantName, AnimatePhysics);
			if (layout.HasAnimateOnlyIfVisible)
			{
				node.Add(layout.CullingTypeName, AnimateOnlyIfVisible);
			}
			if (layout.HasCullingType)
			{
				node.Add(layout.CullingTypeName, (int)CullingType);
			}
			if (layout.HasUserAABB)
			{
				node.Add(layout.UserAABBName, UserAABB.ExportYAML(container));
			}
			return node;
		}

		public PPtr<AnimationClip.AnimationClip>[] Animations { get; set; }
		public Tuple<string, PPtr<AnimationClip.AnimationClip>>[] AnimationsPaired { get; set; }
		public WrapMode WrapMode { get; set; }
		public bool PlayAutomatically { get; set; }
		public bool AnimatePhysics { get; set; }
		public bool PlayFixedFrameRate
		{
			get => AnimatePhysics;
			set => AnimatePhysics = value;
		}
		public bool AnimateOnlyIfVisible
		{
			get => CullingType != AnimationCullingType.AlwaysAnimate;
			set => CullingType = value ? AnimationCullingType.BasedOnRenderers : AnimationCullingType.AlwaysAnimate;
		}
		public AnimationCullingType CullingType { get; set; }

		public PPtr<AnimationClip.AnimationClip> DefaultAnimation;
		public AABB UserAABB;
	}
}
