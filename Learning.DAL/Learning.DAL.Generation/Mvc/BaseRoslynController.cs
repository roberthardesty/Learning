using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DAL.Generation.Mvc
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseRoslynController<T> : ControllerBase where T : class
    {
        private DbContext _context;
        public BaseRoslynController(DbContext context)
        {
            _context = context;
        }

        [ODataRoute("({key})")]
        public async Task<IActionResult> GetEntity(Guid key)
        {
            var entity = await _context.FindAsync<T>(key);
            return entity == null ? (IActionResult)NotFound() : new ObjectResult(entity);
        }
    }
}
