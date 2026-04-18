/// <summary>
/// お餅に味付けの機能を持たせるためのインタフェース
/// </summary>
public interface IRicecakeFlavoring
{
    /// <summary>
    /// このオブジェクトが持つ味付けの種類
    /// </summary>
    RicecakeType MyType { get; }
}
