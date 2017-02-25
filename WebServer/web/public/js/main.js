var maxOrdre = 0;

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
};

function book()
{

	//Ajouter une roue de chargement

	var ticket = {};

	var i = 0;

	$(".foodListItem").each(function() 
	{
		var id = $(this).attr('id').split('-')[1];

		ticket[i] = id;

		i++;
	});

	$.post('final', ticket, function(data)
		{
			if(data == 0)
			{
				window.location.href = "final/booked";
			}
			else
			{
				window.location.href = "final/failed";
			}
		});


}

$('document').ready(function()
{
	$.get('maxOrdre', function(data) 
		{
	  		maxOrdre = data;

		}, 'text');

	$('#chosingPanel').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'));
});

$(function()
{
	$('#btn').click(function()
	{
		if(getUrlParameter('ordre') < maxOrdre)
		{
			History.pushState(null, null, 'reserv?code='+getUrlParameter('code')+'&ordre='+(parseInt(getUrlParameter('ordre'))+1));

			$('#chosingPanel').empty();

			$('#chosingPanel').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'));
		}
		else
		{
			book();
		}
	});
});