using AssetRipper.Parser.Files.Entries;
using AssetRipper.Parser.Files.SerializedFiles.Parser;
using AssetRipper.Parser.Utils;
using System;
using System.Collections.Generic;

namespace AssetRipper.Parser.Files.Schemes
{
	public abstract class FileScheme : IDisposable
	{
		protected FileScheme(string filePath, string fileName)
		{
			FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
			NameOrigin = fileName;
			Name = FilenameUtils.FixFileIdentifier(fileName);
		}

		~FileScheme()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public override string ToString()
		{
			return Name == null ? base.ToString() : $"T:{SchemeType} N:'{Name}'";
		}

		protected virtual void Dispose(bool disposing) { }

		public string FilePath { get; }
		public string NameOrigin { get; }
		public string Name { get; }

		public abstract FileEntryType SchemeType { get; }
		public abstract IEnumerable<FileIdentifier> Dependencies { get; }
	}
}
