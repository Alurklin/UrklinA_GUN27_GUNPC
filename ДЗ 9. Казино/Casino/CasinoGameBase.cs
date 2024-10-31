using System;

namespace Casino
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLoose;
        public event Action OnDraw;

        protected abstract void FactoryMethod();

        public abstract void PlayGame();

        protected void OnWinInvoke() => OnWin?.Invoke();
        protected void OnLooseInvoke() => OnLoose?.Invoke();
        protected void OnDrawInvoke() => OnDraw?.Invoke();
    }
}
