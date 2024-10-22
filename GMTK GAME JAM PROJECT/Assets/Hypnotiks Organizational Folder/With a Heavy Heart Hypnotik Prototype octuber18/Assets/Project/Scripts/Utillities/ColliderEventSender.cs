using System;
using UnityEngine;

public class ColliderEventSender : MonoBehaviour
{
    public event EventHandler<ColliderEventArgs> OnTriggerEnter;
    public event EventHandler<ColliderEventArgs> OnTriggerExit;

    public event EventHandler<ColliderEventArgs> OnCollisionEnter;
    public event EventHandler<ColliderEventArgs> OnCollisionExit;

    public class ColliderEventArgs : EventArgs
    {
        public GameObject collidedGameObject;
    }

    #region TRIGGER
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(this, new ColliderEventArgs { collidedGameObject = collision.gameObject });
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(this, new ColliderEventArgs { collidedGameObject = collision.gameObject });
    }
    #endregion

    #region COLLISION
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(this, new ColliderEventArgs { collidedGameObject = collision.gameObject });
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(this, new ColliderEventArgs { collidedGameObject = collision.gameObject });
    }
    #endregion
}
