using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlinForms.Framework
{
    /// <summary>
    /// An implementation of <see cref="IHostedService"/> that controls the lifetime of a BlinForms application.
    /// When this service starts, it loads the main form registered by
    /// <see cref="BlinFormsServiceCollectionExtensions.AddBlinFormsMainForm{TComponent}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>.
    /// The service will request that the application stops when the main form is closed.
    /// This service will invoke all instances of <see cref="IBlinFormsStartup"/> that are registered in the
    /// container. The order of the startup instances is not guaranteed.
    /// </summary>
    public class BlinFormsHostedService : IHostedService
    {
        private readonly IBlinFormsMainFormType _blinFormsMainForm;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IEnumerable<IBlinFormsStartup> _blinFormsStartups;

        public BlinFormsHostedService(
            IBlinFormsMainFormType blinFormsMainForm,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime hostApplicationLifetime,
            IEnumerable<IBlinFormsStartup> blinFormsStartups)
        {
            _blinFormsMainForm = blinFormsMainForm;
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
            _blinFormsStartups = blinFormsStartups;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_blinFormsStartups != null)
            {
                await Task.WhenAll(_blinFormsStartups.Select(async startup => await startup.OnStartAsync()));
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var renderer = new BlinFormsRenderer(_serviceProvider, _loggerFactory);
            await renderer.Dispatcher.InvokeAsync(() =>
            {
                renderer.AddComponent(_blinFormsMainForm.MainFormType);
                var rootForm = renderer.RootForm;
                rootForm.FormClosed += OnRootFormFormClosed;
                Application.Run(rootForm);
            });
        }

        private void OnRootFormFormClosed(object sender, FormClosedEventArgs e)
        {
            // When the main form closes, request for the application to stop
            _hostApplicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
