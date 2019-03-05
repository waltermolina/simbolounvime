using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using UNViMeSimbolo.Models;

namespace UNViMeSimbolo.Controllers
{
    public class EslogansController : ApiController
    {
        private UnvimeEscudoEntities db = new UnvimeEscudoEntities();

        // GET: api/Eslogans
        public IQueryable<Eslogan> GetEslogan()
        {
            return db.Eslogan;
        }

        // GET: api/Eslogans/5
        [ResponseType(typeof(Eslogan))]
        public IHttpActionResult GetEslogan(int id)
        {
            Eslogan eslogan = db.Eslogan.Find(id);
            if (eslogan == null)
            {
                return NotFound();
            }

            return Ok(eslogan);
        }

        public IEnumerable<GetCountTopFiveEslogans_Result> GetTopTenEslogans()
        {
            var queryResults = db.GetCountTopFiveEslogans();
            var Eslogans = queryResults.Select(eslogans => new GetCountTopFiveEslogans_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,
                NumberWords = eslogans.NumberWords

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetCountTopAzul_Result> GetTopFiveAzul()
        {
            var queryResults = db.GetCountTopAzul();
            var Eslogans = queryResults.Select(eslogans => new GetCountTopAzul_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,
                NumberWords = eslogans.NumberWords,
                Equipo = eslogans.Equipo

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetCountTopCeleste_Result> GetTopFiveCeleste()
        {
            var queryResults = db.GetCountTopCeleste();
            var Eslogans = queryResults.Select(eslogans => new GetCountTopCeleste_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,
                NumberWords = eslogans.NumberWords,
                Equipo = eslogans.Equipo

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetCountTopNaranja_Result> GetTopFiveNaranja()
        {
            var queryResults = db.GetCountTopNaranja();
            var Eslogans = queryResults.Select(eslogans => new GetCountTopNaranja_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,
                NumberWords = eslogans.NumberWords,
                Equipo = eslogans.Equipo

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetCountTopVerde_Result> GetTopFiveVerde()
        {
            var queryResults = db.GetCountTopVerde();
            var Eslogans = queryResults.Select(eslogans => new GetCountTopVerde_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,
                NumberWords = eslogans.NumberWords,
                Equipo = eslogans.Equipo

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetAllWords_Result> GetAllWords()
        {
            var queryResults = db.GetAllWords();
            var Eslogans = queryResults.Select(eslogans => new GetAllWords_Result
            {
                IdOutput = eslogans.IdOutput,
                Words = eslogans.Words,

            }).ToList();

            return Eslogans;
        }

        public IEnumerable<GetAllCountsForTeam_Result> GetAllCountsForEveryTeam()
        {
            var queryResults = db.GetAllCountsForTeam();
            var Eslogans = queryResults.Select(eslogans => new GetAllCountsForTeam_Result
            {
                IdOutput = eslogans.IdOutput,
                Numbers = eslogans.Numbers,
                Team = eslogans.Team

            }).ToList();

            return Eslogans;
        }

        public IHttpActionResult GetEsloganCount()
        {
            List<Eslogan> eslogan = db.Eslogan.ToList();
            int numberOfEslogans = eslogan.Count();
            return CreatedAtRoute("DefaultApi", new { id = numberOfEslogans }, numberOfEslogans);
        }

        // POST: api/Eslogans
        [ResponseType(typeof(Eslogan))]
        public IHttpActionResult PostEslogan(Eslogan eslogan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            eslogan.WordChooses.ToLower();
            string[] tokens = eslogan.WordChooses.Split(' ');
            foreach (string t in tokens)
            {
                eslogan.WordChooses = t;
                db.Eslogan.Add(eslogan);
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.Accepted);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EsloganExists(int id)
        {
            return db.Eslogan.Count(e => e.Id == id) > 0;
        }
    }
}