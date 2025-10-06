using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOP_Maiu;

namespace OOP_Maiu
{
    public class Game
    {
        public Player CurrentPlayer { get; private set; }
        public Theme CurrentTheme { get; private set; }
        public double DurationMs { get; private set; }

        public event Action<string>? OnShowSymbol;
        public event Action? OnHideSymbol;
        public event Action? OnGameFinished;

        private bool isRunning;
        private Random rng = new Random();

        public Game(Player player, Theme theme, double durationMs)
        {
            CurrentPlayer = player;
            CurrentTheme = theme;
            DurationMs = durationMs;
        }

        public async void Start()
        {
            isRunning = true;
            var start = DateTime.Now;

            while (isRunning && (DateTime.Now - start).TotalMilliseconds < DurationMs)
            {
                OnShowSymbol?.Invoke(CurrentPlayer.Symbol);

                // sümbol on nähtav pool sekundit
                await Task.Delay(500);
                OnHideSymbol?.Invoke();

                // juhuslik paus 0.5–2 sek
                int pause = rng.Next(500, 2000);
                await Task.Delay(pause);
            }

            isRunning = false;
            OnGameFinished?.Invoke();
        }

        public void Stop() => isRunning = false;
    }
}
