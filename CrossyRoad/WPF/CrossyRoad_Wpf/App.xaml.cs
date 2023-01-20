using CrossyRoad_WpfController;
using System.Windows;

namespace CrossyRoad_Wpf
{
  /// <summary>
  /// Графический вариант приложения
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    /// Обработчик события начала работы приложения
    /// </summary>
    /// <param name="e">Аргументы события</param>
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      new WpfStartController().Start();
    }
  }
}