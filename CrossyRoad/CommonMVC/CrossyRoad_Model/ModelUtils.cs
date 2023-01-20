using System.IO;

namespace CrossyRoad_Model
{
  /// <summary>
  /// Утилита для работы с логикой приложения
  /// </summary>
  public static class ModelUtils
  {
    /// <summary>
    /// Создаёт папку для данных игры, если она не существует
    /// </summary>
    public static void CreateAppDataHomeDirectoryIfNotExists()
    {
      if (!Directory.Exists(ModelConfiguration.AppHomeDirectoryPath))
      {
        Directory.CreateDirectory(ModelConfiguration.AppHomeDirectoryPath);
      }
    }
  }
}