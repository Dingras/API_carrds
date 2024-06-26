﻿using API_carrds.DataControllers;
using API_carrds.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_carrds.Controllers
{
    [ApiController]
    [Route("proyects")]
    public class ProyectController : ControllerBase
    {
        Proyects proyects = new Proyects();

        [HttpPost]
        [Route("add")]
        public string CreateProyect(Proyect p)
        {
            return proyects.Create(p);
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Proyect> ListProyect() 
        {
            return proyects.GetAll();
        }

        [HttpGet]
        [Route("proyect")]
        public Proyect GetProyect(int id)
        {
            return proyects.GetByID(id);
        }

        [HttpGet]
        [Route("user")]
        public IEnumerable<Proyect> GetProyectsByUser(int id)
        {
            return proyects.GetProyectsByUserID(id);
        }

        [HttpPut]
        [Route("update")]
        public string UpdateProyect(int id, Proyect p)
        {
            return proyects.Update(id,p);
        }
        
        [HttpDelete]
        [Route("delete")]
        public string DeleteProyect(int id)
        {
            return proyects.Delete(id);
        }

    }
}
