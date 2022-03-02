function saveCategory(categoryName) {
    $.post("/Categories/CreateCategoryAjax", { name: categoryName }, function (data) {
        appendNewCategoryToSelect(data);
        closeNewCategoryModal();
    }).fail(function () {
        alert("error");
    });
}

function appendNewCategoryToSelect(category)
{
    $('#CategoryId').append($('<option>', {
        value: category.id,
        text: category.name
    }));
}

function closeNewCategoryModal()
{
    $('#create-category-modal').modal('hide');
}

$("#create-category-form").submit(function (e) {
    if ($("#create-category-form").valid()) {
        var name = $("#category-name").val();
        saveCategory(name);
    }
    return false;
});

$('#create-category-modal').on('hidden.bs.modal', function (e) {
    $("#category-name").val('');
    $("#create-category-form").find(".field-validation-error").empty();
    $("#create-category-form").validate().resetForm();
})


