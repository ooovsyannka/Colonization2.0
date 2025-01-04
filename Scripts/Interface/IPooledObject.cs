using System;

public interface IPooledObject
{
    public event Action<IPooledObject> Died;

    public void Die();
}
