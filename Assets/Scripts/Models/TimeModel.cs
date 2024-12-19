using System;
using System.Threading;
using Core.Utils;
using Cysharp.Threading.Tasks;
using Models.Interfaces;
using UniRx;

namespace Models
{
    public sealed class TimeModel : ITimeModel, IDisposable
    {
        private readonly ReactiveProperty<int> _gameTime = new();
        public IObservable<int> GameTime => _gameTime;

        private CancellationTokenSource _cancellationTokenSource;

        public void Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CountTime(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask CountTime(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await UniTask.Delay(NumericConstants.One * 1000, cancellationToken: cancellationToken);
                    _gameTime.Value++;
                }
                catch (OperationCanceledException)
                {
                    // Операция отменена — прерываем цикл
                    break;
                }
            }
        }

        public void Dispose()
        {
            // Отмена асинхронной операции
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;

            // Освобождение ресурсов ReactiveProperty
            _gameTime.Dispose();
        }
    }
}
