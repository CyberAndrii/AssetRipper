using AssetRipper.Converters.Project.Exporters.Script.Elements;
using AssetRipper.Structure.Assembly.Mono;
using Mono.Cecil;
using System;
using System.IO;

namespace AssetRipper.Converters.Project.Exporters.Script.Mono
{
	public sealed class ScriptExportMonoAttribute : ScriptExportAttribute
	{
		public ScriptExportMonoAttribute(CustomAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException(nameof(attribute));
			}

			Attribute = attribute;

			Module = MonoUtils.GetModuleName(Attribute.AttributeType);
			FullName = ToFullName(Attribute, Module);
		}

		public static string ToFullName(CustomAttribute attribute)
		{
			return MonoUtils.GetFullName(attribute.AttributeType);
		}

		public static string ToFullName(CustomAttribute attribute, string module)
		{
			return MonoUtils.GetFullName(attribute.AttributeType, module);
		}

		private static string GetModule(TypeReference type)
		{
			return Path.GetFileNameWithoutExtension(type.Scope.Name);
		}

		public override void Init(IScriptExportManager manager)
		{
			m_type = manager.RetrieveType(Attribute.AttributeType);
		}

		public static bool IsSerializableAttribute(CustomAttribute attr)
		{
			TypeReference attrType = attr.AttributeType;
			return attrType.Namespace == SystemNamespace && attrType.Name == SerializableName;
		}

		public static bool IsSerializeFieldAttribute(CustomAttribute attr)
		{
			TypeReference attrType = attr.AttributeType;
			return attrType.Namespace == UnityEngineNamespace && attrType.Name == SerializeFieldName;
		}

		public static bool IsMulitlineAttribute(CustomAttribute attr)
		{
			TypeReference attrType = attr.AttributeType;
			return attrType.Namespace == UnityEngineNamespace && attrType.Name == MultilineAttributeName;
		}

		public static bool IsTextAreaAttribute(CustomAttribute attr)
		{
			TypeReference attrType = attr.AttributeType;
			return attrType.Namespace == UnityEngineNamespace && attrType.Name == TextAreaAttributeName;
		}

		public override string FullName { get; }
		public override string Name => Attribute.AttributeType.Name;
		public override string Module { get; }

		protected override ScriptExportType Type => m_type;

		private CustomAttribute Attribute { get; }

		private ScriptExportType m_type;
	}
}
