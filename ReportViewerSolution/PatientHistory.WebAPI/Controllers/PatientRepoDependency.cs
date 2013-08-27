using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Repository;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace WebAPI.Controllers
{
     class PatientsNotesContainer:IDependencyResolver
      {
        static readonly IPatientRepository _repo = new PatientRepository();

        public IDependencyScope BeginScope()
        {
          return this;
        }

        public object GetService(Type serviceType)
        {
          if (serviceType == typeof(PatientNotesController))
          {
            return new PatientNotesController(_repo);
          }
          else
          {
            return null;
          }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
          return new List<object>();
        }

        public void Dispose(){}
      }

    }
