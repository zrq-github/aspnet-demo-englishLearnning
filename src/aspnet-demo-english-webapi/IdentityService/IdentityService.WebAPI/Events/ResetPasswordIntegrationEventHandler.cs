using Identity.Repository;
using Microsoft.Extensions.Logging;
using Ron.EventBus;

namespace IdentityService.WebAPI.Events
{
    [EventName("IdentityService.User.PasswordReset")]
    public class ResetPasswordIntegrationEventHandler : JsonIntegrationEventHandler<ResetPasswordEvent>
    {
        private readonly ILogger<ResetPasswordIntegrationEventHandler> logger;
        private readonly ISmsSender smsSender;

        public ResetPasswordIntegrationEventHandler(ILogger<ResetPasswordIntegrationEventHandler> logger, ISmsSender smsSender)
        {
            this.logger = logger;
            this.smsSender = smsSender;
        }

        public override Task HandleJson(string eventName, ResetPasswordEvent? eventData)
        {
            //发送密码给被用户的手机
            return smsSender.SendAsync(eventData.PhoneNum, eventData.Password);
        }
    }
}