using AssetRipper.Extensions;
using AssetRipper.IO.Endian;
using AssetRipper.Parser.Files.Schemes;
using AssetRipper.Parser.Files.Entries;
using AssetRipper.Structure;
using AssetRipper.Structure.GameStructure;
using System.IO;

namespace AssetRipper.Parser.Files.WebFiles
{
	public sealed class WebFileScheme : FileSchemeList
	{
		private WebFileScheme(string filePath) : base(filePath, string.Empty) { }

		internal static WebFileScheme ReadScheme(byte[] buffer, string filePath)
		{
			WebFileScheme scheme = new WebFileScheme(filePath);
			using (MemoryStream stream = new MemoryStream(buffer, 0, buffer.Length, false))
			{
				scheme.ReadScheme(stream);
			}
			return scheme;
		}

		internal static WebFileScheme ReadScheme(Stream stream, string filePath)
		{
			WebFileScheme scheme = new WebFileScheme(filePath);
			scheme.ReadScheme(stream);
			return scheme;
		}

		internal WebFile ReadFile(GameProcessorContext context)
		{
			WebFile web = new WebFile(this);
			foreach (FileScheme scheme in Schemes)
			{
				web.AddFile(context, scheme);
			}
			return web;
		}

		private void ReadScheme(Stream stream)
		{
			using (EndianReader reader = new EndianReader(stream, EndianType.LittleEndian))
			{
				Header.Read(reader);
				Metadata.Read(reader);
			}

			foreach (WebFileEntry entry in Metadata.Entries)
			{
				byte[] buffer = new byte[entry.Size];
				stream.Position = entry.Offset;
				stream.ReadBuffer(buffer, 0, buffer.Length);
				FileScheme scheme = GameCollection.ReadScheme(buffer, FilePath, entry.NameOrigin);
				AddScheme(scheme);
			}
		}

		public override FileEntryType SchemeType => FileEntryType.Web;

		public WebHeader Header { get; } = new WebHeader();
		public WebMetadata Metadata { get; } = new WebMetadata();
	}
}
