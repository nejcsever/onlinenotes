$(document).ready(function () {
    /* Avtomatski premik strani ob odprtju urejanja opravila. */
    $(".note-title").live('click', function () {
        var note = $(this).parent();
        var target = $("#description" + note.attr('id'));
        target.slideToggle(200);
        $('html,body').animate({ scrollTop: $(this).offset().top }, 500);
    });
});