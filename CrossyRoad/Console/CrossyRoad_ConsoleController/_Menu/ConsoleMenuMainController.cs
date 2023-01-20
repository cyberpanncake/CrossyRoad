using CrossyRoad_Model._Menu;
using CrossyRoad_ConsoleView._Menu;
using System;
using System.Threading;
using CrossyRoad_Model;
using CrossyRoad_Model._Highscores;
using CrossyRoad_ConsoleController._Information;
using CrossyRoad_Controller._Menu;
using CrossyRoad_ConsoleController._Highscores;
using CrossyRoad_ConsoleController._Game;
using CrossyRoad_Model._Game;

namespace CrossyRoad_ConsoleController._Menu
{
  /// <summary>
  /// Контроллер окна главного меню в консольном приложении
  /// </summary>
  public class ConsoleMenuMainController : MenuController
  {
    /// <summary>
    /// Конструктор контроллера окна главного меню в консольном приложении
    /// </summary>
    public ConsoleMenuMainController()
      : base(MenuMain.Instance, new ConsoleMenuMainView())
    {
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Game].Selected += StartGame;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Highscores].Selected += StartHighscores;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Information].Selected += StartInformation;
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Exit].Selected += () => { IsWorkEnded = true; };
    }

    /// <summary>
    /// Запускает переход в игру
    /// </summary>
    private void StartGame()
    {
      new ConsoleGameController(new Game()).Start();
      MenuView.Draw();
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Game].State = MenuItemState.Focused;
    }

    /// <summary>
    /// Запускает переход в таблицу рекордов
    /// </summary>
    private void StartHighscores()
    {
      new ConsoleHighscoresController(Highscores.Instance).Start();
      MenuView.Draw();
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Highscores].State = MenuItemState.Focused;
    }

    /// <summary>
    /// Запускает переход в справку
    /// </summary>
    private void StartInformation()
    {
      new ConsoleInformationController().Start();
      MenuView.Draw();
      Menu.Items[(int)ModelConfiguration.MenuMainItemsPositions.Information].State = MenuItemState.Focused;
    }

    /// <summary>
    /// Запускает переход в главное меню под управлением контроллера
    /// </summary>
    public override void Start()
    {
      MenuView.Draw();
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            Menu.MoveFocusToPreviousItem();
            break;
          case ConsoleKey.DownArrow:
            Menu.MoveFocusToNextItem();
            break;
          case ConsoleKey.Enter:
            Menu.SelectCurrentItem();
            break;
        }
      } while (!IsWorkEnded);
      Thread.Sleep(500);
    }
  }
}