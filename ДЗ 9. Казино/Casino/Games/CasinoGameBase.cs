using System;

namespace CasinoGame.Games
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnDraw;

        protected abstract void FactoryMethod();

        public abstract void PlayGame();

        protected void OnWinInvoke() => OnWin?.Invoke();
        protected void OnLoseInvoke() => OnLose?.Invoke();
        protected void OnDrawInvoke() => OnDraw?.Invoke();
    }
}

