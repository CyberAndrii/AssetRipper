﻿using AssetRipper.Converters.Game;
using AssetRipper.Layout.Classes.Misc.Serializable;
using AssetRipper.Parser.Classes.AnimationClip.Curves;

namespace AssetRipper.Layout.Classes.AnimationClip.Curves
{
	public sealed class QuaternionCurveLayout
	{
		public QuaternionCurveLayout(LayoutInfo info) { }

		public static void GenerateTypeTree(TypeTreeContext context, string name)
		{
			QuaternionCurveLayout layout = context.Layout.AnimationClip.QuaternionCurve;
			context.AddNode(layout.Name, name);
			context.BeginChildren();
			AnimationCurveTplLayout.GenerateTypeTree(context, layout.CurveName, QuaternionfLayout.GenerateTypeTree);
			context.AddString(layout.PathName);
			context.EndChildren();
		}

		public int Version => 1;

		/// <summary>
		/// All versions
		/// </summary>
		public bool HasCurveName => true;
		/// <summary>
		/// All versions
		/// </summary>
		public bool HasPathName => true;

		public string Name => nameof(QuaternionCurve);
		public string CurveName => "curve";
		public string PathName => "path";
	}
}
