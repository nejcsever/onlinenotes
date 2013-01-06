$(document).ready(function () {
    $("img.select-language").click(function (event) {
        event.preventDefault();
        switchLanguage(event.target.id);
    });
});

function switchLanguage(language) {
    $.ajax({
        url: 'data/languages.xml',
        success: function(xml){
            $(xml).find('language').filter(function(){
                return $(this).attr('id') == language;
            }).children().each(function(){
                var element = $(this).attr('id');
                if ($(this).is('input')) {
					alert(element)
					$("#" + element).innerHTML = $(this).contents();
				}
				else {
					$("#" + element).html($(this).contents());
				}
            });
        }
    });
}
