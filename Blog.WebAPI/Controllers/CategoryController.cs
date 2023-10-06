using Blog.DAL;
using Blog.DataEntities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data.Common;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using WebApplication15.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication15.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IRepository<CategoryEntity> categoryRepository;

        public CategoryController(IRepository<CategoryEntity> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<CategoryModel> Get(int id)
        {
            var entity = categoryRepository.FirstOrDefault(x=>x.Id == id);

            if (entity is not null)
            {
                return Ok(new CategoryModel { Id = 1, Name = "Mary" });
            }
            else
            {
                return BadRequest();
            }
        }


        // implementation is based on repository pattern
        [HttpGet("page")]
        [ProducesResponseType(typeof(OkResult), 200)]
        [ProducesResponseType(typeof(BadRequest), 400)]
        [ProducesResponseType(typeof(ProblemHttpResult), 500)]
        public ActionResult<CategoryModel> GetPage([FromQuery] int? num = 0, [FromQuery] int? size = 10)
        {
            if (num is null)
            {
                return BadRequest($"no {nameof(num)} in query");
            }
            if (size is null)
            {
                return BadRequest($"no {nameof(size)} in query");
            }
            if (size == default(int))
            {
                return BadRequest($"{nameof(size)} cannot be null");
            }

            try
            {
                return Ok(new PageModel<CategoryModel>
                {
                    PageNum = num ?? 0,
                    Count = categoryRepository.Count() / size.Value,
                    PageSize = size.Value,
                    Items = categoryRepository
                        .GetPage(x => true, num!.Value, size.Value)
                        .Select(x => new CategoryModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        })
                });
            }
            catch(DbException e)
            {
                // TODO add logging here
                return this.Problem("request to database is failed");
            }
            catch (InvalidOperationException e)
            {
                // TODO add logging here
                return this.Problem("connection to database is lost");
            }
        }

        // here a typed DbContext is used as UnitOfWork object
        [HttpPost("create/{name}")]
        public ActionResult<CategoryModel> Create(string name, StoreDbContext dbContext)
        {
            try
            {
                var entity = new CategoryEntity { Name = name };
                dbContext.Categories.Add(entity);
                dbContext.SaveChanges();
                return Ok(new CategoryModel { Id = entity.Id, Name = entity.Name });
            }
            catch 
            {
                return BadRequest();
            }
        }

    }
}
