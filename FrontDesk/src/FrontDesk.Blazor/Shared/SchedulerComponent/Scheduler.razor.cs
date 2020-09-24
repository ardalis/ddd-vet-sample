using BlazorShared.Models.Appointment;
using BlazorShared.Models.AppointmentType;
using BlazorShared.Models.Room;
using FrontDesk.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FrontDesk.Blazor.Shared.SchedulerComponent
{

    public partial class Scheduler
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        AppointmentService AppointmentService { get; set; }        

        [Parameter]
        public int Height { get; set; } = 750;
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public RenderFragment<Resource> RenderFragmentResources { get; set; }

        [Parameter] public List<string> Groups { get; set; } = new List<string>();
        [Parameter] public DateTime StartDate { get; set; } = new DateTime();
        [Parameter] public DateTime StartTime { get; set; } = new DateTime();
        [Parameter] public DateTime EndTime { get; set; } = new DateTime();
        [Parameter] public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

        List<AppointmentDto> _appointments;
        [Parameter]
        public List<AppointmentDto> Appointments
        {
            get { return _appointments; }
            set
            {
                _appointments = value;
                CallJSMethod();
            }
        }

        [Parameter] public List<AppointmentTypeDto> AppointmentTypes { get; set; } = new List<AppointmentTypeDto>();
        [Parameter] public List<SchedulerResourceModel> SchedulerResources { get; set; } = new List<SchedulerResourceModel>();

        [Parameter]
        public EventCallback<AppointmentDto> OnEditCallback { get; set; }

        private List<SchedulerResourceModel> Resources { get; set; } = new List<SchedulerResourceModel>();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CallJSMethod();
                var thisReference = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("addListenerToFireEdit", thisReference);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        public void AddResource(Resource resourceComponent)
        {
            Resources.Add(resourceComponent.SchedulerResource);
            StateHasChanged();
        }

        private async Task CallJSMethod()
        {
            await JSRuntime.InvokeVoidAsync("scheduler", StartDate, StartTime, EndTime, Appointments, Resources, Groups, Height);            
        }

        [JSInvokable]
        public async Task KendoCall(string action, string jsonData)
        {
            if(action == "edit")
            {
                var result = JsonSerializer.Deserialize<AppointmentDto>(jsonData, JsonOptions);
                await OnEditCallback.InvokeAsync(result);
            }
            else if (action == "move")
            {
                var result = JsonSerializer.Deserialize<AppointmentDto>(jsonData, JsonOptions);
                await AppointmentService.EditAsync(UpdateAppointmentRequest.FromDto(result));
            }
            else if (action == "delete")
            {
                var result = JsonSerializer.Deserialize<AppointmentDto>(jsonData, JsonOptions);
                await AppointmentService.DeleteAsync(result.AppointmentId);
                Appointments.Remove(Appointments.First(x => x.AppointmentId == result.AppointmentId));
                await CallJSMethod();
            }
            


        }
    }
}
