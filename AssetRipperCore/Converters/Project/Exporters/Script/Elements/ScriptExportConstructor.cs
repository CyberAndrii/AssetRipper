using AssetRipper.Extensions;
using System.Collections.Generic;
using System.IO;

namespace AssetRipper.Converters.Project.Exporters.Script.Elements
{
	public abstract class ScriptExportConstructor : ScriptExportMethod
	{
		public override void Export(TextWriter writer, int indent)
		{
			writer.WriteIndent(indent);
			writer.Write("{0} {1}(", Keyword, DeclaringType.CleanName);
			for (int i = 0; i < Parameters.Count; i++)
			{
				ScriptExportParameter parameter = Parameters[i];
				parameter.Export(writer, indent);
				if (i < Parameters.Count - 1)
				{
					writer.Write(", ");
				}
			}

			if (Base != null && Base.Parameters.Count != 0)
			{
				writer.Write(") : base(");
				for (int i = 0; i < Base.Parameters.Count; i++)
				{
					ScriptExportParameter parameter = Base.Parameters[i];
					writer.Write("default({0})", parameter.Type.GetTypeNestedName(DeclaringType));
					if (i < Base.Parameters.Count - 1)
					{
						writer.Write(", ");
					}
				}
			}
			else if (DeclaringType.IsStruct)
			{
				// all field of a value type must be initialized
				writer.Write(") : this(");
			}

			writer.WriteLine(")");
			writer.WriteIndent(indent);
			writer.WriteLine("{");
			writer.WriteIndent(indent);
			writer.WriteLine("}");
		}

		public override void GetUsedNamespaces(ICollection<string> namespaces)
		{
			base.GetUsedNamespaces(namespaces);
			if (Base != null)
			{
				foreach (ScriptExportParameter parameter in Base.Parameters)
				{
					parameter.GetUsedNamespaces(namespaces);
				}
			}
		}

		public ScriptExportMethod Base => DeclaringType.Base?.Constructor;
	}
}
