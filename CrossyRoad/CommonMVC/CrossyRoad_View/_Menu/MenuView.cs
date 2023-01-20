using CrossyRoad_Model._Menu;
using System.Collections.Generic;

namespace CrossyRoad_View._Menu
{
  /// <summary>
  /// Представление окна с меню
  /// </summary>
  public abstract class MenuView : View
  {
    /// <summary>
    /// Модель меню
    /// </summary>
    protected Menu Menu { get; }
    /// <summary>
    /// Представления пунктов меню
    /// </summary>
    protected List<MenuItemView> ItemsViews { get; }

    /// <summary>
    /// Конструктор представления меню
    /// </summary>
    /// <param name="parMenu">Модель меню</param>
    public MenuView(Menu parMenu)
      : base()
    {
      ItemsViews = new List<MenuItemView>();
      Menu = parMenu;
    }
  }
}