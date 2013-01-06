$.fx.speeds._default = 1000;
$(function() {
	$( "#add-category-form" ).dialog({
		autoOpen: false,
		modal: true,
		width: 400,
		height: 350,
		resizable: false,
	});

    $("#add-category-form").parent().appendTo(jQuery("form:first"));

	$( "#add-category" ).click(function() {
		$( "#add-category-form" ).dialog( "open" );
		return false;
	});
	
	$( "#add-note-form" ).dialog({
		autoOpen: false,
		modal: true,
		width: 400,
		height: 480,
		resizable: false,
	});

    $("#add-note-form").parent().appendTo(jQuery("form:first"));

	$( "#add-note" ).click(function() {
		$( "#add-note-form" ).dialog( "open" );
		return false;
	});
	
	$("#datepicker").datepicker({
		dateFormat: $.datepicker.ATOM,
		showAnim: 'slideDown',
	});
});