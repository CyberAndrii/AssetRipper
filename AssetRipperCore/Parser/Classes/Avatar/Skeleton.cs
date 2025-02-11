using AssetRipper.Converters.Project;
using AssetRipper.IO.Asset;
using AssetRipper.IO.Extensions;
using AssetRipper.YAML;
using AssetRipper.YAML.Extensions;

namespace AssetRipper.Parser.Classes.Avatar
{
	public struct Skeleton : IAssetReadable, IYAMLExportable
	{
		public void Read(AssetReader reader)
		{
			Node = reader.ReadAssetArray<Node>();
			ID = reader.ReadUInt32Array();
			AxesArray = reader.ReadAssetArray<Axes>();
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add(NodeName, Node == null ? YAMLSequenceNode.Empty : Node.ExportYAML(container));
			node.Add(IDName, ID == null ? YAMLSequenceNode.Empty : ID.ExportYAML(true));
			node.Add(AxesArrayName, AxesArray == null ? YAMLSequenceNode.Empty : AxesArray.ExportYAML(container));
			return node;
		}

		public Node[] Node { get; set; }
		public uint[] ID { get; set; }
		public Axes[] AxesArray { get; set; }

		public const string NodeName = "m_Node";
		public const string IDName = "m_ID";
		public const string AxesArrayName = "m_AxesArray";
	}
}
