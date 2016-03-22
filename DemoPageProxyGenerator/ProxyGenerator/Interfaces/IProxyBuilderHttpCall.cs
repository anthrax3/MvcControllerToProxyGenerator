using ProxyGenerator.Container;
using ProxyGenerator.Enums;

namespace ProxyGenerator.Interfaces
{
    public interface IProxyBuilderHttpCall
    {
        /// <summary>
        /// Den passenden HttpCall zusammenbauen und pr�fen ob Post oder Get verwendet werden soll
        /// Erstellt wird: post("/Home/LoadAll", data) oder get("/Home/LoadAll?userId=" + id)
        /// </summary>
        string BuildHttpCall(ProxyMethodInfos methodInfo, ProxyBuilder proxyBuilder);

        /// <summary>
        /// Erstellen eines einfachen HREF Links f�r window.location.href
        /// </summary>
        string BuildHrefLink(ProxyMethodInfos methodInfo, ProxyBuilder proxyBuilder);
    }
}