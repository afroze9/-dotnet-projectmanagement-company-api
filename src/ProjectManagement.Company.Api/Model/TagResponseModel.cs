﻿namespace ProjectManagement.CompanyAPI.Model;

[ExcludeFromCodeCoverage]
public class TagResponseModel
{
    public int Id { get; set; }

    required public string Name { get; set; }
}