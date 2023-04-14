using UnityEngine;

public abstract class InputController : ScriptableObject
{
    /// <summary>
    /// Захват ввода движений
    /// </summary>
    /// <returns></returns>
    public abstract float RetrieveMoveInput(GameObject gameObject);

    /// <summary>
    /// Захват ввода короткого прыжка
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveJumpInput(GameObject gameObject);

    /// <summary>
    /// Захват ввода  длинного прыжка
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveJumpHoldInput(GameObject gameObject);

    /// <summary>
    /// Захват ввода дэша
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveDashInput(GameObject gameObject);

    /// <summary>
    /// Захват ввода атаки
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveAttackInput(GameObject gameObject);
}
