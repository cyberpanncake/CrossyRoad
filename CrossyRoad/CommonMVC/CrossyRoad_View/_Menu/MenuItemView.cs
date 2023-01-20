using CrossyRoad_Model._Menu;

namespace CrossyRoad_View._Menu
{
  /// <summary>
  /// Представление пункта меню
  /// </summary>
  public abstract class MenuItemView : View
  {
    /// <summary>
    /// Модель пункта меню
    /// </summary>
    protected MenuItem Item { get; }

    /// <summary>
    /// Конструктор представления пункта меню
    /// </summary>
    /// <param name="parItem">Модель пункта меню</param>
    public MenuItemView(MenuItem parItem)
      : base()
    {
      Item = parItem;
    }
  }
}