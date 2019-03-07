using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Domain.Exceptions;
using VacationsTracker.Core.Exceptions;
//using VacationsTracker.Core.Exceptions;
using VacationsTracker.Core.Navigation;
using VacationsTracker.Core.Operations;
using VacationsTracker.Core.Resourses;
//using VacationsTracker.Core.Operations;
//using VacationsTracker.Core.Resources;

namespace VacationsTracker.Core.Presentation.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase//, Operations.IViewModelWithOperation
    {
        private readonly INavigationService _navigationService;
        private readonly IUserRepository _userRepository;
        private bool _errorVisibility;
        private string _login;
        private string _password;
        private string _errorMessage;

        public ICommand LoginCommand => CommandProvider.GetForAsync(OnLogin);

        public LoginViewModel(INavigationService navigationService, IUserRepository userRepository, IOperationFactory operationFactory) : base(operationFactory)
        {
            _navigationService = navigationService;
            _userRepository = userRepository;
        }

        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public bool ErrorVisibility
        {
            get => _errorVisibility;
            set => Set(ref _errorVisibility, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => Set(ref _errorMessage, value);
        }

        private Task OnLogin()
        {
            ErrorVisibility = false;

             return OperationFactory
                .CreateOperation(OperationContext)
                //.WithLoadingNotification()
                .WithInternetConnectionCondition()
                .WithExpressionAsync(token =>
                {
                    var user = new User(Login, Password);
                    user.ValidateCredentials();
                    return _userRepository.AuthorizeAsync(user, token);
                })
                .OnSuccess(() => _navigationService.NavigateToMainList(this))
                .OnError<AuthenticationException>(_ => SetError(Strings.LoginPage_InvalidCredentials))
                .OnError<EmptyPasswordException>(_ => SetError(Strings.LoginPage_InvalidPassword))
                .OnError<EmptyLoginException>(_ => SetError(Strings.LoginPage_InvalidLogin))
                .OnError<InternetConnectionException>(_ => SetError(Strings.LoginPage_NoInternet))
                .OnError<Exception>(
                    error =>
                    {
                        SetError(Strings.LoginPage_UnknownError);
                    })
                .ExecuteAsync();
        }

        private void SetError(string errorText)
        {
            ErrorVisibility = true;
            ErrorMessage = errorText;
        }
    }
}
