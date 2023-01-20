using CrossyRoad_Controller;
using System;

namespace CrossyRoad_ConsoleController
{
  /// <summary>
  /// Утилита для работы с контроллерами
  /// </summary>
  public static class ConsoleControllerUtils
  {
    /// <summary>
    /// Запускает цикл ожидания нажатия клавиши выхода,
    /// после получения устанавливает флаг окончания работы контроллера
    /// </summary>
    /// <param name="parController">Контроллер</param>
    /// <param name="parExitKey">Клавиша выхода</param>
    public static void StartStaticWindowKeyboardProccessing(Controller parController, ConsoleKey parExitKey)
    {
      ConsoleKeyInfo keyInfo;
      do
      {
        keyInfo = Console.ReadKey(true);
      } while (keyInfo.Key != parExitKey);
      parController.IsWorkEnded = true;
    }
  }
}