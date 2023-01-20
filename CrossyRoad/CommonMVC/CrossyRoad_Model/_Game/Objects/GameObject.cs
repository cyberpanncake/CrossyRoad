using System;

namespace CrossyRoad_Model._Game.Objects
{
  /// <summary>
  /// Модель игрового объекта
  /// </summary>
  public abstract class GameObject
  {
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    public static readonly Random Random = new Random();
    /// <summary>
    /// Родительский объект (объект, относительно которого расположен текущий)
    /// </summary>
    public GameObject Parent { get; set; }
    /// <summary>
    /// Координата левого верхнего угла объекта по горизонтали
    /// относительно левого верхнего угла родительского объекта (клетка)
    /// </summary>
    public float X { get; set; }
    /// <summary>
    /// Координата левого верхнего угла объекта по вертикали
    /// относительно левого верхнего угла родительского объекта (клетка)
    /// </summary>
    public float Y { get; set; }
    /// <summary>
    /// Абсолютная координата левого верхнего угла по горизонтали
    /// </summary>
    public float AbsoluteX
    {
      get
      {
        if (Parent == null)
        {
          return X;
        }
        return Parent.AbsoluteX + X;
      }
    }
    /// <summary>
    /// Абсолютная координата левого верхнего угла по вертикали (клетка)
    /// </summary>
    public float AbsoluteY
    {
      get
      {
        if (Parent == null)
        {
          return Y;
        }
        return Parent.AbsoluteY + Y;
      }
    }
    /// <summary>
    /// Ширина объекта (клетка)
    /// </summary>
    public float Width { get; }
    /// <summary>
    /// Высота объекта (клетка)
    /// </summary>
    public float Height { get; }

    /// <summary>
    /// Конструктор модели игрового объекта
    /// </summary>
    /// <param name="parParent">Родительский объект</param>
    /// <param name="parX">Относительная координата левого верхнего угла по горизонтали (клетка)</param>
    /// <param name="parY">Относительная координата левого верхнего угла по вертикали (клетка)</param>
    /// <param name="parWidth">Ширина (клетка)</param>
    /// <param name="parHeight">Высота (клетка)</param>
    public GameObject(GameObject parParent, float parX, float parY, float parWidth, float parHeight)
    {
      Parent = parParent;
      X = parX;
      Y = parY;
      Width = parWidth;
      Height = parHeight;
    }
  }
}