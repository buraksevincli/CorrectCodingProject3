namespace GameFolders.Scripts.Abstracts.Combats
{
    public interface IHealth : ITakeHit
    {
        bool IsDead { get; }

        void Heal(int lifeCount);

        event System.Action<int, int> OnHealthChanged;

        event System.Action OnDead;
    }
}
