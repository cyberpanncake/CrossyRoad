using System;
using System.Collections.ObjectModel;

namespace CrossyRoad_Model._Game.Objects.Levels._Field
{
  /// <summary>
  /// Модель поля
  /// </summary>
  public class Field : Level
  {
    /// <summary>
    /// Статичные препятствия (камни и кусты)
    /// </summary>
    public ObservableCollection<Barrier> Barriers { get; }
    /// <summary>
    /// Предыдущее поле (если соседняя снизу полоса препятствий не является полем,
    /// то _previousField = null)
    /// </summary>
    private readonly Field _previousField;
    // Поля _leftX и _rightX нужны для того, чтобы на уже рассмотренной один раз координате,
    // для которой не было сгенерировано препятствие (т.е. клетка осталась пустой),
    // при повторном рассмотрении не было сгенерировано препятствие
    /// <summary>
    /// Самая левая координата по горизонтали, для которой были сгенерированы препятствия на поле
    /// </summary>
    private float _leftX;
    /// <summary>
    /// Самая правая координата по горизонтали, для которой были сгенерированы препятствия на поле
    /// </summary>
    private float _rightX;

    /// <summary>
    /// Конструктор модели поля
    /// </summary>
    /// <param name="parY">Координата по вертикали (клетка)</param>
    /// <param name="parPreviousLevel">Предыдущая полоса препятствий</param>
    /// <param name="parEmpty">Флаг генерации поля без препятствий
    /// (true - в начальном состоянии поле генерируется без препятствий, false - в препятствиями)</param>
    public Field(GameField parGameField, float parY, Level parPreviousLevel, bool parEmpty = false)
      : base(parGameField, parY)
    {
      Barriers = new ObservableCollection<Barrier>();
      _leftX = 0;
      _rightX = parEmpty ? (GameField.AbsoluteY + GameField.Width) : 0 - 1;
      _previousField = (Field)(parPreviousLevel != null && parPreviousLevel is Field ? parPreviousLevel : null);
      if (!parEmpty)
      {
        float lastCoordinate = GameField.AbsoluteX + GameField.Width - 1 + ModelConfiguration.COORDINATE_EPS;
        for (float x = GameField.AbsoluteX; x < lastCoordinate; x++)
        {
          Barrier barrier = (Barrier)GenerateObject(true);
          if (barrier != null)
          {
            Barriers.Add(barrier);
          }
        }
      }
    }

    /// <summary>
    /// Генерирует препятствие на поле с некоторой вероятностью
    /// </summary>
    /// <param name="parGenerateOnRight">Флаг генерации препятствия справа от уже существующих (true), или слева (false)</param>
    /// <returns>Сгенерированное препятствие, либо null - отсутствие препятствия на координате</returns>
    protected override GameObject GenerateObject(bool parGenerateOnRight = true)
    {
      float x = parGenerateOnRight ? ++_rightX : --_leftX;
      int dx = (parGenerateOnRight ? -1 : 1) * ModelConfiguration.BARRIER_WIDTH;
      if (!(_previousField != null && !_previousField.HasObjectOnX(x)
        && _previousField.HasObjectOnX(x + dx) && HasObjectOnX(x + dx)))
      {
        bool isMaxSequenceOfBarriers = true;
        for (int i = 1; i <= ModelConfiguration.MAX_BARRIER_SEQUENCE; i++)
        {
          isMaxSequenceOfBarriers &= HasObjectOnX(x + dx * i);
        }
        if (!isMaxSequenceOfBarriers)
        {
          if (Random.NextDouble() < GameSettings.BarrierGenerationProbability)
          {
            return new Barrier(this, x);
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Проверяет, есть ли на координате по горизонтали статичное препятствие
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>true - если на заданной координате есть статичное препятствие, false - в противном случае</returns>
    public bool HasObjectOnX(float parX)
    {
      foreach (Barrier elBarrier in Barriers)
      {
        if (elBarrier.AbsoluteX > parX + ModelConfiguration.PLAYER_WIDTH - ModelConfiguration.COORDINATE_EPS)
        {
          break;
        }
        if (elBarrier.AbsoluteX + elBarrier.Width > parX + ModelConfiguration.COORDINATE_EPS)
        {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Изменяет состояние модели поля
    /// </summary>
    /// <param name="parTimeMilliseconds">Время, прошедшее с последнего перемещения (мс)</param>
    public override void ChangeState(long parTimeMilliseconds)
    {
      Move(parTimeMilliseconds);
    }

    /// <summary>
    /// Проверяет, возникает ли ситуация конца игры на координате по горизонтали
    /// </summary>
    /// <param name="parX">Координата по горизонтали (клетка)</param>
    /// <returns>true - если игра заканчивается на выбранной координате, false - в противном случае</returns>
    public override bool IsGameOverOnX(float parX)
    {
      return false;
    }

    /// <summary>
    /// Генерирует статичное препятствие на поле, если это необходимо
    /// (например, когда игрок переместился вправо/влево)
    /// </summary>
    public override void GenerateObjectIfNeed()
    {
      while (_leftX > GameField.AbsoluteX)
      {
        Barrier barrier = (Barrier)GenerateObject(false);
        if (barrier != null)
        {
          Barriers.Insert(0, barrier);
        }
      }
      while (_rightX + ModelConfiguration.BARRIER_WIDTH < GameField.AbsoluteX + GameField.Width)
      {
        Barrier barrier = (Barrier)GenerateObject(true);
        if (barrier != null)
        {
          Barriers.Add(barrier);
        }
      }
    }
  }
}