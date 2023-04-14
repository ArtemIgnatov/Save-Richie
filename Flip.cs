using UnityEngine;

[RequireComponent(typeof(Controller), typeof(CollisionDataRetriever))]
public class Flip : MonoBehaviour
{
    private Controller _controller;
    private bool _facingRight = true;
    private Vector2 _direction;
    private CollisionDataRetriever _ground;
    Vector3 currentScale;
    private float _wallDirectionX;


    private void Awake()
    {
        _controller = GetComponent<Controller>();
        _ground = GetComponent<CollisionDataRetriever>();
    }

    void Update()
    {
        if (_controller.input != null)
            _direction.x = _controller.input.RetrieveMoveInput(this.gameObject);
    }

    private void FixedUpdate()
    {
        _wallDirectionX = _ground.ContactNormal.x;

        //Меняем ориентацию спрайта в зависимости от направления движения
        if (_direction.x > 0f && !_facingRight) Flipping();
        else if (_direction.x < 0 && _facingRight) Flipping();

        //Меняем ориентацию спрайта в зависимости от того с какой стороны находится стена
        if (_ground.OnWall)
        {
            if (_wallDirectionX < 0f && !_facingRight) Flipping();
            else if (_wallDirectionX > 0 && _facingRight) Flipping();
        }
    }

    private void Flipping()
    {
        currentScale = this.gameObject.transform.localScale;
        currentScale.x *= -1;
        this.gameObject.transform.localScale = currentScale;
        _facingRight = !_facingRight;
    }
}
