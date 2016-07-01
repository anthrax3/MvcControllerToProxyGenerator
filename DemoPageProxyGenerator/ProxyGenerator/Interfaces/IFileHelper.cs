namespace ProxyGenerator.Interfaces
{
    public interface IFileHelper
    {
        /// <summary>
        /// Zum Ermitteln des Hauptpfades des Webprojektes
        /// </summary>
        string GetParentDirectory(string path, string pathNameToFind);

        /// <summary>
        /// Gibt zum �bergebenen Dateinamen den Ausgabepfad zur�ck in dem die Proxy Dateien erstellt werden sollen.
        /// </summary>
        /// <param name="fileName">Der Dateiname der an den Pfad angeh�ngt werden soll.</param>
        /// <param name="alternateOutputPath">Ein alternativer Ausgabepfad der �bergeben werden kann</param>
        string GetProxyFileOutputPath(string fileName, string alternateOutputPath);
    }
}