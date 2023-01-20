using CrossyRoad_Model._Information;

namespace CrossyRoad_View._Information
{
  /// <summary>
  /// Представление окна справки (пункт меню "Как играть")
  /// </summary>
  public abstract class InformationView : View
  {
    /// <summary>
    /// Текст справки
    /// </summary>
    protected string InformationText { get; }

    /// <summary>
    /// Конструктор представления окна справки
    /// </summary>
    public InformationView()
      : base()
    {
      InformationText = Information.Text;
    }
  }
}