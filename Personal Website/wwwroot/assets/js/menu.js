function pageNavigate(menu) {
    $.post(`/${menu}`, function (content) {

        //let mainDiv = document.getElementById('mainDiv');
        //if (mainDiv.classList.contains('animation-page')) {
        //    mainDiv.classList.remove('animation-page');
        //}

        //$('#mainDiv').html(content);
        //$('#mainDiv').addClass('animation-page').html(content);
        $('#mainDiv').html(`<div class="animation-page">${content}</div>`);

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