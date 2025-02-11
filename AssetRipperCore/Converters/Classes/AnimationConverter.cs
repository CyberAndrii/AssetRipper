﻿using AssetRipper.Converters.Project;
using AssetRipper.Layout.Classes;
using AssetRipper.Parser.Classes.Animation;
using AssetRipper.Parser.Classes.Misc;
using System.Linq;

namespace AssetRipper.Converters.Classes
{
	public static class AnimationConverter
	{
		public static Animation Convert(IExportContainer container, Animation origin)
		{
			AnimationLayout layout = container.Layout.Animation;
			AnimationLayout exlayout = container.ExportLayout.Animation;
			Animation instance = new Animation(container.ExportLayout);
			BehaviourConverter.Convert(container, origin, instance);
			if (exlayout.HasAnimations)
			{
				instance.Animations = GetAnimations(container, origin);
			}
			else
			{
				instance.AnimationsPaired = origin.AnimationsPaired.ToArray();
			}
			instance.WrapMode = origin.WrapMode;
			instance.PlayAutomatically = origin.PlayAutomatically;
			instance.AnimatePhysics = origin.AnimatePhysics;
			if (exlayout.HasAnimateOnlyIfVisible && layout.HasAnimateOnlyIfVisible)
			{
				instance.AnimateOnlyIfVisible = origin.AnimateOnlyIfVisible;
			}
			if (exlayout.HasCullingType && layout.HasCullingTypeInvariant)
			{
				instance.CullingType = origin.CullingType;
			}
			if (layout.HasUserAABB && exlayout.HasUserAABB)
			{
				instance.UserAABB = origin.UserAABB;
			}
			return instance;
		}

		private static PPtr<Parser.Classes.AnimationClip.AnimationClip>[] GetAnimations(IExportContainer container, Animation origin)
		{
			if (container.Layout.Animation.HasAnimations)
			{
				return origin.Animations.ToArray();
			}
			else
			{
				PPtr<Parser.Classes.AnimationClip.AnimationClip>[] animations = new PPtr<Parser.Classes.AnimationClip.AnimationClip>[origin.AnimationsPaired.Length];
				for (int i = 0; i < origin.AnimationsPaired.Length; i++)
				{
					animations[i] = origin.AnimationsPaired[i].Item2;
				}
				return animations;
			}
		}
	}
}
