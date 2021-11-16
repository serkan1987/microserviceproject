﻿using Communication.Http.Department.HR.Models;

using Infrastructure.Communication.Http.Wrapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Business.Departments.HR.Services;
using Services.Business.Departments.HR.Util.Validation.Person.CreatePerson;
using Services.Business.Departments.HR.Util.Validation.Person.CreateTitle;
using Services.Business.Departments.HR.Util.Validation.Person.CreateWorker;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Business.Departments.HR.Controllers
{
    [Authorize]
    [Route("Person")]
    public class PersonController : BaseController
    {
        private readonly PersonService _personService;

        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route(nameof(GetPeople))]
        public async Task<IActionResult> GetPeople(CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<List<PersonModel>>(async () =>
            {
                return await _personService.GetPeopleAsync(cancellationTokenSource);
            },
            services: _personService);
        }

        [HttpPost]
        [Route(nameof(CreatePerson))]
        public async Task<IActionResult> CreatePerson([FromBody] PersonModel person, CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<int>(async () =>
            {
                await CreatePersonValidator.ValidateAsync(person, cancellationTokenSource);

                return await _personService.CreatePersonAsync(person, cancellationTokenSource);
            },
            services: _personService);
        }

        [HttpGet]
        [Route(nameof(GetTitles))]
        public async Task<IActionResult> GetTitles(CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<List<TitleModel>>(async () =>
            {
                return await _personService.GetTitlesAsync(cancellationTokenSource);
            },
            services: _personService);
        }

        [HttpPost]
        [Route(nameof(CreateTitle))]
        public async Task<IActionResult> CreateTitle([FromBody] TitleModel title, CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<int>(async () =>
            {
                await CreateTitleValidator.ValidateAsync(title, cancellationTokenSource);

                return await _personService.CreateTitleAsync(title, cancellationTokenSource);
            },
            services: _personService);
        }

        [HttpGet]
        [Route(nameof(GetWorkers))]
        public async Task<IActionResult> GetWorkers(CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<List<WorkerModel>>(async () =>
            {
                return await _personService.GetWorkersAsync(cancellationTokenSource);
            },
            services: _personService);
        }

        [HttpPost]
        [Route(nameof(CreateWorker))]
        public async Task<IActionResult> CreateWorker([FromBody] WorkerModel worker, CancellationTokenSource cancellationTokenSource)
        {
            return await HttpResponseWrapper.WrapAsync<int>(async () =>
            {
                await CreateWorkerValidator.ValidateAsync(worker, cancellationTokenSource);

                return await _personService.CreateWorkerAsync(worker, cancellationTokenSource);
            },
            services: _personService);
        }
    }
}
