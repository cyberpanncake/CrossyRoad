using CrossyRoad_Model._Menu;
using CrossyRoad_View._Menu;

namespace CrossyRoad_Controller._Menu
{
  /// <summary>
  /// Контроллер окна меню
  /// </summary>
  public abstract class MenuController : Controller
  {
    /// <summary>
    /// Модель меню
    /// </summary>
    protected Menu Menu { get; }
    /// <summary>
    /// Представление окна меню
    /// </summary>
    protected MenuView MenuView { get; }

    /// <summary>
    /// Конструктор контроллера окна меню
    /// </summary>
    /// <param name="parMenu">Меню</param>
    /// <param name="parMenuView">Представление окна меню</param>
    public MenuController(Menu parMenu, MenuView parMenuView) : base()
    {
      Menu = parMenu;
      MenuView = parMenuView;
    }
  }
}