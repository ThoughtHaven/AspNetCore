using System.Threading.Tasks;

namespace ThoughtHaven.AspNetCore.Tracking
{
    public interface ITrackingConsentService
    {
        Task<bool> CanTrack();
        Task GrantConsent();
        Task WithdrawConsent();
    }
}