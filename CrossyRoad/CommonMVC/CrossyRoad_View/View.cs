namespace CrossyRoad_View
{
  /// <summary>
  /// Абстрактное представление
  /// </summary>
  public abstract class View
  {
    /// <summary>
    /// Координата левого верхнего угла отображаемого объекта по горизонтали
    /// </summary>
    public float X { get; set; }
    /// <summary>
    /// Координата левого верхнего угла отображаемого объекта по вертикали
    /// </summary>
    public float Y { get; set; }
    /// <summary>
    /// Ширина отображаемого объекта
    /// </summary>
    public float Width { get; set; }
    /// <summary>
    /// Высота отображаемого объекта
    /// </summary>
    public float Height { get; set; }

    /// <summary>
    /// Конструктор представления
    /// </summary>
    public View()
    {
    }

    /// <summary>
    /// Отображает объект на экране приложения
    /// </summary>
    public abstract void Draw();
  }
}