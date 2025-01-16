using System;

public interface IPoolableObject
{
    public event Action<IPoolableObject> Died;

    public void Die();
}
