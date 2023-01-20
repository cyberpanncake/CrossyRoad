using System.IO;
using System.Windows.Media.Imaging;

namespace CrossyRoad_WpfView._Game.Objects.Images
{
  /// <summary>
  /// Хранилище изображений игровых объектов для WPF
  /// </summary>
  public static class WpfGameObjectsImages
  {
    /// <summary>
    /// Изображение игрока
    /// </summary>
    public static BitmapImage Player { get; }
    /// <summary>
    /// Изображение поля (1 клетка)
    /// </summary>
    public static BitmapImage Field { get; }
    /// <summary>
    /// Изображение камня
    /// </summary>
    public static BitmapImage Stone { get; }
    /// <summary>
    /// Изображение куста
    /// </summary>
    public static BitmapImage Bush { get; }
    /// <summary>
    /// Изображение дороги (1 клетка)
    /// </summary>
    public static BitmapImage Road { get; }
    /// <summary>
    /// Изображение синей машины
    /// </summary>
    public static BitmapImage CarBlue { get; }
    /// <summary>
    /// Изображение зелёной машины
    /// </summary>
    public static BitmapImage CarGreen { get; }
    /// <summary>
    /// Изображение красной машины
    /// </summary>
    public static BitmapImage CarRed { get; }
    /// <summary>
    /// Изображение фиолетовой машины
    /// </summary>
    public static BitmapImage CarPurple { get; }
    /// <summary>
    /// Изображение реки (1 клетка)
    /// </summary>
    public static BitmapImage River { get; }
    /// <summary>
    /// Изображение бревна длины 2
    /// </summary>
    public static BitmapImage Log2 { get; }
    /// <summary>
    /// Изображение бревна длины 3
    /// </summary>
    public static BitmapImage Log3 { get; }
    /// <summary>
    /// Изображение бревна длины 4
    /// </summary>
    public static BitmapImage Log4 { get; }

    /// <summary>
    /// Конструктор хранилища изображений игровых объектов
    /// </summary>
    static WpfGameObjectsImages()
    {
      Player = LoadImageFromResource(Properties.Resources.Player);
      Field = LoadImageFromResource(Properties.Resources.Field);
      Stone = LoadImageFromResource(Properties.Resources.Stone);
      Bush = LoadImageFromResource(Properties.Resources.Bush);
      Road = LoadImageFromResource(Properties.Resources.Road);
      CarBlue = LoadImageFromResource(Properties.Resources.CarBlue);
      CarGreen = LoadImageFromResource(Properties.Resources.CarGreen);
      CarRed = LoadImageFromResource(Properties.Resources.CarRed);
      CarPurple = LoadImageFromResource(Properties.Resources.CarPurple);
      River = LoadImageFromResource(Properties.Resources.River);
      Log2 = LoadImageFromResource(Properties.Resources.Log2);
      Log3 = LoadImageFromResource(Properties.Resources.Log3);
      Log4 = LoadImageFromResource(Properties.Resources.Log4);
    }

    /// <summary>
    /// Загружает изображение игрового объекта из ресурса
    /// </summary>
    /// <param name="parResource">Ресурс изображения</param>
    /// <returns>Изображение игрового объекта</returns>
    private static BitmapImage LoadImageFromResource(byte[] parResource)
    {
      if (parResource == null || parResource.Length == 0)
      {
        return null;
      }
      BitmapImage image = new BitmapImage();
      using (MemoryStream mem = new MemoryStream(parResource))
      {
        mem.Position = 0;
        image.BeginInit();
        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.UriSource = null;
        image.StreamSource = mem;
        image.EndInit();
      }
      image.Freeze();
      return image;
    }
  }
}