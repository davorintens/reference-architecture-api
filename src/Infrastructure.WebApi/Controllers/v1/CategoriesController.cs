namespace CompanyName.Notebook.NoteTaking.Infrastructure.WebApi.Controllers.v1
{
    using System;
    using System.Collections.Generic;
    using CompanyName.Notebook.NoteTaking.Core.Application.Messages;
    using CompanyName.Notebook.NoteTaking.Core.Application.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/v1/[controller]")]
    public class CategoriesController : Controller
    {
        private ILogger _logger;
        private readonly INoteTaker _noteTaker;

        public CategoriesController(
            INoteTaker noteTaker,
            ILogger<CategoriesController> logger) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _noteTaker = noteTaker ?? throw new ArgumentNullException(nameof(noteTaker));
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <response code="200">Categories found and returned</response>
        // GET api/v1/categories

        [HttpGet, Route("")]
        [ProducesResponseType(typeof(IList<CategoryDto>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult Get()
        {
            var categoryDtos =  _noteTaker.ListCategories();
            return Ok(categoryDtos);
        }

        /// <summary>
        /// Get the category identified by the specified ID.
        /// </summary>
        /// <param name="id">Category Identifier.</param>
        /// <response code="200">Category found and returned</response>
        // GET api/v1/categories/:id

        [HttpGet, Route("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult Get(Guid id)
        {
            var categoryDto = _noteTaker.GetCategoryDetail(id);
            return Ok(categoryDto);
        }

        /// <summary>
        /// Create new Category.
        /// </summary>
        /// <param name="newCategoryMessage">New Category Message.</param>
        /// <response code="200">Category created.</response>
        // POST api/v1/categories

        [HttpPost, Route("")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult Post([FromBody]NewCategoryMessage newCategoryMessage)
        {
            var categoryDto = _noteTaker.CreateNewCategory(newCategoryMessage);
            return CreatedAtAction("Get", new { id = categoryDto.Id }, categoryDto);
        }

        /// <summary>
        /// Change the Category name.
        /// </summary>
        /// <param name="updatedCategory">Category DTO with updated name.</param>
        /// <response code="200">Category updated.</response>
        // PUT api/v1/categories/:id

        [HttpPut, Route("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        public IActionResult Put(Guid id, [FromBody]CategoryDto updatedCategory)
        {
            var categoryDto = _noteTaker.RenameCategory(id, updatedCategory.Name);
            return CreatedAtAction("Get", new { id = categoryDto.Id }, categoryDto);
        }
    }
}