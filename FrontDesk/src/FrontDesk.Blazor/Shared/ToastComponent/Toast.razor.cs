using Microsoft.AspNetCore.Components;

namespace FrontDesk.Blazor.Shared.ToastComponent
{
    public partial class Toast
    {
        [Parameter]
        public bool IsShow { get; set; } = false;
        [Parameter]
        public ToastType ToastType { get; set; } = ToastType.Success;

        [Parameter]
        public string Message { get; set; } = string.Empty;

        private void Close()
        {
            IsShow = false;
        }

        private string AlertType
        {
            get
            {
                if (ToastType == ToastType.Error)
                {
                    return "alert-danger";
                }
                else if (ToastType == ToastType.Info)
                {
                    return "alert-info";
                }
                else if (ToastType == ToastType.Warning)
                {
                    return "alert-warning";
                }

                return "alert-success";
            }
        }
    }
}
