using Learning.DAL.Generation.Mvc;
using Learning.DAL.Models;
using Learning.DAL.Models.AdventureWorksModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learning.DAL.RoslynGeneration
{
    public class TestController : BaseRoslynController<BusinessEntity>
    {
        public TestController(AdventureWorksContext context) : base(context)
        {
        }
    }
}
