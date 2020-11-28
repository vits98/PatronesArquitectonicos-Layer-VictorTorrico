using LibreriaArqui.Exceptions;
using LibreriaArqui.Models;
using LibreriaArqui.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaArqui.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private IAuthorService authorsService;

        public AuthorController(IAuthorService authorsService)
        {
            this.authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> Get(string orderBy = "Id", bool showBooks = false)
        {
            try
            {
                return Ok(await authorsService.GetAuthorsAsync(orderBy, showBooks));
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");

            }
        }

        [HttpGet("{authorId:int}")]
        public async Task<ActionResult<Author>> Get(int authorId, bool showBooks = false)
        {
            try
            {
                var author = await this.authorsService.GetAuthorAsync(authorId, showBooks);
                return Ok(author);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Post([FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postedAuthor = await this.authorsService.AddAuthorAsync(author);
            return Created($"/api/authors/{postedAuthor.Id}", postedAuthor);
        }

        [HttpDelete("{authorId:int}")]
        public async Task<ActionResult<bool>> Delete(int authorId)
        {
            try
            {
                return Ok(await this.authorsService.DeleteAuthorAsync(authorId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<Author>> Update(int authorId, [FromBody] Author author)
        {
            if (!ModelState.IsValid)
            {
                foreach (var pair in ModelState)
                {
                    if (pair.Key == nameof(author.Nationallity) && pair.Value.Errors.Count > 0)
                    {
                        return BadRequest(pair.Value.Errors);
                    }
                }
            }

            try
            {
                var authorUpdated = await this.authorsService.UpdateAuthorAsync(authorId, author);
                return Ok(authorUpdated);
            }
            catch (System.InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Something bad happened: {ex.Message}");
            }
        }
    }
}
