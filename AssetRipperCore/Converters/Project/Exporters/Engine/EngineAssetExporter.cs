using AssetRipper.Parser.Asset;
using AssetRipper.Parser.Files.SerializedFiles;
using AssetRipper.Structure.Collections;
using System;
using System.Collections.Generic;
using Object = AssetRipper.Parser.Classes.Object.Object;

namespace AssetRipper.Converters.Project.Exporters.Engine
{
	public class EngineAssetExporter : IAssetExporter
	{
		public bool IsHandle(Object asset, ExportOptions options)
		{
			return EngineExportCollection.IsEngineAsset(asset, options.Version);
		}

		public IExportCollection CreateCollection(VirtualSerializedFile virtualFile, Object asset)
		{
			return new EngineExportCollection(asset, virtualFile.Version);
		}

		public bool Export(IExportContainer container, Object asset, string path)
		{
			throw new NotSupportedException();
		}

		public void Export(IExportContainer container, Object asset, string path, Action<IExportContainer, Object, string> callback)
		{
			throw new NotSupportedException();
		}

		public bool Export(IExportContainer container, IEnumerable<Object> assets, string path)
		{
			throw new NotSupportedException();
		}

		public void Export(IExportContainer container, IEnumerable<Object> assets, string path, Action<IExportContainer, Object, string> callback)
		{
			throw new NotSupportedException();
		}

		public AssetType ToExportType(Object asset)
		{
			return AssetType.Internal;
		}

		public bool ToUnknownExportType(ClassIDType classID, out AssetType assetType)
		{
			assetType = AssetType.Internal;
			return false;
		}
	}
}
