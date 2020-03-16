﻿namespace CodeAnalyzer.Metrics
{
    using System.Collections.Generic;
    using System.Linq;

    using NDepend.CodeModel;
    using NDepend.Helpers;

    public static class ClassificationExtensions
    {
        public static IEnumerable<IMember> ProtectedMembers(this IType type)
        {
            return type.BaseClass.Members.Where(m => m.IsProtected).ToHashSetEx();
        }

        public static IEnumerable<IMember> ProtectedMembersUsed(this IType type)
        {
            return type.ProtectedMembers().UsedBy(type);
        }

        public static IEnumerable<IMethod> OverridingMethods(this IType type)
        {
            return type.Methods.Where(
                m => !m.IsClassConstructor && !m.IsConstructor && !m.IsStatic
                     && m.OverriddensBase.ParentTypes().Contains(type.BaseClass));
        }

        public static IEnumerable<IMethod> PublicMethods(this IType type)
        {
            return type.Methods.Where(m => m.IsPublicNonStatic());
        }

        public static IEnumerable<IMethod> NewlyAddedMethods(this IType type)
        {
            return type.Methods.Where(m => m.IsPublicNonStatic() && !m.OverriddensBase.Any());
        }

        private static bool IsPublicNonStatic(this IMethod method)
        {
            return method.IsPublic && !method.IsClassConstructor && !method.IsConstructor && !method.IsStatic;
        }
    }
}