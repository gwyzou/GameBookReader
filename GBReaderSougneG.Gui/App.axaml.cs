using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderSougneG.Avalonia.Views;
using GBReaderSougneG.Presentation;
using GBReaderSougneG.Presentation.Switch;
using GBReaderSougneG.Repositories;
using GBReaderSougneG.Repository;
using GBReaderSougnG.Domains;


namespace GBReaderSougneG.Avalonia
{
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _mainWindow = new MainWindow();
                desktop.MainWindow = _mainWindow;
                CreateStructure();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void CreateStructure()
        {
            var repo = new RepositoryFactory().GetRepo(RepoType.Mysql,RepoType.Json);
            var game = new CommunicationPresenter();
            
            
            MainViewAndPresenter(repo, game);

            GameViewAndPresenter(repo, game);

            EndViewAndPresenter(repo, game);

            HistoryViewAndPresenter(repo, game);
        }

        private void HistoryViewAndPresenter(IGeneralRepo repo, CommunicationPresenter game)
        {
            var historyView = new HistoryView();
            _mainWindow.RegisterPage(SwitchNames.HistoryPage, historyView);
            HistoryPresenter(repo, game, historyView);
        }

        private void HistoryPresenter(IGeneralRepo repo, CommunicationPresenter game, HistoryView historyView)
        {
            var historyViewPresenter = new HistoryViewPresenter(historyView, _mainWindow, _mainWindow, repo, game);
        }

        private void EndViewAndPresenter(IGeneralRepo repo, CommunicationPresenter game)
        {
            var endView = new EndView();
            _mainWindow.RegisterPage(SwitchNames.EndPage, endView);
            EndPresenter(repo, game, endView);
        }

        private void EndPresenter(IGeneralRepo repo, CommunicationPresenter game, EndView endView)
        {
            var endViewPresenter = new EndViewPresenter(endView, _mainWindow, _mainWindow, repo, game);
        }

        private void GameViewAndPresenter(IGeneralRepo repo, CommunicationPresenter game)
        {
            var gameView = new GameView();
            _mainWindow.RegisterPage(SwitchNames.GameBook, gameView);
            GamePresenter(repo, game, gameView);
        }

        private void GamePresenter(IGeneralRepo repo, CommunicationPresenter game, GameView gameView)
        {
            var gameViewPresenter = new GameViewPresenter(gameView, _mainWindow, _mainWindow, repo, game);
        }

        private void MainViewAndPresenter(IGeneralRepo repo, CommunicationPresenter game)
        {
            var mainView = new MainView();
            _mainWindow.RegisterPage(SwitchNames.StartPage, mainView);
            MainPresenter(repo, game, mainView);
        }

        private void MainPresenter(IGeneralRepo repo, CommunicationPresenter game, MainView mainView)
        {
            var mainViewPresenter = new MainViewPresenter(mainView, _mainWindow, _mainWindow, repo, game);
        }
    }
}