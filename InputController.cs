using UnityEngine;

public abstract class InputController : ScriptableObject
{
    /// <summary>
    /// ������ ����� ��������
    /// </summary>
    /// <returns></returns>
    public abstract float RetrieveMoveInput(GameObject gameObject);

    /// <summary>
    /// ������ ����� ��������� ������
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveJumpInput(GameObject gameObject);

    /// <summary>
    /// ������ �����  �������� ������
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveJumpHoldInput(GameObject gameObject);

    /// <summary>
    /// ������ ����� ����
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveDashInput(GameObject gameObject);

    /// <summary>
    /// ������ ����� �����
    /// </summary>
    /// <returns></returns>
    public abstract bool RetrieveAttackInput(GameObject gameObject);
}
