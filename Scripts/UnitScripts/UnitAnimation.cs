using UnityEngine;

[RequireComponent(typeof(Animator))]

public class UnitAnimation : MonoBehaviour
{
    private const string IsMove = nameof(IsMove);
    private const string Processing = nameof(Processing);

    private Animator _animator;

    [field: SerializeField] public AnimationClip ProcessingAnimationDuration { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(bool isMove)
    {
        _animator.SetBool(IsMove, isMove);
    }
    
    public void PlayProcessing()
    {
        _animator.SetTrigger(Processing);
    }
}