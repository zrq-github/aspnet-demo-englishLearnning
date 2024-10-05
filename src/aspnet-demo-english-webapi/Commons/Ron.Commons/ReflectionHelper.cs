using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using AsmResolver;
using ModuleDefinition = AsmResolver.DotNet.ModuleDefinition;

namespace Ron.Commons;

public static class ReflectionHelper
{
    /// <summary>
    /// loop through all assemblies
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllReferencedAssemblies(bool skipSystemAssemblies = true)
    {
        var rootAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        var returnAssemblies = new HashSet<Assembly>(new AssemblyEquality());
        var loadedAssemblies = new HashSet<string>();
        var assembliesToCheck = new Queue<Assembly>();
        assembliesToCheck.Enqueue(rootAssembly);

        if (skipSystemAssemblies
            && IsSystemAssembly(rootAssembly)
            && IsValid(rootAssembly))
            returnAssemblies.Add(rootAssembly);

        while (assembliesToCheck.Any())
        {
            var assemblyToCheck = assembliesToCheck.Dequeue();
            foreach (var reference in assemblyToCheck.GetReferencedAssemblies())
                if (!loadedAssemblies.Contains(reference.FullName))
                {
                    var assembly = Assembly.Load(reference);
                    if (skipSystemAssemblies && IsSystemAssembly(assembly)) continue;

                    assembliesToCheck.Enqueue(assembly);
                    loadedAssemblies.Add(reference.FullName);
                    if (IsValid(assembly)) returnAssemblies.Add(assembly);
                }
        }

        var asmsInBaseDir = Directory.EnumerateFiles(AppContext.BaseDirectory,
            "*.dll", new EnumerationOptions { RecurseSubdirectories = true });
        foreach (var asmPath in asmsInBaseDir)
        {
            if (!IsManagedAssembly(asmPath)) continue;

            var asmName = AssemblyName.GetAssemblyName(asmPath);
            //如果程序集已经加载过了就不再加载
            if (returnAssemblies.Any(x => AssemblyName.ReferenceMatchesDefinition(x.GetName(), asmName))) continue;

            if (skipSystemAssemblies && IsSystemAssembly(asmPath)) continue;

            var asm = TryLoadAssembly(asmPath);
            if (asm == null) continue;

            //Assembly asm = Assembly.Load(asmName);
            if (!IsValid(asm)) continue;

            if (skipSystemAssemblies && IsSystemAssembly(asm)) continue;

            returnAssemblies.Add(asm);
        }

        return returnAssemblies.ToArray();
    }

    /// <summary>
    /// 是否是微软等的官方Assembly
    /// </summary>
    /// <param name="asm">程序集对象</param>
    /// <remarks>通过 AssemblyCompanyAttribute 是否含有Microsoft进行判断</remarks>
    /// <returns></returns>
    private static bool IsSystemAssembly(Assembly asm)
    {
        var asmCompanyAttr = asm.GetCustomAttribute<AssemblyCompanyAttribute>();
        if (asmCompanyAttr == null)
            return false;
        var companyName = asmCompanyAttr.Company;
        return companyName.Contains("Microsoft");
    }

    private static bool IsSystemAssembly(string asmPath)
    {
        var moduleDef = ModuleDefinition.FromFile(asmPath);
        var assembly = moduleDef.Assembly;
        if (assembly == null) return false;

        var asmCompanyAttr = assembly.CustomAttributes.FirstOrDefault(c => c.Constructor?.DeclaringType?.FullName == typeof(AssemblyCompanyAttribute).FullName);
        if (asmCompanyAttr == null) return false;

        var companyName = ((Utf8String?)asmCompanyAttr.Signature?.FixedArguments[0]?.Element)?.Value;
        if (companyName == null) return false;

        return companyName.Contains("Microsoft");
    }

    /// <summary>
    /// 判断file这个文件是否是程序集
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    private static bool IsManagedAssembly(string file)
    {
        using var fs = File.OpenRead(file);
        using var peReader = new PEReader(fs);
        return peReader.HasMetadata && peReader.GetMetadataReader().IsAssembly;
    }

    private static Assembly? TryLoadAssembly(string asmPath)
    {
        var asmName = AssemblyName.GetAssemblyName(asmPath);
        Assembly? asm = null;
        try
        {
            asm = Assembly.Load(asmName);
        }
        catch (BadImageFormatException ex)
        {
            Debug.WriteLine(ex);
        }
        catch (FileLoadException ex)
        {
            Debug.WriteLine(ex);
        }

        if (asm == null)
            try
            {
                asm = Assembly.LoadFile(asmPath);
            }
            catch (BadImageFormatException ex)
            {
                Debug.WriteLine(ex);
            }
            catch (FileLoadException ex)
            {
                Debug.WriteLine(ex);
            }

        return asm;
    }

    private static bool IsValid(Assembly asm)
    {
        try
        {
            asm.GetTypes();
            asm.DefinedTypes.ToList();
            return true;
        }
        catch (ReflectionTypeLoadException)
        {
            return false;
        }
    }

    #region Nested type: AssemblyEquality

    private class AssemblyEquality : EqualityComparer<Assembly>
    {
        public override bool Equals(Assembly? x, Assembly? y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return AssemblyName.ReferenceMatchesDefinition(x.GetName(), y.GetName());
        }

        public override int GetHashCode([DisallowNull] Assembly obj)
        {
            return obj.GetName().FullName.GetHashCode();
        }
    }

    #endregion
}
