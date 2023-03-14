using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagement.CompanyAPI.Authorization;

[ExcludeFromCodeCoverage]
public class ScopeRequirement : IAuthorizationRequirement
{
    public string Scope { get; }

    public ScopeRequirement(string scope)
    {
        Scope = scope;
    }
}