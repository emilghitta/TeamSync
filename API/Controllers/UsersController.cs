using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamSyncUser>>> GetUsers()
        {

            return await _context.Users.ToListAsync(); //returneaza lista de completa de users din tabel

        }

        //cand cineva loveste enpoint-ul poate sa faca api/Users/3 si atunci id-ul ala se trimite mai departe catre db si returneaza user-ul cu id-ul respectiv
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamSyncUser>> GetUser(int id)
        { //preia ca parametru id-ul venit 

            return await _context.Users.FindAsync(id); // Gaseste o entitate bazat pe un primary key
        }
    }
}

//Nu e scalabil fiindca thread-ul nostru se opreste pana ce query-urile au avut loc! Asa ca nu vrem sa ramanem blocati daca avem un query mai complex.
//Intr-un web server modern sun multi threaded. 

// Indiferent cate thread-uri avem. Daca avem 100 de useri putem sa servim doar 100 de useri pe 100 de thread-uri disponibile. 


// Putem sa facem codul nostru asynchronus. Cand ne vine un request de API main thread-ul o sa paseze la un alt thread request-ul ala. si dupa la altu (daca mai vine unu, si tot asa)

// Tot timpul daca face codul care se conecteaza cu db-ul trebuie sa fie asyncronus 