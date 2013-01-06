var XMLPath = 'data/categories.xml';
var shownCategory;
var temporaryAddNoteID = 0; // zacasni IDji opravil, ki se jih dinamicno dodaja
var temporaryAddCategoryID = 0; // zacasni IDji kategorij, ki se jih dinamicno dodaja

$(document).ready(function(){

    $("#category-navigation").hide(); // skrijemo, ker se ni izbrana nobena kategorija

    /* Delete category */
    $("#DeleteCategory").live('click', function() {
        $("#SelectedCategory").val(shownCategory.replace('category',''));
    });

    $("#add-note").on('click', function() {
        $("#ShownCategory").val(shownCategory.replace('category',''));
    });

    /* Spreminjanje ozanja izbrane kategorije. */
	$(".category-item").bind('click', function() {
		/* Set color for selected category. */
		var id = $(this).attr('id')
		$('#' + shownCategory).css('color', '');
		$('#' + shownCategory).css('background-color', '');
		$('#' + id).css('color', 'white');
		$('#' + id).css('background-color', '#222');
		shownCategory = id;
	});

    /* After ajax call. */
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandle);
    function endRequestHandle(sender, Args)
    {

        /* Add navigation buttons. */
        $("#category-navigation").show();

        /* Shrani spremembe */
        $(".save-note-changes.basic-button").on('click', function() {
            var noteDiv = $(this).closest('div.note');
            var id = noteDiv.attr('id');
            id = id.substring(4, id.length) // odstranimo "note"
            var title = noteDiv.find('input.note-title-input').attr('value');
            var deadline = noteDiv.find('input.datepicker').attr('value');
            var description = noteDiv.find('textarea.edit-note-input').attr('value');
            var priority = noteDiv.find('input[name=prioritynote' + id  + ']:checked').val();
            noteDiv.find('p.note-title-p').html(title); // dinamièno spremenimo naslov
            $('input[name=radioName]:checked', '#myForm').val()
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: '{"id" : "' + id + '", "title" : "' + title + '", "deadline" : "' + deadline + '", "description": "' + description + '", "priority" : "' + priority + '"}',
                url: "Default.aspx/UpdateNote",
                success: function () {}
            });
            event.preventDefault();
        });

        /* Brisanje opravila */
        $(".delete-note.basic-button").on('click', function() {
            var divNote = $(this).closest('div.note');
            var id = divNote.attr('id');
            id = id.substring(4, id.length) // odstranimo "note"
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: '{"id" : "' + id + '"}',
                url: "Default.aspx/DeleteNote",
                success: function () {}
            });

            divNote.slideUp(400, function(){ divNote.remove(); });
            event.preventDefault();
        });

        /* Dokoncanje opravila */
        $("button.finish-note").on('click', function() {
            var id = $(this).attr('value').replace('note', '')
            $.get("completeNote.php" + "?id=" + $(this).attr('value').replace('note', ''), function(){});
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                data: '{"id" : "' + id + '"}',
                url: "Default.aspx/CompleteNote",
                success: function () {}
            });
            var target = $("#" + $(this).attr('value'));
            target.slideUp(400, function(){ target.remove(); });
            event.preventDefault();
        });

        /* Avtomatski premik strani ob odprtju urejanja opravila. */
        $(".note-title").on('click', function() {
		    var note = $(this).parent();
		    var target = $("#description" + note.attr('id'));
		    target.slideToggle(200);
		     $('html,body').animate({scrollTop: $(this).offset().top}, 500);
		 });

        /* Izbira datuma za dinamicno dodane forme za urejanje opravil */
	    $(".datepicker.edit-note-input").on('mouseover', function(){
	        $(this).datepicker({
	            dateFormat: $.datepicker.ATOM,
			    showAnim: 'slideDown',
    	    });
	    });
	
	    /* Spreminjanje ozanja izbrane kategorije. */
        $(".category-item").unbind('click');
	    $(".category-item").bind('click', function() {
		    /* Set color for selected category. */
		    var id = $(this).attr('id')
		    $('#' + shownCategory).css('color', '');
		    $('#' + shownCategory).css('background-color', '');
		    $('#' + id).css('color', 'white');
		    $('#' + id).css('background-color', '#222');
		    shownCategory = id;
	    });
    }
	
	/* Na zacetku nalozimo kategorije in prikazemo opravila za prvo kategorijo */
	shownCategory = '0';
	showCategories(XMLPath);
	showCategoryNotes('0', XMLPath);
});

/* Prikaze seznam kategorij. */
/*function showCategories(categoriesXMLPath){
    $.ajax({
        url: categoriesXMLPath,
        success: function(xml){
            $(xml).find('category').each(function(){
				addCategoryToDOM("category" + $(this).attr('id'), $(this).find('name').text());
            });
        }
    });
}*/

/* Prikaze opravila podane kategorije. */
/*function showCategoryNotes(categoryID, categoriesXMLPath) {*/
	
	/* Show notes */
	/*$.ajax({
        url: categoriesXMLPath,
        success: function(xml){
			var category = $(xml).find('category').filter(function(){
				return $(this).attr('id') == categoryID;
            });
			
			$("#category-name").html(category.find('name').text());
			$("#category-subname").html(category.find('subname').text());
			
			category.find('note').each(function (){ // chosen category
            	var noteID = $(this).attr('id');
				var noteTitle = $(this).find('title').text();
				var noteDescription = $(this).find('description').text();
				var noteDeadline = $(this).find('deadline').text();
				var notePriority = $(this).find('priority').text();
				var noteCreated = $(this).find('created').text();
				// ID: note+noteID - zato, da jih razlikujemo z categoryID
				addNoteToDOM("note" + noteID, noteTitle, noteDescription, noteDeadline, notePriority, noteCreated);
			});
        }
    });
}*/

/* Dodajanje opravila v seznam opravil. */
function addNoteToDOM(id, title, description, deadline, priority, created) {
	var newNote = document.createElement('div');
	/* Note title */
	newNote.setAttribute("id", id);
	newNote.setAttribute("class", "note");
	var content = '<div class="note-title"><table><tr><td><p class="note-title-p">'+ title + 
	'</p></td><td class="second-column"><button value="' + id +
	'" class="finish-note basic-button" title="finish note"></button></td></tr></table></div>';	

	/* Note description */
	content += '<div id="description' + id + '" class="note-description"><table class="note-properties"><tr><td class="'+
	'first-column"><label class="description">Title:</label></td><td><input type="text" class="note-title-input edit-note-input" value="'+
	title + '" /></td></tr><tr><td class="first-column"><label class="description">Deadline:</label></td><td><input value="' + deadline + '" class="datepicker edit-note-input"/></td></tr><tr>' +
	'<td class="first-column"><label class="description">Priority:</label></td><td>' +
	'<label><input type="radio" name="priority'+ id +'" value="High"' + ((priority == 'h')? ' checked ' : ' ') + '>High</label>' +
	'<label><input type="radio" name="priority'+ id +'" value="Medium"' + ((priority == 'm')? ' checked ' : ' ') + '>Medium</label>' +
	'<label><input type="radio" name="priority'+ id +'" value="Low"' + ((priority == 'l')? ' checked ' : ' ') + '>Low</label>' +
	'</td></tr><tr><td class="first-column"><label class="description">Description:</label></td><td>' +
	'<textarea class="edit-note-input">' + description + '</textarea></td></tr></table>' +
	'<p class="note-created">Created: ' + created + '</p><button class="delete-note basic-button">Delete note</button></div>';
	newNote.innerHTML = content;
	$('#note-list').append(newNote);
}
