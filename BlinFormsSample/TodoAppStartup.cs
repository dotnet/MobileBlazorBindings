using BlinForms.Framework;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BlinFormsSample
{
    public class TodoAppStartup : IBlinFormsStartup
    {
        private readonly AppState _appState;
        private readonly IConfiguration _configuration;

        public TodoAppStartup(AppState appState, IConfiguration configuration)
        {
            _appState = appState;
            _configuration = configuration;
        }

        public Task OnStartAsync()
        {
            if (_appState.IsEmptyAppState())
            {
                var createDefaultTodoItems = _configuration.GetValue<bool>("CreateDefaultTodoItems");
                _appState.ResetAppState(createDefaultTodoItems);
            }

            return Task.CompletedTask;
        }
    }
}
