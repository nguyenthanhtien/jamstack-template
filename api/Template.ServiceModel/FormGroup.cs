// Complete declarative AutoQuery services for FormGroups CRUD example:
// https://docs.servicestack.net/autoquery-crud-FormGroups

using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Template.ServiceModel;

[Description("FormGroup Details")]
[Notes("Information")]
public class FormGroup : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }
}

[Tag("FormGroups"), Description("Find FormGroups")]
[Notes("Find out how to quickly create a <a class='svg-external' target='_blank' href='https://youtu.be/nhc4MZufkcM'>C# FormGroups App from Scratch</a>")]
[Route("/FormGroups", "GET")]
[Route("/FormGroups/{Id}", "GET")]
[AutoApply(Behavior.AuditQuery)]
public class QueryFormGroups : QueryDb<FormGroup>
{
    public int? Id { get; set; }
}

// Uncomment below to enable DeletedFormGroups API to view deleted FormGroups:
// [Route("/FormGroups/deleted")]
// [AutoFilter(QueryTerm.Ensure, nameof(AuditBase.DeletedDate), Template = SqlTemplate.IsNotNull)]
// public class DeletedFormGroups : QueryDb<FormGroup> {}

[Tag("FormGroups"), Description("Create a new FormGroup")]
[Route("/FormGroups", "POST")]
[ValidateHasRole("Employee")]
[AutoApply(Behavior.AuditCreate)]
public class CreateFormGroup : ICreateDb<FormGroup>, IReturn<IdResponse>
{
    [Description("Name this FormGroup is for"), ValidateNotEmpty]
    public string Name { get; set; }
    public int Index { get; set; }
}

[Tag("FormGroups"), Description("Update an existing FormGroup")]
[Route("/FormGroup/{Id}", "PATCH")]
[ValidateHasRole("Employee")]
[AutoApply(Behavior.AuditModify)]
public class UpdateFormGroup : IPatchDb<FormGroup>, IReturn<IdResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? Index { get; set; }

}

[Tag("FormGroups"), Description("Delete a FormGroup")]
[Route("/FormGroup/{Id}", "DELETE")]
[ValidateHasRole("Manager")]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteFormGroup : IDeleteDb<FormGroup>, IReturnVoid
{
    public int Id { get; set; }
}
