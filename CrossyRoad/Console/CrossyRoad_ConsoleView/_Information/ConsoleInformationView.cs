using CrossyRoad_View._Information;
using System;

namespace CrossyRoad_ConsoleView._Information
{
  /// <summary>
  /// Представление окна справки (пункт меню "Как играть") в консольном приложении
  /// </summary>
  public class ConsoleInformationView : InformationView
  {
    /// <summary>
    /// Конструктор представления окна справки в консольном приложении
    /// </summary>
    public ConsoleInformationView()
      : base()
    {
      X = 2;
      Y = 5;
      Width = ConsoleViewConfiguration.WINDOW_WIDTH - X - 2;
      Height = ConsoleViewConfiguration.WINDOW_HEIGHT - Y - 1;
    }

    /// <summary>
    /// Отображает окно справки на экране приложения
    /// </summary>
    public override void Draw()
    {
      Console.Clear();
      ConsoleUtils.DrawHelpText(CrossyRoad_View.Properties.Resources.HelpText_ToMenu);
      ConsoleUtils.DrawCenteredText(CrossyRoad_View.Properties.Resources.Information_Title, 3);
      Console.SetCursorPosition((int)X, (int)Y);
      string[] textLines = InformationText.Split("\n");
      foreach (string line in textLines)
      {
        DrawTextLine(line);
      }
    }

    /// <summary>
    /// Отображает строку текста (с выравниванием по левому красю)
    /// </summary>
    /// <param name="parLine">Строка текста</param>
    private void DrawTextLine(string parLine)
    {
      if (parLine.EndsWith("\r"))
      {
        parLine = parLine[0..(parLine.Length - 1)];
      }
      string[] words = parLine.Split(" ");
      foreach (string word in words)
      {
        int space = (int)(Width - (Console.CursorLeft - X));
        if (word.Length <= space)
        {
          Console.Write(word);
          if (word.Length < space)
          {
            Console.Write(" ");
          }
          continue;
        }
        Console.SetCursorPosition((int)X, Console.CursorTop + 1);
        Console.Write(word + " ");
      }
      Console.SetCursorPosition((int)X, Console.CursorTop + 2);
    }
  }
}