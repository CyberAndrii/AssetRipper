using AssetRipper.Converters.Project;
using AssetRipper.IO.Extensions;
using AssetRipper.Parser.Asset;
using AssetRipper.Parser.Classes.Misc;
using AssetRipper.Parser.Classes.Utils.Extensions;
using AssetRipper.Parser.Files.SerializedFiles;
using AssetRipper.IO.Asset;
using AssetRipper.YAML;
using System;
using System.Collections.Generic;
using System.Linq;
using Version = AssetRipper.Parser.Files.Version;

namespace AssetRipper.Parser.Classes.EditorBuildSettings
{
	public sealed class EditorBuildSettings : Object.Object
	{
		public EditorBuildSettings(AssetInfo assetInfo) : base(assetInfo) { }

		public static EditorBuildSettings CreateVirtualInstance(VirtualSerializedFile virtualFile)
		{
			return virtualFile.CreateAsset((assetInfo) => new EditorBuildSettings(assetInfo));
		}

		public static int ToSerializedVersion(Version version)
		{
			// unknown version
			// KeyValuePairs to Scene class
			if (version.IsGreaterEqual(2, 5))
			{
				return 2;
			}
			return 1;
		}

		/// <summary>
		/// 2018.1 and greater
		/// </summary>
		public static bool HasConfigObjects(Version version) => version.IsGreaterEqual(2018);

		/// <summary>
		/// 2.5.0 and greater (NOTE: unknown version)
		/// </summary>
		private static bool HasScenes(Version version) => version.IsGreaterEqual(2, 5);

		public void Initialize(IEnumerable<Scene> scenes)
		{
			if (scenes == null)
			{
				throw new ArgumentNullException(nameof(scenes));
			}
			Scenes = scenes.ToArray();
			ConfigObjects = new Dictionary<string, PPtr<Object.Object>>();
		}

		public override void Read(AssetReader reader)
		{
			base.Read(reader);

			if (HasScenes(reader.Version))
			{
				Scenes = reader.ReadAssetArray<Scene>();
			}
			else
			{
				Tuple<bool, string>[] scenes = reader.ReadTupleBoolStringArray();
				Scenes = scenes.Select(t => new Scene(t.Item1, t.Item2)).ToArray();
			}
			if (HasConfigObjects(reader.Version))
			{
				ConfigObjects = new Dictionary<string, PPtr<Object.Object>>();
				ConfigObjects.Read(reader);
			}
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.AddSerializedVersion(ToSerializedVersion(container.ExportVersion));
			node.Add(ScenesName, Scenes.ExportYAML(container));
			if (HasConfigObjects(container.ExportVersion))
			{
				node.Add(ConfigObjectsName, GetConfigObjects(container.Version).ExportYAML(container));
			}
			return node;
		}

		private IReadOnlyDictionary<string, PPtr<Object.Object>> GetConfigObjects(Version version)
		{
			return HasConfigObjects(version) ? ConfigObjects : new Dictionary<string, PPtr<Object.Object>>(0);
		}

		public Scene[] Scenes { get; set; }
		public Dictionary<string, PPtr<Object.Object>> ConfigObjects { get; set; }

		public const string ScenesName = "m_Scenes";
		public const string ConfigObjectsName = "m_configObjects";
	}
}
