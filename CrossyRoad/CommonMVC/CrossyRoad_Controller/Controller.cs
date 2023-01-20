namespace CrossyRoad_Controller
{
  /// <summary>
  /// Абстрактный контроллер
  /// </summary>
  public abstract class Controller
  {
    /// <summary>
    /// Флаг окончания работы контроллера
    /// </summary>
    public bool IsWorkEnded { get; set; }

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    public Controller()
    {
      IsWorkEnded = false;
    }

    /// <summary>
    /// Запускает часть программы под управлением контроллера
    /// </summary>
    public abstract void Start();
  }
}