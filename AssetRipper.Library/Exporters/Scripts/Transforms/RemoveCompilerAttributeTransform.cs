﻿using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp.Transforms;
using System.Runtime.CompilerServices;
using Attribute = ICSharpCode.Decompiler.CSharp.Syntax.Attribute;

namespace AssetRipper.Library.Exporters.Scripts.Transforms
{
	/// <summary>
	/// Removes all compiler-generated attributes, like
	/// <see cref="AsyncStateMachineAttribute"/>, 
	/// <see cref="IteratorStateMachineAttribute"/>, and
	/// <see cref="IsReadOnlyAttribute"/>
	/// </summary>
	internal class RemoveCompilerAttributeTransform : DepthFirstAstVisitor, IAstTransform
	{
		private static void RemoveCompilerGeneratedAttributes(EntityDeclaration entity)
		{
			RemoveCompilerGeneratedAttributes(entity.Attributes);
		}

		private static void RemoveCompilerGeneratedAttributes(AstNodeCollection<AttributeSection> attributes)
		{
			foreach (AttributeSection attributeSection in attributes)
			{
				foreach (Attribute attribute in attributeSection.Attributes)
				{
					if (attribute.Type
					    is SimpleType { Identifier: "AsyncStateMachine" or "IteratorStateMachine" or "IsReadOnly" }
					    or MemberType { MemberName: "AsyncStateMachine" or "IteratorStateMachine" or "IsReadOnly" })
					{
						attribute.Remove();
					}
				}

				if (attributeSection.Attributes.Count == 0)
				{
					attributeSection.Remove();
				}
			}
		}


		public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
		{
			base.VisitParameterDeclaration(parameterDeclaration);
			RemoveCompilerGeneratedAttributes(parameterDeclaration.Attributes);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			base.VisitTypeDeclaration(typeDeclaration);
			RemoveCompilerGeneratedAttributes(typeDeclaration);
		}

		public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
		{
			base.VisitMethodDeclaration(methodDeclaration);
			RemoveCompilerGeneratedAttributes(methodDeclaration);
		}

		public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
		{
			base.VisitFieldDeclaration(fieldDeclaration);
			RemoveCompilerGeneratedAttributes(fieldDeclaration);
		}

		public override void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
		{
			base.VisitConstructorDeclaration(constructorDeclaration);
			RemoveCompilerGeneratedAttributes(constructorDeclaration);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			base.VisitPropertyDeclaration(propertyDeclaration);
			RemoveCompilerGeneratedAttributes(propertyDeclaration);
			RemoveCompilerGeneratedAttributes(propertyDeclaration.Getter);
			RemoveCompilerGeneratedAttributes(propertyDeclaration.Setter);
		}

		public override void VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
		{
			base.VisitEnumMemberDeclaration(enumMemberDeclaration);
			RemoveCompilerGeneratedAttributes(enumMemberDeclaration);
		}

		public void Run(AstNode rootNode, TransformContext context)
		{
			rootNode.AcceptVisitor(this);
		}
	}
}
