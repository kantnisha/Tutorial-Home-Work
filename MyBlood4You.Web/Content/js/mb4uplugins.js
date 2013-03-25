(function ($) {
    $.fn.populateDropDownList = function (options) {
        var defaults = {};
        var opts = $.extend(defaults, options);

        return this.each(function () {
            $(this).change(function () {
                var selectedValue = $(this).val();
                var params = {};
                params[opts.paramName] = selectedValue;
                $('.dropdowns').css("disabled", "disabled");
                $.getJSON(opts.url, params, function (items) {
                    opts.childSelect.empty();
                    $.each(items, function (index, item) {
                        opts.childSelect.append($('<option/>').attr('value', item.Id).text(item.Name));
                    });
                    opts.childSelect.trigger('change');

                    $('.dropdowns').removeAttr("disabled");
                });
            });
        });
    }
})(jQuery);

$(function () {
    $('#selectedCountry').populateDropDownList({
        paramName: 'id',
        url: '/Application/GetStateList',
        childSelect: $('#selectedState')
    });

    $('#selectedState').populateDropDownList({
        paramName: 'id',
        url: '/Application/GetDistrictList',
        childSelect: $('#selectedDistrict')
    });

    $('#selectedDistrict').populateDropDownList({
        paramName: 'id',
        url: '/Application/GetTownList',
        childSelect: $('#selectedTown')
    });
});

