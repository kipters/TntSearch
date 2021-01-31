using MvvmCross.Commands;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TntSearch.Core.Extensions
{
    public static class CommandExtensions
    {
        public static void TryExecute(this ICommand self)
        {
            if (self.CanExecute(null))
            {
                self.Execute(null);
            }
        }

        public static void TryExecute(this ICommand self, object parameter)
        {
            if (self.CanExecute(parameter))
            {
                self.Execute(parameter);
            }
        }

        public static void TryExecute<T>(this IMvxCommand<T> self, T parameter)
        {
            if (self.CanExecute(parameter))
            {
                self.Execute(parameter);
            }
        }

        public static async Task TryExecuteAsync(this IMvxAsyncCommand self)
        {
            if (self.CanExecute())
            {
                await self.ExecuteAsync();
            }
        }

        public static async Task TryExecuteAsync<T>(this IMvxAsyncCommand<T> self, T parameter)
        {
            if (self.CanExecute(parameter))
            {
                await self.ExecuteAsync(parameter);
            }
        }
    }
}
