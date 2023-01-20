using CrossyRoad_View._Information;

namespace CrossyRoad_Controller._Information
{
  /// <summary>
  /// Контроллер окна справки (пункт меню "Как играть")
  /// </summary>
  public abstract class InformationController : Controller
  {
    /// <summary>
    /// Представление окна справки
    /// </summary>
    protected InformationView InformationView { get; }

    /// <summary>
    /// Конструктор контроллера справки (пункт меню "Как играть")
    /// </summary>
    /// <param name="parInformationView">Представление окна справки</param>
    public InformationController(InformationView parInformationView)
      : base()
    {
      InformationView = parInformationView;
    }
  }
}