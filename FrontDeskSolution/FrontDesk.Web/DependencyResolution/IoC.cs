// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using AppointmentScheduling.Core.Interfaces;
using AppointmentScheduling.Data;
using AppointmentScheduling.Data.Repositories;
using AppointmentScheduling.Data.Services;
using ClientPatientManagement.Core.Interfaces;
using ClientPatientManagement.Data;
using FrontDesk.SharedKernel.Interfaces;
using FrontDesk.Web.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Owin;
using StructureMap;

namespace FrontDesk.Web.DependencyResolution
{
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.AssemblyContainingType<IScheduleRepository>();
                                        scan.AssemblyContainingType<SchedulingContext>();
                                        scan.AssemblyContainingType<ScheduleRepository>();
                                        scan.AssemblyContainingType<CrudContext>();
                                        scan.WithDefaultConventions();
                                        scan.ConnectImplementationsToTypesClosing(typeof(ClientPatientManagement.Core.Interfaces.IRepository<>));
                                        scan.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
                                    });
                            // Examples:
                            // x.For<IExample>().Use<Example>();
                            // Hybrid per-request scoping
                            //x.For(typeof(IRepository<>)).HybridHttpOrThreadLocalScoped().Use(typeof(GenericRepository<>));
                            //x.For<DataContext>().HybridHttpOrThreadLocalScoped().Use(() => new DataContext());

                            // Wire up Entity Framework and Repositories using Http Request Scoping
                            x.For<IScheduleRepository>().HybridHttpOrThreadLocalScoped().Use<ScheduleRepository>();
                            x.For<SchedulingContext>().HybridHttpOrThreadLocalScoped().Use(() => new SchedulingContext());

                            // Wire up CRUD context
                            x.For(typeof(IRepository<>)).HybridHttpOrThreadLocalScoped().Use(typeof(Repository<>));
                            x.For<CrudContext>().HybridHttpOrThreadLocalScoped().Use(() => new CrudContext());

                            // Wire up application settings
                            x.For<IApplicationSettings>().Singleton().Use(() => new OfficeSettings());

                            // Wire up Message Publisher
                            x.For<IMessagePublisher>().Use<ServiceBrokerMessagePublisher>();

                            // SignalR Settings
                            //x.For<IDependencyResolver>().Add<SignalRSmDependencyResolver>();
                            //x.For<IConnectionManager>().Use(() => AspNetHost.DependencyResolver.Resolve<IConnectionManager>());
                            //x.For<IHubConnectionContext>().Use(() => )
                        //    x.For<IHubConnectionContext>()
                        //        .ConditionallyUse(c => c.If(t => t.ParentType.GetInterface(typeof(Microsoft.AspNet.SignalR.Sc.IStockTicker).Name) ==
                        //typeof(Microsoft.AspNet.SignalR.StockTicker.IStockTicker))
                        //        .ThenIt.Is.ConstructedBy(
                        //    () => resolver.Resolve<IConnectionManager>().GetHubContext<ScheduleHub>().Clients)
                    //);
                        });
            return ObjectFactory.Container;
        }
    }
}