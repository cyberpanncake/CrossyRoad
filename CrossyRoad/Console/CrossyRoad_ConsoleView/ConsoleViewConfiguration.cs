using CrossyRoad_Model._Menu;
using System;
using System.Collections.Generic;

namespace CrossyRoad_ConsoleView
{
  /// <summary>
  /// Класс с настройками представления в консольном приложении
  /// </summary>
  public static class ConsoleViewConfiguration
  {
    /// <summary>
    /// Ширина окна (столбцы консоли)
    /// </summary>
    public const int WINDOW_WIDTH = 124;
    /// <summary>
    /// Высота окна (строки консоли)
    /// </summary>
    public const int WINDOW_HEIGHT = 44;
    /// <summary>
    /// Цвет фона по умолчанию
    /// </summary>
    public const ConsoleColor DEFAULT_BACK_COLOR = ConsoleColor.DarkGreen;
    /// <summary>
    /// Цвет текста по умолчанию
    /// </summary>
    public const ConsoleColor DEFAULT_FORE_COLOR = ConsoleColor.White;
    /// <summary>
    /// Цвет пункта меню в зависимости от его состояния
    /// </summary>
    public static readonly Dictionary<MenuItemState, ConsoleColor> MenuItemStatesColors =
      new Dictionary<MenuItemState, ConsoleColor>()
    {
      { MenuItemState.Default, ConsoleColor.White },
      { MenuItemState.Focused, ConsoleColor.Cyan },
      { MenuItemState.Selected, ConsoleColor.DarkBlue }
    };
    /// <summary>
    /// Период времени, через который перерисовывается игровое поле в игровом процессе (мс)
    /// </summary>
    public const int GAME_REDRAW_PERIOD = 40;
    /// <summary>
    /// Коэффициент масштабирования ("пиксели" в одной клетке игрового поля по ширине/высоте)
    /// </summary>
    public const float GAME_OBJECTS_SCALE = 8;
  }
}