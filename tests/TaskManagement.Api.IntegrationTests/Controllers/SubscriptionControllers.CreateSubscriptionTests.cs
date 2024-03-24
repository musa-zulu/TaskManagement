﻿using TaskManagement.Api.IntegrationTests.Common;
using TaskManagement.Api.IntegrationTests.Common.Subscriptions;
using TaskManagement.Api.IntegrationTests.Common.Tokens;
using TaskManagement.Api.IntegrationTests.Common.WebApplicationFactory;
using TaskManagement.Application.Common.Security.Permissions;
using TaskManagement.Application.Common.Security.Roles;

namespace TaskManagement.Api.IntegrationTests.Controllers;

[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateSubscriptionTests
{
    private readonly AppHttpClient _client;

    public CreateSubscriptionTests(WebAppFactory webAppFactory)
    {
        _client = webAppFactory.CreateAppHttpClient();
        webAppFactory.ResetDatabase();
    }

    [Theory]
    [MemberData(nameof(ListSubscriptionTypes))]
    public async Task CreateSubscriptionForSelf_WhenHasPermission_ShouldCreateSubscription(SubscriptionType subscriptionType)
    {
        // Arrange
        var createSubscriptionRequest = SubscriptionRequestFactory.CreateCreateSubscriptionRequest(subscriptionType: subscriptionType);

        string token = await _client.GenerateTokenAsync(
            TokenRequestFactory.CreateGenerateTokenRequest(
                permissions: [Permission.Subscription.Create],
                roles: []));

        // Act
        var response = await _client.CreateSubscriptionAndExpectSuccessAsync(
            createSubscriptionRequest: createSubscriptionRequest,
            token: token);

        // Assert
        response.SubscriptionType.Should().Be(subscriptionType);
    }

    [Theory]
    [MemberData(nameof(ListSubscriptionTypes))]
    public async Task CreateSubscriptionForSelf_WhenDoesNotHaveRequiredPermission_ShouldReturnForbidden(SubscriptionType subscriptionType)
    {
        // Arrange
        var createSubscriptionRequest = SubscriptionRequestFactory.CreateCreateSubscriptionRequest(subscriptionType: subscriptionType);

        string token = await _client.GenerateTokenAsync(
            TokenRequestFactory.CreateGenerateTokenRequest(
                permissions: [],
                roles: []));

        // Act
        var response = await _client.CreateSubscriptionAsync(
            createSubscriptionRequest: createSubscriptionRequest,
            token: token);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Forbidden);
    }

    [Theory]
    [MemberData(nameof(ListSubscriptionTypes))]
    public async Task CreateSubscriptionForDifferentUser_WhenIsAdmin_ShouldCreateSubscription(SubscriptionType subscriptionType)
    {
        // Arrange
        var createSubscriptionRequest = SubscriptionRequestFactory.CreateCreateSubscriptionRequest(subscriptionType: subscriptionType);

        string token = await _client.GenerateTokenAsync(
            TokenRequestFactory.CreateGenerateTokenRequest(
                id: Guid.NewGuid(),
                permissions: [Permission.Subscription.Create],
                roles: [Role.Admin]));

        // Act
        var response = await _client.CreateSubscriptionAndExpectSuccessAsync(
            createSubscriptionRequest: createSubscriptionRequest,
            token: token);

        // Assert
        response.SubscriptionType.Should().Be(subscriptionType);
    }

    [Theory]
    [MemberData(nameof(ListSubscriptionTypes))]
    public async Task CreateSubscriptionForDifferentUser_WhenIsNotAdmin_ShouldReturnUnauthorized(SubscriptionType subscriptionType)
    {
        // Arrange
        var createSubscriptionRequest = SubscriptionRequestFactory.CreateCreateSubscriptionRequest(subscriptionType: subscriptionType);

        string token = await _client.GenerateTokenAsync(
            TokenRequestFactory.CreateGenerateTokenRequest(
                id: Guid.NewGuid(),
                permissions: [Permission.Subscription.Create],
                roles: []));

        // Act
        var response = await _client.CreateSubscriptionAsync(
            createSubscriptionRequest: createSubscriptionRequest,
            token: token);

        // Assert
        response.Should().HaveStatusCode(HttpStatusCode.Forbidden);
    }

    public static TheoryData<SubscriptionType> ListSubscriptionTypes()
    {
        var subscriptionTypes = Enum.GetValues<SubscriptionType>().ToList();

        var theoryData = new TheoryData<SubscriptionType>();

        foreach (var subscriptionType in subscriptionTypes)
        {
            theoryData.Add(subscriptionType);
        }

        return theoryData;
    }
}