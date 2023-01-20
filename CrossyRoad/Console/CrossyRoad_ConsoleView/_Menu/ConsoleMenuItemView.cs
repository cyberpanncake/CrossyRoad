using CrossyRoad_Model._Menu;
using CrossyRoad_View._Menu;
using System;

namespace CrossyRoad_ConsoleView._Menu
{
  /// <summary>
  /// Представление пункта меню в консольном приложении
  /// </summary>
  public class ConsoleMenuItemView : MenuItemView
  {
    /// <summary>
    /// Конструктор представления пункта меню в консольном приложении
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    public ConsoleMenuItemView(MenuItem parItem)
      : base(parItem)
    {
    }

    /// <summary>
    /// Отображает пункт меню на экране приложения
    /// </summary>
    public override void Draw()
    {
      Console.ForegroundColor = ConsoleViewConfiguration.MenuItemStatesColors[Item.State];
      int length = Item.Name.Length;
      for (int i = 0; i < length; i += (int)Width)
      {
        int nextI = Math.Min(i + (int)Width, length);
        int currentLength = nextI - i;
        int x = (int)X + ((int)Width - currentLength) / 2;
        Console.SetCursorPosition(x, (int)Y + i);
        Console.Write(Item.Name[i..nextI]);
      }
    }
  }
}