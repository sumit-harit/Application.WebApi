using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.WebApi.Application.Interfaces;
using Application.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math;

namespace Application.WebApi.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        IGenericRepositoryAsync<Product> _Repo;

        public testController(IGenericRepositoryAsync<Product> repo)
        {
            _Repo = repo;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _Repo.GetAllAsync());
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _Repo.Get(X => X.Id == id));

        }



    }
}
