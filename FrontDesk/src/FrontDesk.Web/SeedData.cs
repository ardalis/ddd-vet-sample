using FrontDesk.Core.Entities;
using FrontDesk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FrontDesk.Web
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
        }
    }
}
