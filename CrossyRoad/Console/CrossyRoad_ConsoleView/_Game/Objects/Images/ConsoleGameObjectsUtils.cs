using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace CrossyRoad_ConsoleView._Game.Objects.Images
{
  /// <summary>
  /// Утилита для отображения игровых объектов в консоли
  /// </summary>
  public static class ConsoleGameObjectsUtils
  {
    #region Импорты для записи в буфер
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);
    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutput(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
      public short X;
      public short Y;
      public Coord(short X, short Y)
      {
        this.X = X;
        this.Y = Y;
      }
    };
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
      [FieldOffset(0)] public char UnicodeChar;
      [FieldOffset(0)] public byte AsciiChar;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
      [FieldOffset(0)] public CharUnion Char;
      [FieldOffset(2)] public short Attributes;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
      public short Left;
      public short Top;
      public short Right;
      public short Bottom;
    }
    #endregion

    /// <summary>
    /// Индекс строки расположения левого верхнего угла изображения игрового поля
    /// </summary>
    private const int GAME_FIELD_START_I = 3;
    /// <summary>
    /// Индекс столбца расположения левого верхнего угла изображения игрового поля
    /// </summary>
    private const int GAME_FIELD_START_J = 2;
    /// <summary>
    /// Изображение игрового поля со всеми игровыми объектами
    /// </summary>
    private static ConsoleGameObjectImage _gameFieldImage;

    /// <summary>
    /// Инициализирует изображение игрового поля
    /// </summary>
    /// <param name="parWidth">Ширина игрового поля ("пиксель")</param>
    /// <param name="parHeight">Высота игрового поля ("пиксель")</param>
    public static void InitializeGameFieldImage(int parWidth, int parHeight)
    {
      _gameFieldImage = new ConsoleGameObjectImage
      {
        Pixels = new ConsoleColor?[parHeight, parWidth]
      };
    }

    /// <summary>
    /// Добавляет изображение игрового объекта на изображение игрового поля
    /// </summary>
    /// <param name="parImage">Изображение игрового объекта</param>
    /// <param name="parStartI">Индекс строки левого верхнего угла изображения</param>
    /// <param name="parStartJ">Индекс столбца левого верхнего угла изображения</param>
    public static void DrawImageInBuffer(ConsoleGameObjectImage parImage, int parStartI, int parStartJ)
    {
      int width = parImage.Pixels.GetLength(1);
      int height = parImage.Pixels.GetLength(0);
      int gameFieldWidth = _gameFieldImage.Pixels.GetLength(1);
      int gameFieldHeight = _gameFieldImage.Pixels.GetLength(0);
      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          int realI = parStartI + i;
          int realJ = parStartJ + j;
          if (realI >= 0 && realI < gameFieldHeight
              && realJ >= 0 && realJ < gameFieldWidth
              && parImage.Pixels[i, j] != null)
          {
            _gameFieldImage.Pixels[realI, realJ] = parImage.Pixels[i, j];
          }
        }
      }
    }

    /// <summary>
    /// Отображает поверхность полосы препятствий на изображении игрового поля
    /// </summary>
    /// <param name="parColor">Цвет поверхности полосы препятствий</param>
    /// <param name="parStartI">Индекс строки левого верхнего угла полосы препятствий</param>
    public static void DrawLevelBackgroundInBuffer(ConsoleColor parColor, int parStartI)
    {
      int gameFieldWidth = _gameFieldImage.Pixels.GetLength(1);
      int gameFieldHeight = _gameFieldImage.Pixels.GetLength(0);
      for (int i = 0; i < ConsoleViewConfiguration.GAME_OBJECTS_SCALE; i++)
      {
        int realI = parStartI + i;
        if (realI >= 0 && realI < gameFieldHeight)
        {
          for (int j = 0; j < gameFieldWidth; j++)
          {
            _gameFieldImage.Pixels[realI, j] = parColor;
          }
        }
      }
    }

    /// <summary>
    /// Отображает буфер в консоли
    /// </summary>
    [STAThread]
    public static void DrawBufferInConsole()
    {
      using SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
      if (!h.IsInvalid)
      {
        int width = _gameFieldImage.Pixels.GetLength(1);
        int height = _gameFieldImage.Pixels.GetLength(0) / 2;
        SmallRect rect = new SmallRect()
        {
          Left = GAME_FIELD_START_J,
          Top = GAME_FIELD_START_I,
          Right = (short)(width + GAME_FIELD_START_J),
          Bottom = (short)(height + GAME_FIELD_START_I)
        };
        WriteConsoleOutput(h, ImageBufferToConsoleBuffer(), new Coord() { X = (short)width, Y = (short)height },
          new Coord() { X = 0, Y = 0 }, ref rect);
      }
    }

    /// <summary>
    /// Преобразует изображение в буфер консоли
    /// </summary>
    /// <returns>Буфер символов консоли</returns>
    private static CharInfo[] ImageBufferToConsoleBuffer()
    {
      ConsoleColor?[,] pixels = _gameFieldImage.Pixels;
      int width = pixels.GetLength(1);
      int height = pixels.GetLength(0);
      CharInfo[] buffer = new CharInfo[width * (height / 2)];
      for (int i = 0; i < height; i += 2)
      {
        for (int j = 0; j < width; j++)
        {
          if (pixels[i, j] != null && pixels[i + 1, j] != null)
          {
            ConsoleColor foregroundColor = (ConsoleColor)pixels[i, j];
            ConsoleColor backgroundColor = (ConsoleColor)pixels[i + 1, j];
            buffer[(i / 2) * width + j].Char.AsciiChar = 220;
            buffer[(i / 2) * width + j].Attributes = (short)((int)backgroundColor | (((short)(foregroundColor)) << 4));
          }
        }
      }
      return buffer;
    }

    /// <summary>
    /// Отражает изображение по горизонтали
    /// </summary>
    /// <param name="parImage">Исходное изображение</param>
    /// <returns>Отражённое изображение</returns>
    public static ConsoleGameObjectImage FlipImageHorizontally(ConsoleGameObjectImage parImage)
    {
      ConsoleGameObjectImage flippedImage = new ConsoleGameObjectImage();
      int width = parImage.Pixels.GetLength(1);
      int height = parImage.Pixels.GetLength(0);
      flippedImage.Pixels = new ConsoleColor?[height, width];
      for (int i = 0; i < height; i++)
      {
        for (int j = 0; j < width; j++)
        {
          flippedImage.Pixels[i, j] = parImage.Pixels[i, width - j - 1];
        }
      }
      return flippedImage;
    }
  }
}