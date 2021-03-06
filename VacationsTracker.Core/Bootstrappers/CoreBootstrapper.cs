﻿using FlexiMvvm;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using FlexiMvvm.Operations;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Infrastructure;
using VacationsTracker.Core.Infrastructure.Connectivity;
using VacationsTracker.Core.Operations;
using VacationsTracker.Core.Presentation;
using Connectivity = VacationsTracker.Core.Infrastructure.Connectivity.Connectivity;

namespace VacationsTracker.Core.Bootstrappers
{
    public class CoreBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            SetupDependencies(simpleIoc);
            SetupViewModelLocator(simpleIoc);
        }

        private void SetupDependencies(ISimpleIoc simpleIoc)
        {
            simpleIoc.Register<IErrorHandler>(() => new ExceptionHandler(), Reuse.Singleton);

            simpleIoc.Register<IConnectivity>(() => new Connectivity(), Reuse.Singleton);

            simpleIoc.Register<IDbService>(() => new DbService(), Reuse.Singleton);

            simpleIoc.Register<ISecureStorage>(() => new CustomSecureStorage(), Reuse.Singleton);

            simpleIoc.Register<IVacationApi>(
                () => new VacationsApi(
                    simpleIoc.Get<ISecureStorage>(),
                    simpleIoc.Get<IConnectivity>()),
                Reuse.Singleton);

            simpleIoc.Register<IVacationRepository>(
                () => new VacationsRepository(
                    simpleIoc.Get<IVacationApi>()),
                Reuse.Singleton);

            simpleIoc.Register<ISynchronizationService>(
                () => new SynchronizationService(
                    simpleIoc.Get<IDbService>(),
                    simpleIoc.Get<IVacationRepository>(),
                    simpleIoc.Get<IConnectivity>()),
                Reuse.Singleton);

            simpleIoc.Register<IUserRepository>(
                () => new UserRepository(
                    simpleIoc.Get<ISecureStorage>(),
                    simpleIoc.Get<IVacationApi>()));

            simpleIoc.Register<IDependencyProvider>(() => new DependencyProvider(simpleIoc.Get<IConnectivity>()));

            simpleIoc.Register<IOperationFactory>(() => new OperationFactory(
                simpleIoc.Get<IDependencyProvider>(),
                simpleIoc.Get<IErrorHandler>()));
        }

        private void SetupViewModelLocator(IDependencyProvider dependencyProvider)
        {
            ViewModelLocator.SetLocator(new VacationsTrackerViewModelLocator(dependencyProvider));
        }
    }
}
