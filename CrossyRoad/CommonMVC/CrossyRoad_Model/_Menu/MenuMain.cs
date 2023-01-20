using System;

namespace CrossyRoad_Model._Menu
{
  /// <summary>
  /// Модель главного меню
  /// </summary>
  public class MenuMain : Menu
  {
    /// <summary>
    /// Единственный объект главного меню
    /// </summary>
    private static MenuMain _instance;
    /// <summary>
    /// Единственный объект главного меню
    /// </summary>
    public static MenuMain Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new MenuMain();
        }
        return _instance;
      }
    }

    /// <summary>
    /// Конструктор модели главного меню
    /// </summary>
    private MenuMain() : base()
    {
      foreach (ModelConfiguration.MenuMainItemsPositions elPosition
        in Enum.GetValues(typeof(ModelConfiguration.MenuMainItemsPositions)))
      {
        AddItem(new MenuItem(ModelConfiguration.GetMenuMainItemName(elPosition)), (int)elPosition);
      }
      Items[0].State = MenuItemState.Focused;
    }
  }
}