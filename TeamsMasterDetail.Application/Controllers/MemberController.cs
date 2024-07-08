using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamsMasterDetail.Application.Models;
using TeamsMasterDetail.Domain.Entities;
using TeamsMasterDetail.Infrastructure.Helpers;
using TeamsMasterDetail.Infrastructure.Services.Commands;
using TeamsMasterDetail.Infrastructure.Services.Queries;

namespace TeamsMasterDetail.Application.Controllers
{
    public class MemberController(IMediator mediator) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> List(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> Select(int teamId, int memberId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = await mediator.Send(new MemberQueryFindAsync(memberId)),
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }

        #region Insert
        [HttpPost]
        public async Task<IActionResult> InsertEntry(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Insert
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> InsertSave(Member member)
        {
            await mediator.Send(new MemberCommand(member, CommandMode.Create));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(member.TeamId)),
                SelectedMember = await mediator.Send(new MemberQueryFindAsync(member.MemberId)),
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<IActionResult> UpdateEntry(int teamId, int memberId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = await mediator.Send(new MemberQueryFindAsync(memberId)),
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Update
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSave(Member member)
        {
            await mediator.Send(new MemberCommand(member, CommandMode.Update));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(member.TeamId)),
                SelectedMember = await mediator.Send(new MemberQueryFindAsync(member.MemberId)),
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> Delete(int teamId, int memberId)
        {
            Member? member = await mediator.Send(new MemberQueryFindAsync(memberId));

            await mediator.Send(new MemberCommand(member, CommandMode.Delete));

            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

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
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }

        [HttpPost]
        public async Task<IActionResult> CancelSelection(int teamId)
        {
            MasterDetailViewModel model = new()
            {
                Teams = await mediator.Send(new TeamQueryToListAsync()),
                SelectedTeam = await mediator.Send(new TeamQueryFindAsync(teamId)),
                SelectedMember = null,
                InputData = InputData.Members,
                DisplayMode = DisplayMode.Read
            };

            await mediator.Send(new MemberQueryLoadMembersByTeam(model.SelectedTeam));

            return View("Main", model);
        }
    }
}