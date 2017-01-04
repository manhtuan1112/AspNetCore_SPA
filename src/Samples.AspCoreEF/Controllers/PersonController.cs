using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Samples.AspCoreEF.DAL.EF.Models;
using Samples.AspCoreEF.Models;
using Samples.AspCoreEF.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Samples.AspCoreEF.Controllers
{
    public class PersonController : Controller
    {
        private IMapper _mapper;
        private IPersonService _personService;

        public PersonController(IPersonService personService,IMapper mapper)
        {
            this._mapper = mapper;
            this._personService = personService;
        }

        public  IActionResult Index()
        {
            var listPersons = _personService.GetAll();
            var model = _mapper.Map<IEnumerable<Person>,IEnumerable<PersonViewModel>>(listPersons);
            //_personService.GetAll().ToList().ForEach(b =>
            //{

            //    PersonViewModel book = new PersonViewModel
            //    {
            //        Id = b.Id,
            //        Name = b.Name,
            //        AddedDate = b.AddedDate,
            //        ModifiedDate = b.ModifiedDate
            //    };
            //    model.Add(book);
            //});
            return View( model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Name,AddedDate,ModifiedDate")] PersonViewModel personVm)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<PersonViewModel, Person>(personVm);
                _personService.Add(model);
                return RedirectToAction("Index");
            }
            return View(personVm);
        }
        // GET: Movies/Edit/5
        public IActionResult Edit(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            var model=_mapper.Map<Person, PersonViewModel>(person);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id,[Bind("Id,Name,AddedDate,ModifiedDate")] PersonViewModel personVm)
        {
            if (id != personVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var model = _mapper.Map<PersonViewModel, Person>(personVm);
                    _personService.Update(model);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
                return RedirectToAction("Index");
            }
            return View(personVm);
        }

        // GET: Movies/Delete/5
        public IActionResult  Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var person = _personService.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<Person, PersonViewModel>(person);
            return View(model);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(long id)
        {
            var model = _personService.GetById(id);
            _personService.Delete(model);
            return RedirectToAction("Index");
        }
    }
}