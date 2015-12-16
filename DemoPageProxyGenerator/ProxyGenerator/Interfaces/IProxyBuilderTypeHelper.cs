using System;
using System.Reflection;

namespace ProxyGenerator.Interfaces
{
    public interface IProxyBuilderTypeHelper
    {
        /// <summary>
        /// F�r den �bergebenen ParameterInfoWert den passenden "TypeScript" Typen ermitteln.
        /// </summary>
        string GetTsType(ParameterInfo info);

        /// <summary>
        /// Gibt den passenden TypeScript Datentyp zum �bergebenen .NET Type Objekt zur�ck.
        /// </summary>
        /// <param name="type">.NET Type Objekt f�r das der TypeScript Typestring ermittelt werden soll.</param>
        string GetTsType(Type type);

        /// <summary>
        /// Anpassen eines Namespaces inkl. Type in Namespace und Interface z.B.:
        /// Aus: MyWebapp.Type.Address => MyWebapp.Type.IAddress
        /// </summary>
        string AddInterfacePrefixToFullName(string fullNameWithNamespace, bool isEnum);

        /// <summary>
        /// Sucht einfach nur die Parameternamen der aktuell �bergebenen Methode heraus und setzt noch den passenden Typ 
        /// f�r TypeScript hinter den Namen z.B.: "alter: number, name: string, ..."
        /// </summary>
        string GetFunctionParametersWithType(MethodInfo methodInfo);

        /// <summary>
        /// Gibt zur�ck der �bergebenen Typ einen ReturnType hat.
        /// </summary>
        bool HasReturnType(Type type);
    }
}