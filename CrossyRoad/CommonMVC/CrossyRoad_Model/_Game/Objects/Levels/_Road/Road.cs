using System;
using System.Collections.ObjectModel;

namespace CrossyRoad_Model._Game.Objects.Levels._Road
{
  /// <summary>
  /// Модель дороги
  /// </summary>
  public class Road : Level
  {
    /// <summary>
    /// Машины
    /// </summary>
    public ObservableCollection<Car> Cars { get; }
    /// <summary>
    /// Скорость всех машин на этой дороге (клетка/сек)
    /// </summary>
    private readonly float _allCarsSpeed;

    /// <summary>
    /// Конструктор модели дороги
    /// </summary>
    /// <param name="parGameField">Игровое поле</param>
    /// <param name="parY">Координата по вертикали (клетка)</param>
    public Road(GameField parGameField, float parY)
      : base(parGameField, parY)
    {
      _allCarsSpeed = (float)((Random.Next(2) == 0 ? -1 : 1)
        * (Random.NextDouble() * (GameSettings.MaxCarSpeed - GameSettings.MinCarSpeed) + GameSettings.MinCarSpeed));
      Cars = new ObservableCollection<Car>();
      while (Cars.Count == 0 || Cars[^1].AbsoluteX < GameField.AbsoluteX + GameField.Width)
      {
        Car car = (Car)GenerateObject(true);
        if (car != null)
        {
          Cars.Add(car);
        }
      }
      while (Cars[0].AbsoluteX > GameField.AbsoluteX)
      {
        Car car = (Car)GenerateObject(false);
        if (car != null)
        {
          Cars.Insert(0, car);
        }
      }
    }

    /// <summary>
    /// Генерирует машину на дороге на некотором расстояниии от крайней
    /// </summary>
    /// <param name="parGenerateOnRight">Флаг генерации машины справа от уже существующих (true), или слева (false)</param>
    /// <returns>Сгенерированная машина</returns>
    protected override GameObject GenerateObject(bool parGenerateOnRight)
    {
      float x = GameField.AbsoluteX;
      int dxMin = Cars.Count == 0 ? 0 : GameSettings.MinDistanceBetweenCars;
      int dx = Random.Next(dxMin, GameSettings.MaxDistanceBetweenCars + 1);
      if (Cars.Count > 0)
      {
        x = Cars[(parGenerateOnRight ? ^1 : 0)].AbsoluteX;
        dx += (int)Cars[(parGenerateOnRight ? ^1 : 0)].Width;
      }
      dx *= parGenerateOnRight ? 1 : -1;
      return new Car(this, x + dx, _allCarsSpeed);
    }

    /// <summary>
    /// Изменяет состояние модели дороги
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public override void ChangeState(long parTimeMilliseconds)
    {
      Move(parTimeMilliseconds);
      foreach (Car elCar in Cars)
      {
        elCar.Move(parTimeMilliseconds);
      }
    }

    /// <summary>
    /// Генерирует машину на дороге, если это необходимо
    /// (например, когда игрок переместился вправо/влево)
    /// </summary>
    public override void GenerateObjectIfNeed()
    {
      if (Cars[0].AbsoluteX > GameField.AbsoluteX)
      {
        Car car = (Car)GenerateObject(false);
        if (car != null)
        {
          Cars.Insert(0, car);
        }
      }
      if (Cars[^1].AbsoluteX + Cars[^1].Width < GameField.AbsoluteX + GameField.Width)
      {
        Car car = (Car)GenerateObject(true);
        if (car != null)
        {
          Cars.Add(car);
        }
      }
    }

    /// <summary>
    /// Проверяет, возникает ли ситуация конца игры на координате по горизонтали
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>true - если игра заканчивается на выбранной координате, false - в противном случае</returns>
    public override bool IsGameOverOnX(float parX)
    {
      foreach (Car elCar in Cars)
      {
        if (elCar.AbsoluteX > parX + ModelConfiguration.PLAYER_WIDTH * 0.25)
        {
          break;
        }
        if (elCar.AbsoluteX + elCar.Width > parX + ModelConfiguration.PLAYER_WIDTH * 0.75)
        {
          return true;
        }
      }
      return false;
    }
  }
}