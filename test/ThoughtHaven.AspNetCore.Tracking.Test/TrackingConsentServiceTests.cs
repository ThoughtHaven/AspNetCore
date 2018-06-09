using System;
using System.Threading.Tasks;
using ThoughtHaven.AspNetCore.Tracking.Fakes;
using Xunit;

namespace ThoughtHaven.AspNetCore.Tracking
{
    public class TrackingConsentServiceTests
    {
        public class Type
        {
            [Fact]
            public void ImplementsITrackingConsentService()
            {
                Assert.True(typeof(ITrackingConsentService).IsAssignableFrom(
                    typeof(TrackingConsentService)));
            }
        }

        public class Constructor
        {
            public class HttpContextAccessorOverload
            {
                [Fact]
                public void NullHttpContextAccessor_Throws()
                {
                    Assert.Throws<ArgumentNullException>("httpContextAccessor", () =>
                    {
                        new TrackingConsentService(httpContextAccessor: null);
                    });
                }
            }
        }

        public class CanTrackMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public async Task HttpContextHasNoITrackingConsentFeature_ReturnsFalse()
                {
                    var service = new TrackingConsentService(HttpContextAccessor(
                        trackingFeature: null));

                    Assert.False(await service.CanTrack());
                }

                [Fact]
                public async Task TrackingConsentFeatureReturnsFalse_ReturnsFalse()
                {
                    var feature = TrackingFeature();
                    feature.CanTrack = false;

                    var service = new TrackingConsentService(HttpContextAccessor(feature));

                    Assert.False(await service.CanTrack());
                }

                [Fact]
                public async Task TrackingConsentFeatureReturnsTrue_ReturnsTrue()
                {
                    var feature = TrackingFeature();
                    feature.CanTrack = true;

                    var service = new TrackingConsentService(HttpContextAccessor(feature));

                    Assert.True(await service.CanTrack());
                }
            }
        }

        public class GrantConsentMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public async Task HttpContextHasNoTrackingConsentFeature_Throws()
                {
                    var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    {
                        await Service(HttpContextAccessor(trackingFeature: null))
                            .GrantConsent();
                    });

                    Assert.Equal("No ITrackingConsentFeature exists in the HttpContext.Features collection.",
                        exception.Message);
                }

                [Fact]
                public async Task WhenCalled_CallsGrantConsentOnTrackingConsentFeature()
                {
                    var feature = TrackingFeature();

                    await Service(HttpContextAccessor(feature)).GrantConsent();

                    Assert.True(feature.GrantConsent_Called);
                }
            }
        }

        public class WithdrawConsentMethod
        {
            public class EmptyOverload
            {
                [Fact]
                public async Task HttpContextHasNoTrackingConsentFeature_Throws()
                {
                    var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    {
                        await Service(HttpContextAccessor(trackingFeature: null))
                            .WithdrawConsent();
                    });

                    Assert.Equal("No ITrackingConsentFeature exists in the HttpContext.Features collection.",
                        exception.Message);
                }

                [Fact]
                public async Task WhenCalled_CallsWithdrawConsentOnTrackingConsentFeature()
                {
                    var feature = TrackingFeature();

                    await Service(HttpContextAccessor(feature)).WithdrawConsent();

                    Assert.True(feature.WithdrawConsent_Called);
                }
            }
        }

        private static FakeTrackingConsentFeature TrackingFeature() =>
            new FakeTrackingConsentFeature();
        private static FakeHttpContextAccessor HttpContextAccessor(
            FakeTrackingConsentFeature trackingFeature = null) =>
            new FakeHttpContextAccessor(trackingFeature);
        private static TrackingConsentService Service(
            FakeHttpContextAccessor httpContextAccessor = null) =>
            new TrackingConsentService(httpContextAccessor ?? HttpContextAccessor());
    }
}