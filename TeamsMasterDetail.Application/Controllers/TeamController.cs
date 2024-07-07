using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamsMasterDetail.Application.Models;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Helpers;
using TeamsMasterDetail.Infrastructure.Services.Commands;
using TeamsMasterDetail.Infrastructure.Services.Queries;

namespace TeamsMasterDetail.Application.Controllers
{
    public class TeamController(IMediator mediator) : Controller
    {
        public async Task<IActionResult> Index()
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = null,
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> Select(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }

        #region Insert
        [HttpPost]
        public async Task<IActionResult> InsertEntry()
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = null,
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Insert
            };

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> InsertSave(Team team)
        {
            await mediator.Send(new TeamCommand(team, CommandMode.Create));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(team.TeamId)),
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<IActionResult> UpdateEntry(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Update
            };

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSave(Team team)
        {
            await mediator.Send(new TeamCommand(team, CommandMode.Update));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = team,
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Delete(int teamId)
        {
            Team? team = await mediator.Send(new TeamQueryFindAsync(teamId));

            await mediator.Send(new TeamCommand(team, CommandMode.Delete));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = null,
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> CancelEntry(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> CancelSelection()
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = null,
                SelectedMember = null,
                InputData = InputData.Teams,
                DisplayMode = DisplayMode.Read
            };

            return View("Main", model);
        }
    }
}
