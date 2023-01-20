using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CrossyRoad_ConsoleView
{
  /// <summary>
  /// Утилита для настройки и работы с консолью
  /// </summary>
  public static class ConsoleUtils
  {
    #region Импорты для изменения размеров окна
    /// <summary>
    /// Указывает, что parNPosition (в методе DeleteMenu)
    /// предоставляет идентификатор элемента меню
    /// </summary>
    private const int MF_BYCOMMAND = 0x00000000;
    /// <summary>
    /// Значение идентификатора меню развёртывания окна
    /// </summary>
    private const int SC_MAXIMIZE = 0xF030;
    /// <summary>
    /// Значение идентификатора меню изменения размеров окна
    /// </summary>
    private const int SC_SIZE = 0xF000;

    /// <summary>
    /// Удаляет элемент из указанного меню
    /// </summary>
    /// <param name="parHMenu">Дескриптор меню, который необходимо изменить</param>
    /// <param name="parNPosition">Удаляемый элемент меню, определяемый параметром parWFlags</param>
    /// <param name="parWFlags">Указывает, как интерпретируется параметр parNPosition.
    /// MF_BYCOMMAND - Указывает, что parNPosition предоставляет идентификатор элемента меню.
    /// MF_BYPOSITION - Указывает, что parNPosition дает относительное положение элемента меню
    /// отсчитываемое от нуля.</param>
    /// <returns>Если функция выполняется успешно, возвращается ненулевое значение.
    /// Если функция выполняется неудачно, возвращается нулевое значение</returns>
    [DllImport("user32.dll")]
    public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

    /// <summary>
    /// Получить доступ к меню окна для копирования и изменения
    /// </summary>
    /// <param name="parHWnd">Дескриптор окна, который будет принадлежать копии меню окна</param>
    /// <param name="parBRevert">Если этот параметр имеет значение FALSE, GetSystemMenu
    /// возвращает дескриптор копии используемого меню окна. Если этот параметр имеет значение TRUE,
    /// GetSystemMenu сбрасывает меню окна обратно в состояние по умолчанию. Предыдущее меню окна,
    /// если таковое имеется, уничтожается.</param>
    /// <returns>Если параметр parBRevert имеет значение FALSE, возвращаемое значение является дескриптором копии меню окна.
    /// Если параметр parBRevert имеет значение TRUE, возвращаемое значение равно NULL</returns>
    [DllImport("user32.dll")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    /// <summary>
    /// Извлекает дескриптор окна, используемый консолью, связанной с вызывающим процессом
    /// </summary>
    /// <returns>Дескриптор окна, используемого консолью, связанной с вызывающим процессом
    /// или NULL, если нет какой-либо связанной консоли.</returns>
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();
    #endregion

    #region Импорты для установки положения окна приложения на экране компьютера
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    [DllImport("User32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    /// <summary>
    /// Иизменяет размер, позицию и Z-индекс окна
    /// </summary>
    /// <param name="parHWnd">Дескриптор окна</param>
    /// <param name="parHWndInsertAfter">Дескриптор порядка размещения</param>
    /// <param name="parX">Позиция по горизонтали</param>
    /// <param name="parY">Позиция по горизонтали</param>
    /// <param name="parCx">Новая ширина</param>
    /// <param name="parCy">Новая высота</param>
    /// <param name="parUFlags">Флаги позиционирования окна</param>
    /// <returns>Если функция завершилась успешно, возвращается значение - не нуль.
    /// Если функция потерпела неудачу, возвращаемое значение - ноль. </returns>
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    #endregion

    /// <summary>
    /// Подготавливает окно для отображения:
    /// устанавливает его размеры, положение, цвет фона и шрифта
    /// </summary>
    public static void PrepareWindow()
    {
      Console.Title = CrossyRoad_View.Properties.Resources.Game_Name;
      Console.WindowWidth = ConsoleViewConfiguration.WINDOW_WIDTH;
      Console.WindowHeight = ConsoleViewConfiguration.WINDOW_HEIGHT;
      Console.BufferWidth = ConsoleViewConfiguration.WINDOW_WIDTH;
      Console.BufferHeight = ConsoleViewConfiguration.WINDOW_HEIGHT;
      Console.CursorVisible = false;
      ClearWindow();
      IntPtr handle = GetConsoleWindow();
      IntPtr sysMenu = GetSystemMenu(handle, false);
      {
        DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
        DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
      }
      SetWindowPositionToLeftTop();
    }

    /// <summary>
    /// Очищает окно и устанавливает цвета текста и фона по умолчанию
    /// </summary>
    public static void ClearWindow()
    {
      Console.BackgroundColor = ConsoleViewConfiguration.DEFAULT_BACK_COLOR;
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      Console.Clear();
    }

    /// <summary>
    /// Устанавливает положения окна в левом верхнем углу экрана
    /// </summary>
    private static void SetWindowPositionToLeftTop()
    {
      var hWnd = FindWindow(null, Console.Title);
      _ = new RECT();
      var SWP_NOSIZE = 0x1;
      var HWND_TOPMOST = 0;
      SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE);
    }

    /// <summary>
    /// Отображает текст, выровненный по центру, на заданной строке
    /// </summary>
    /// <param name="parText">Текст</param>
    /// <param name="parI">Индекс строки</param>
    /// <param name="parBackgroundColor">Цвет фона</param>
    /// <param name="parForegroundColor">Цвет текста</param>
    public static void DrawCenteredText(string parText, int parI,
      ConsoleColor? parBackgroundColor = null, ConsoleColor? parForegroundColor = null)
    {
      if (parBackgroundColor != null)
      {
        Console.BackgroundColor = (ConsoleColor)parBackgroundColor;
      }
      if (parForegroundColor != null)
      {
        Console.ForegroundColor = (ConsoleColor)parForegroundColor;
      }
      Console.SetCursorPosition((ConsoleViewConfiguration.WINDOW_WIDTH - parText.Length) / 2, parI);
      Console.Write(parText);
    }

    /// <summary>
    /// Отображает текст, выровненный по центру, на заданной строке
    /// с цветами текста и фона по умолчанию
    /// </summary>
    /// <param name="parText">Текст</param>
    /// <param name="parI">Индекс строки</param>
    public static void DrawCenteredTextWithDefaultColors(string parText, int parI)
    {
      Console.BackgroundColor = ConsoleViewConfiguration.DEFAULT_BACK_COLOR;
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      Console.SetCursorPosition((ConsoleViewConfiguration.WINDOW_WIDTH - parText.Length) / 2, parI);
      Console.Write(parText);
    }

    /// <summary>
    /// Отображает текст подсказки
    /// </summary>
    public static void DrawHelpText(string helpText)
    {
      Console.ForegroundColor = ConsoleViewConfiguration.DEFAULT_FORE_COLOR;
      Console.SetCursorPosition(ConsoleViewConfiguration.WINDOW_WIDTH - helpText.Length - 2, 1);
      Console.Write(helpText);
    }

    /// <summary>
    /// Рассчитывает координаты верхнего левого угла отображаемого объекта так, чтобы он находился в центре экрана
    /// с учётом текста подсказки сверху окна
    /// </summary>
    /// <param name="parWidth">Ширина объекта (строки консоли)</param>
    /// <param name="parHeight">Высота объекта (столбцы консоли)</param>
    /// <param name="outI">Рассчитанный индекс строки</param>
    /// <param name="outJ">Рассчитанный индекс столбца</param>
    public static void GetViewPositionInCenterWithHelpText(float parWidth, float parHeight,
      out float outI, out float outJ)
    {
      outI = 3 + (ConsoleViewConfiguration.WINDOW_HEIGHT - 4 - parHeight) / 2;
      outJ = 2 + (ConsoleViewConfiguration.WINDOW_WIDTH - 4 - parWidth) / 2;
    }

    /// <summary>
    /// Отображает текст обратного отсчёта, выровненный по центру, на заданной строке
    /// </summary>
    /// <param name="parCountDownStart">Начальное значение обратного отсчёта</param>
    /// <param name="parI">Индекс строки</param>
    /// <param name="parBackGroundColor">Цвет фона</param>
    /// <param name="parForegroundColor">Цвет текста</param>
    public static void DrawCountDown(int parCountDownStart, int parI,
      ConsoleColor? parBackGroundColor = null, ConsoleColor? parForegroundColor = null)
    {
      for (int i = parCountDownStart; i > 0; i--)
      {
        DrawCenteredText(i.ToString(), parI, parBackGroundColor, parForegroundColor);
        Thread.Sleep(1000);
      }
      DrawCenteredText("Старт", parI, parBackGroundColor, parForegroundColor);
      Thread.Sleep(200);
    }

    /// <summary>
    /// Отображает поле ввода текста, отцентрованное по ширине экрана, на заданной строке
    /// </summary>
    /// <param name="parLength">Максимальная длина текста в поле ввода</param>
    /// <param name="parI">Индекс строки</param>
    public static void DrawCenteredTextField(int parLength, int parI)
    {
      string s = new string(' ', parLength + 2);
      DrawCenteredText(s, parI, ConsoleColor.White, ConsoleColor.Black);
    }
  }
}