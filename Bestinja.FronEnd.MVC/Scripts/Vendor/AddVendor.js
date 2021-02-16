$("form").on('submit',
    function (e) {
       
        var lat = $("#lat").val();
        var lng = $("#lng").val();
        var numberOfChecked = $('input:checkbox:checked').length;
        if (lat == null || lng == null) {
            alert("لطفا مکان دقیق را روی نقشه مشخص کنید");
            e.preventDefault();
            return;
        }
        if (numberOfChecked < 1) {
            alert("حداقل یک مورد از امکانات را انتخاب کنید");
            e.preventDefault();
            return;
        }
        debugger;
        if ($("#imageUpload").val() === "") {
            alert("لطفا تصویر شاخص را وارد نمایید");
            e.preventDefault();
            return;
        }
    });






var selectCategory = function (element) {
    LoadMap();
    var subCagegory = document.getElementById('subcategory');
    subCagegory.disabled = false;
    var idToLookFor = element.value;
    debugger;
    var category = findId(idToLookFor);
    if (idToLookFor === "") {
        $(".type").addClass('hidden').attr('name', '');
        $(element).attr('name', 'categoryId');
        return;
    }
    if (category.length > 0) {
        $(".type").removeClass('hidden').attr('name', 'categoryId');
        subCagegory.innerHTML = buildOptionsListSubCategory(category, 'name');

    } else {
        $(".type").addClass('hidden').attr('name', '');
        $(element).attr('name', 'categoryId');

    }


};

function findId(idToLookFor) {

    for (var i = 0; i < categories.length; i++) {
        if (categories[i].Id == idToLookFor) {
            return (categories[i].Childrens);
        }
    }
};

LoadMap = function () {
    if ($('#facilities').find('div').length === 0) {
       
    $.ajax({
        url: '/vendor/LoadScript',
        type: 'get',
        success: function (data) {
            $('body').append(data);
        }
    });}
};

var categoryList = document.getElementById('category');
var categories;



$.ajax({
    url: '/vendor/GetCategroy',
    type: 'get',
    contentType: 'application/json',
    success: function (data) {
        categories = data;
        var html = buildOptionsListCategory(data, 'name');
        categoryList.innerHTML = html;
        $(categoryList).attr('data-val-required', "لطفا دسته بندی را وارد نمایید").attr('data-val', true);
        $("#categoryContent.field-validation-valid").attr('data-valmsg-for', 'category');
    }
});

var buildOptionsListSubCategory = function (list) {

    var htmlArray = [];

    for (var i = 0; i < list.length; i++) {

        htmlArray.push('<option' +
            ' value="' +
            list[i].Id +
            '">' +
            list[i].Title +
            '</option>');

    }

    return htmlArray.join('\n');
};
var buildOptionsListCategory = function (list) {

    var htmlArray = ['<option value="">لطفا انتخاب کنید...</option>'];

    for (var i = 0; i < list.length; i++) {
        if (list[i].ParentId === null) {
            htmlArray.push('<option' +
                ' value="' +
                list[i].Id +
                '">' +
                list[i].Title +
                '</option>');
        }

    }

    return htmlArray.join('\n');
};


//image upload functions 
$("#imagePreview").click(function () { $("#imageUpload").click() });
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imagePreview').css('background-image', 'url(' + e.target.result + ')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#imageUpload").change(function () {
    var ext = $(this).val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) === -1) {
        alert('فرمت فایل نام معتبر است');
        event.preventDefault();
        $(this).val("");
        return;
    };
    readURL(this);
});





