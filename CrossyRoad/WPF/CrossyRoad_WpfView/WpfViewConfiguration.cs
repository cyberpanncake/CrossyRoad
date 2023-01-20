using CrossyRoad_Model._Menu;
using System.Collections.Generic;
using System.Windows.Media;

namespace CrossyRoad_WpfView
{
  /// <summary>
  /// Настройки WPF
  /// </summary>
  public static class WpfViewConfiguration
  {
    /// <summary>
    /// Минимальная ширина окна приложения (пиксели)
    /// </summary>
    public const int WINDOW_MIN_WIDTH = 500;
    /// <summary>
    /// Минимальная высота окна приложения (пиксели)
    /// </summary>
    public const int WINDOW_MIN_HEIGHT = 500;
    /// <summary>
    /// Начальная ширина окна приложения (пиксели)
    /// </summary>
    public const int WINDOW_START_WIDTH = 700;
    /// <summary>
    /// Начальная высота окна приложения (пиксели)
    /// </summary>
    public const int WINDOW_START_HEIGHT = 700;
    /// <summary>
    /// Цвет фона по умолчанию
    /// </summary>
    public static readonly Brush DefaultBackgroundColor = Brushes.DarkGreen;
    /// <summary>
    /// Цвет текста по умолчанию
    /// </summary>
    public static readonly Brush DefaultForegroundColor = Brushes.White;
    /// <summary>
    /// Цвет пункта меню в зависимости от его состояния
    /// </summary>
    public static readonly Dictionary<MenuItemState, Brush> MenuItemStatesColors =
      new Dictionary<MenuItemState, Brush>()
    {
      { MenuItemState.Default, Brushes.White },
      { MenuItemState.Focused, Brushes.Cyan },
      { MenuItemState.Selected, Brushes.DarkBlue }
    };
    /// <summary>
    /// Размер шрифта по умолчанию
    /// </summary>
    public const int DEFAULT_FONT_SIZE = 18;
    /// <summary>
    /// Размер шрифта подсказки
    /// </summary>
    public const int HELP_TEXT_FONT_SIZE = 14;
    /// <summary>
    /// Размер шрифта логотипа игры
    /// </summary>
    public const int LOGO_FONT_SIZE = 50;
    /// <summary>
    /// Размер шрифта надписи с обратным отсчётом пере игрой
    /// </summary>
    public const int COUNTDOWN_FONT_SIZE = 50;
    /// <summary>
    /// Шрифт логотипа
    /// </summary>
    public static readonly FontFamily LogoFontFamily = new FontFamily("Jokerman");
    /// <summary>
    /// Отступ от родительского элемента по умолчанию (пиксели)
    /// </summary>
    public const float DEFAULT_MARGIN = 10;
    /// <summary>
    /// Отступы ячейки таблицы (пиксели)
    /// </summary>
    public const float TABLE_CELL_MARGIN = 5;
    /// <summary>
    /// Минимальная высота ячейки таблицы (пиксели)
    /// </summary>
    public const float TABLE_CELL_MIN_HEIGHT = 20;
    /// <summary>
    /// Промежуток времени, через который происходит перерисовка игрового поля (мс)
    /// </summary>
    public const int GAME_REDRAW_PERIOD = 40;
  }
}