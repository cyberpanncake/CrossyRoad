using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CrossyRoad_ConsoleView._Game.Objects.Images
{
  /// <summary>
  /// Хранилище изображений игровых объектов
  /// </summary>
  public static class ConsoleGameObjectsImages
  {
    /// <summary>
    /// Изображение игрока
    /// </summary>
    public static ConsoleGameObjectImage Player { get; }
    /// <summary>
    /// Изображение камня
    /// </summary>
    public static ConsoleGameObjectImage Stone { get; }
    /// <summary>
    /// Изображение куста
    /// </summary>
    public static ConsoleGameObjectImage Bush { get; }
    /// <summary>
    /// Изображение синей машины
    /// </summary>
    public static ConsoleGameObjectImage CarBlue { get; }
    /// <summary>
    /// Изображение зелёной машины
    /// </summary>
    public static ConsoleGameObjectImage CarGreen { get; }
    /// <summary>
    /// Изображение красной машины
    /// </summary>
    public static ConsoleGameObjectImage CarRed { get; }
    /// <summary>
    /// Изображение фиолетовой машины
    /// </summary>
    public static ConsoleGameObjectImage CarPurple { get; }
    /// <summary>
    /// Изображение бревна длины 2
    /// </summary>
    public static ConsoleGameObjectImage Log2 { get; }
    /// <summary>
    /// Изображение бревна длины 3
    /// </summary>
    public static ConsoleGameObjectImage Log3 { get; }
    /// <summary>
    /// Изображение бревна длины 4
    /// </summary>
    public static ConsoleGameObjectImage Log4 { get; }

    /// <summary>
    /// Статический конструктор, инициализирует изображения игровых объектов из ресурсов
    /// </summary>
    static ConsoleGameObjectsImages()
    {
      Player = LoadImageFromResource(Properties.Resources.Player);
      Stone = LoadImageFromResource(Properties.Resources.Stone);
      Bush = LoadImageFromResource(Properties.Resources.Bush);
      CarBlue = LoadImageFromResource(Properties.Resources.CarBlue);
      CarGreen = LoadImageFromResource(Properties.Resources.CarGreen);
      CarRed = LoadImageFromResource(Properties.Resources.CarRed);
      CarPurple = LoadImageFromResource(Properties.Resources.CarPurple);
      Log2 = LoadImageFromResource(Properties.Resources.Log2);
      Log3 = LoadImageFromResource(Properties.Resources.Log3);
      Log4 = LoadImageFromResource(Properties.Resources.Log4);
    }

    /// <summary>
    /// Загружает изображение игрового объекта из ресурса
    /// </summary>
    /// <param name="parResource">Ресурс</param>
    /// <returns>Изображения игрового объекта</returns>
    private static ConsoleGameObjectImage LoadImageFromResource(byte[] parResource)
    {
      BinaryFormatter formatter = new BinaryFormatter();
      using Stream stream = new MemoryStream(parResource);
      return (ConsoleGameObjectImage)formatter.Deserialize(stream);
    }
  }
}