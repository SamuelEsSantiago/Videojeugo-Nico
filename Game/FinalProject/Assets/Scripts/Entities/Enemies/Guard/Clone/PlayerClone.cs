using UnityEngine;
using System.Collections;
public abstract class PlayerClone : Enemy
{
    [Header("Self Additions")]
    [SerializeField] protected float delay;
    private float curTime;

    #region CloningStuff
    protected Queue cloners;
    protected Cloner currentCloner;
    protected class Cloner
    {
        public Vector3 position;
        public Quaternion rotation;
        public AnimationState animation;
    }
    protected enum AnimationState
    {
        Idle, 
        Walk, 
        Jump, 
        Run, 
        Fall,
        Fly
    }
    #endregion

    protected abstract void CloneMovement();

    new void Awake()
    {
        base.Awake();
        cloners = new Queue();
        itemInteractionManager.entity = this;
    }

    new void Start()
    {
        base.Start();
    }
    
    new void FixedUpdate()
    {
        // Every frame, a Cloner is added to the Queue, storing position, rotation and animation state from player current state
        cloners.Enqueue( new Cloner { position = player.GetPosition(), rotation = player.transform.rotation, animation = GetAnimationState() }  );
        if (curTime > delay)
        {
            // Takes a Cloner out of the Queue, updating the currentCloner
            currentCloner = (Cloner)cloners.Dequeue();
            CloneMovement();
            SetAnimation(currentCloner.animation);
        }
        else
        {
            curTime += Time.deltaTime;
        }
        base.FixedUpdate();
    }

    /// <summary>
    /// Gets the player current animation state
    /// </summary>
    /// <returns></returns>
    protected AnimationState GetAnimationState()
    {
        if (player.isWalking)
        {
            return AnimationState.Walk;
        }
        else if (player.isJumping)
        {
            return AnimationState.Jump;
        }
        else if (player.isFalling)
        {
            return AnimationState.Fall;
        }
        else if (player.isRunning)
        {
            return AnimationState.Run;
        }
        else if (player.isFlying)
        {
            return AnimationState.Fly;
        }
        else
        {
            return AnimationState.Idle;
        }
    }

    /// <summary>
    /// Updates current state so the upper (Entity) class does the animations
    /// </summary>
    /// <param name="animationState"></param>
    protected void SetAnimation(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                break;
            case AnimationState.Walk:
                isWalking = true;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Jump:
                isWalking = false;
                isJumping = true;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Run:
                isWalking = true;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = false;
                break;
            case AnimationState.Fall:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = true;
                isFlying = false;
                break;
            case AnimationState.Fly:
                isWalking = false;
                isJumping = false;
                isRunning = false;
                isFalling = false;
                isFlying = true;
                break;
        }
    }
    
}