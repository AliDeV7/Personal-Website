function pageNavigate(menu) {
    $.post(`/${menu}`, function (content) {
        $('#mainDiv').html("");
        $('#mainDiv').html(content);

        $('#homemenu').removeClass("active");
        $('#homemenu').removeClass("disabled-link");

        $('#resumemenu').removeClass("active");
        $('#resumemenu').removeClass("disabled-link");

        $('#projectsmenu').removeClass("active");
        $('#projectsmenu').removeClass("disabled-link");

        $('#contactmenu').removeClass("active");
        $('#contactmenu').removeClass("disabled-link");

        $(`#${menu}menu`).addClass("active");
        $(`#${menu}menu`).addClass("disabled-link");
    })
}