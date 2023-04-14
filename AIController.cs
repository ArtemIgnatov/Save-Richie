using UnityEngine;

[CreateAssetMenu(fileName = "AIController", menuName = "inputeController/AIController")]
public class AIController : InputController

{
    [Header("Interaction")]
    [SerializeField] private LayerMask _layerMask = -1;
    [SerializeField] private LayerMask _playerLayerMask = 0;
    [Header("Ray")]
    [SerializeField] private float _bottomDistance = 1f;
    [SerializeField] private float _topDistance = 1f;
    [SerializeField] private float _xOffset = 1f;
    [SerializeField] private float _attackRange = 5f;
    private bool _desiredAttack = false;

    private RaycastHit2D _groundInfoBottom;
    private RaycastHit2D _groundInfoTop;
    private RaycastHit2D _playerDetector;

    public override bool RetrieveJumpInput(GameObject gameObject)
    {
        return true;
    }

    public override bool RetrieveJumpHoldInput(GameObject gameObject)
    {
        return false;
    }

    public override float RetrieveMoveInput(GameObject gameObject)
    {
        // Определяем достиг ли противник края платформы
        _groundInfoBottom = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x),
            gameObject.transform.position.y), Vector2.down, _bottomDistance, _layerMask);

        _groundInfoTop = Physics2D.Raycast(new Vector2(gameObject.transform.position.x + (_xOffset * gameObject.transform.localScale.x),
            gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, _topDistance, _layerMask);

        //Меняем направление движения, при достижении края платформы
        if (_groundInfoTop.collider == true || _groundInfoBottom.collider == false)
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

        return gameObject.transform.localScale.x;
    }

    public override bool RetrieveDashInput(GameObject gameObject)
    {
        return false;
    }

    public override bool RetrieveAttackInput(GameObject gameObject)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Определяем растояние до противника
            float distanceToPlayer = Vector2.Distance(gameObject.transform.position, player.transform.position);

            //Определяем находится ли игрок перед противником
            bool _playerDetector = Physics2D.Raycast(new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y), Vector2.right * gameObject.transform.localScale.x, _attackRange, _playerLayerMask).collider;

            //Определяем жив ли игрок
            bool isPlayerDead = player.TryGetComponent(out Player component) && component.IsDead;

            if (!isPlayerDead && _playerDetector)
                _desiredAttack = distanceToPlayer <= _attackRange;
            else
                return false;
        }
        return _desiredAttack;
    }
}
