﻿using ProjectManagement.CompanyAPI.Domain.Entities;
using ProjectManagement.CompanyAPI.Domain.Events;

namespace ProjectManagement.CompanyAPI.UnitTests.Domain.Events;

[ExcludeFromCodeCoverage]
public class TagRemovedEventTests
{
    [Fact]
    public void TagRemovedEvent_WhenCreated_ReturnsCorrectValues()
    {
        Tag tag = new ("tag 1");
        Company company = new ("company a");

        TagRemovedEvent sut = new (company, tag);

        Assert.Equal(tag, sut.Tag);
        Assert.Equal(company, sut.Company);
        Assert.True(sut.DateOccurred >= DateTime.UtcNow.AddMinutes(-5));
    }
}