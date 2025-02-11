﻿using AssetRipper.Structure.Assembly.Managers;
using AssetRipper.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace AssetRipper.Structure.GameStructure.Platforms
{
	internal sealed class MacGameStructure : PlatformGameStructure
	{
		public MacGameStructure(string rootPath)
		{
			if (string.IsNullOrEmpty(rootPath))
			{
				throw new ArgumentNullException(nameof(rootPath));
			}
			m_root = new DirectoryInfo(DirectoryUtils.ToLongPath(rootPath));
			if (!m_root.Exists)
			{
				throw new Exception($"Directory '{rootPath}' doesn't exist");
			}

			string dataPath = Path.Combine(m_root.FullName, ContentsName, DataFolderName);
			if (!Directory.Exists(dataPath))
			{
				throw new Exception("Data directory wasn't found");
			}
			string resourcePath = Path.Combine(m_root.FullName, ContentsName, ResourcesName);
			if (!Directory.Exists(resourcePath))
			{
				throw new Exception("Resources directory wasn't found");
			}
			DataPaths = new string[] { dataPath, resourcePath };


			Name = m_root.Name.Substring(0, m_root.Name.Length - AppExtension.Length);
			RootPath = rootPath;
			GameDataPath = dataPath;
			ManagedPath = Path.Combine(GameDataPath, ManagedName);
			UnityPlayerPath = Path.Combine(RootPath, DefaultUnityPlayerName);
			UnityVersion = null;
			Il2CppGameAssemblyPath = Path.Combine(RootPath, DefaultGameAssemblyName);
			Il2CppMetaDataPath = Path.Combine(GameDataPath, "il2cpp_data", MetadataName, DefaultGlobalMetadataName);

			if (HasIl2CppFiles())
				Backend = Assembly.ScriptingBackend.Il2Cpp;
			else if (HasMonoAssemblies(ManagedPath))
				Backend = Assembly.ScriptingBackend.Mono;
			else
				Backend = Assembly.ScriptingBackend.Unknown;


			DirectoryInfo dataDirectory = new DirectoryInfo(DirectoryUtils.ToLongPath(dataPath));

			CollectGameFiles(dataDirectory, Files);
			CollectStreamingAssets(dataDirectory, Files);

			CollectMainAssemblies(dataDirectory, Assemblies);
		}

		public static bool IsMacStructure(string path)
		{
			DirectoryInfo dinfo = new DirectoryInfo(DirectoryUtils.ToLongPath(path));
			if (!dinfo.Exists)
			{
				return false;
			}
			if (!dinfo.Name.EndsWith(AppExtension, StringComparison.Ordinal))
			{
				return false;
			}

			string dataPath = Path.Combine(dinfo.FullName, ContentsName, DataFolderName);
			if (!Directory.Exists(dataPath))
			{
				return false;
			}
			string resourcePath = Path.Combine(dinfo.FullName, ContentsName, ResourcesName);
			if (!Directory.Exists(resourcePath))
			{
				return false;
			}
			return true;
		}

		public override PlatformType Platform => PlatformType.Mac;

		private const string ContentsName = "Contents";
		private const string AppExtension = ".app";

		private readonly DirectoryInfo m_root;
	}
}
