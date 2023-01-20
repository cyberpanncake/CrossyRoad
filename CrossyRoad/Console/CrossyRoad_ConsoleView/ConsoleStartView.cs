using CrossyRoad_View;
using System;
using System.Threading;

namespace CrossyRoad_ConsoleView
{
  /// <summary>
  /// Представление стартового окна в консольном приложении
  /// </summary>
  public class ConsoleStartView : View
  {
    /// <summary>
    /// Конструктор представление стартового окна в консольном приложении
    /// </summary>
    public ConsoleStartView()
      : base()
    {
      X = 2;
      Y = 0;
      Width = ConsoleViewConfiguration.WINDOW_WIDTH - 2;
      Height = ConsoleViewConfiguration.WINDOW_HEIGHT;
    }

    /// <summary>
    /// Отображает стартовое окно на экране приложения
    /// </summary>
    public override void Draw()
    {
      int pause = 60;
      string[] logo = Properties.Resources.Logo.Split("\n");
      int hight = logo.Length;
      int width = logo[0].Length;
      int x = (int)X + ((int)Width - width) / 2;
      int y0 = (int)Y + ((int)Height - hight) / 2;
      for (int i = 0; i < hight; i++)
      {
        Console.SetCursorPosition(x, y0 + i);
        Console.Write(logo[i]);
        Thread.Sleep(pause);
      }
    }
  }
}