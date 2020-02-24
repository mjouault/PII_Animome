@model Animome.Models.Suivi

@{
    ViewData["Title"] = "Create";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h1>Create</h1>

<h4>Patient</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Identifiant" class="control-label"></label>
                <input asp-for="Identifiant" class="form-control" />
                <span asp-validation-for="Identifiant" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type = "submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action="Index">Back to List</a>
</div>

@section Scripts
{
    @{ await Html.RenderPartialAsync("_ValidationScriptsPartial"); }
}
