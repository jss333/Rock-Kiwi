using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowEnemy : Enemy
{
    public MeowIdleState meowIdleState { get; private set; }

    public MeowTraverseState meowTraverseState { get; private set; }

    public MeowAlertedState meowAlertedState { get; private set; }

    [SerializeField] private GameObject ball;

    [SerializeField] private Transform ballSpawn;

    [SerializeField] private float shootForce;

    [SerializeField] private float newPointRadius;

    [SerializeField] private LayerMask groundLayerMask;

    public GameObject BallProjectile => ball;

    public float ShootForce => shootForce;
    public float NewPointRadius => newPointRadius;
    public LayerMask GroundLayerMask => groundLayerMask;


    protected override void Awake()
    {
        base.Awake();

        meowIdleState = new MeowIdleState(this, stateMachine, "Idle", this, Color.white);
        meowAlertedState = new MeowAlertedState(this, stateMachine, "Traverse", this, Color.yellow);
        meowTraverseState = new MeowTraverseState(this, stateMachine, "Traverse", this, Color.red);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(meowIdleState);

        Ball.OnBallDisappear += Traverse;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        Ball.OnBallDisappear -= Traverse;
    }

    public void ShootBall()
    {
        Ball existingBall = FindAnyObjectByType<Ball>();
        if (existingBall != null) return;

        GameObject ballInstance = Instantiate(BallProjectile, ballSpawn.position, ballSpawn.rotation);

        Rigidbody2D rb = ballInstance.GetComponent<Rigidbody2D>();
        if(rb != null )
        {
            PlayerMovement playerMovement = FindAnyObjectByType<PlayerMovement>();
            if (playerMovement != null)
            {
                rb.AddForce((ballSpawn.right - playerMovement.transform.position).normalized * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    public void Traverse(Vector2 newPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(newPosition, Vector2.down, Mathf.Infinity, GroundLayerMask);
        if (hit.collider != null)
        {
            transform.position = hit.point;
        }
        else
        {
            Debug.Log("Hit nothing.");
        }
    }

    public void MoveTo(Vector2 newPosition)
    {
        transform.Translate(newPosition);
    }
}
