using System;

namespace CrossyRoad_ConsoleView._Game.Objects.Images
{
  /// <summary>
  /// Изображение игрового объекта в консольном приложении
  /// </summary>
  [Serializable]
  public class ConsoleGameObjectImage
  {
    /// <summary>
    /// Матрица цветов "пикселей" изображения, null - отсутствие цвета (прозрачный "пиксель")
    /// </summary>
    public ConsoleColor?[,] Pixels { get; set; }
  }
}