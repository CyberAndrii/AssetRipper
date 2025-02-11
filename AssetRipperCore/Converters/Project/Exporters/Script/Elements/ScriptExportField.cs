using AssetRipper.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AssetRipper.Converters.Project.Exporters.Script.Elements
{
	public abstract class ScriptExportField
	{
		public abstract void Init(IScriptExportManager manager);

		public void Export(TextWriter writer, int indent)
		{
			foreach (ScriptExportAttribute attribute in Attributes)
			{
				attribute.Export(writer, indent);
			}

			writer.WriteIndent(indent);
			writer.Write("{0} ", Keyword);
			if (IsNew)
			{
				writer.Write("new ");
			}

			string typeName = Type.GetTypeNestedName(DeclaringType);
			writer.WriteLine("{0} {1};", typeName, Name);
		}

		public void ExportEnum(TextWriter writer, int indent)
		{
			writer.WriteIndent(indent);
			writer.WriteLine("{0} = {1},", Name, Constant);
		}

		public void GetUsedNamespaces(ICollection<string> namespaces)
		{
			Type.GetTypeNamespaces(namespaces);
			foreach (ScriptExportAttribute attribute in Attributes)
			{
				attribute.GetUsedNamespaces(namespaces);
			}
		}

		public override string ToString()
		{
			if (Name == null)
			{
				return base.ToString();
			}
			else
			{
				return Name;
			}
		}

		public abstract string Name { get; }

		public abstract ScriptExportType DeclaringType { get; }
		public abstract ScriptExportType Type { get; }
		public abstract IReadOnlyList<ScriptExportAttribute> Attributes { get; }

		protected abstract string Keyword { get; }
		protected bool IsNew
		{
			get
			{
				ScriptExportType baseType = DeclaringType.Base;
				if (baseType == null)
				{
					return false;
				}
				return baseType.HasMember(Name);
			}
		}
		protected abstract string Constant { get; }

		protected const string PublicKeyWord = "public";
		protected const string InternalKeyWord = "internal";
		protected const string ProtectedKeyWord = "protected";
		protected const string PrivateKeyWord = "private";
	}
}
