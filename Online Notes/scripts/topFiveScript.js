$(document).ready(function () {

    /* Izbira datuma za dinamicno dodane forme za urejanje opravil */
    $(".datepicker.edit-note-input").on('mouseover', function () {
        $(this).datepicker({
            dateFormat: $.datepicker.ATOM,
            showAnim: 'slideDown',
        });
    });

    /* Shrani spremembe */
    $(".save-note-changes.basic-button").on('click', function () {
        var noteDiv = $(this).closest('div.note');
        var id = noteDiv.attr('id');
        id = id.substring(4, id.length) // odstranimo "note"
        var title = noteDiv.find('input.note-title-input').attr('value');
        var deadline = noteDiv.find('input.datepicker').attr('value');
        var description = noteDiv.find('textarea.edit-note-input').attr('value');
        var priority = noteDiv.find('input[name=prioritynote' + id + ']:checked').val();
        noteDiv.find('p.note-title-p').html(title); // dinamično spremenimo naslov
        $('input[name=radioName]:checked', '#myForm').val()
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: '{"id" : "' + id + '", "title" : "' + title + '", "deadline" : "' + deadline + '", "description": "' + description + '", "priority" : "' + priority + '"}',
            url: "Default.aspx/UpdateNote",
            success: function () { }
        });
        event.preventDefault();
    });

    /* Brisanje opravila */
    $(".delete-note.basic-button").on('click', function () {
        var divNote = $(this).closest('div.note');
        var id = divNote.attr('id');
        id = id.substring(4, id.length) // odstranimo "note"
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: '{"id" : "' + id + '"}',
            url: "Default.aspx/DeleteNote",
            success: function () { }
        });

        divNote.slideUp(400, function () { divNote.remove(); });
        event.preventDefault();
    });

    /* Dokoncanje opravila */
    $("button.finish-note").on('click', function () {
        var id = $(this).attr('value').replace('note', '')
        $.get("completeNote.php" + "?id=" + $(this).attr('value').replace('note', ''), function () { });
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: '{"id" : "' + id + '"}',
            url: "Default.aspx/CompleteNote",
            success: function () { }
        });
        var target = $("#" + $(this).attr('value'));
        target.slideUp(400, function () { target.remove(); });
        event.preventDefault();
    });

    /* Avtomatski premik strani ob odprtju urejanja opravila. */
    $(".note-title").on('click', function () {
        var note = $(this).parent();
        var target = $("#description" + note.attr('id'));
        target.slideToggle(200);
        $('html,body').animate({ scrollTop: $(this).offset().top }, 500);
    });
});