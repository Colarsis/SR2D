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

function getTicket()
{
	var ticket = [];

	var i = 0;

	$(".foodListItem").each(function() 
	{
		var id = $(this).attr('id').split('-')[1];

		ticket.push(id);

		i++;
	});

	var realTicket = {};

	realTicket[0] = ticket;

	return realTicket;
}

function book()
{

	//Ajouter une roue de chargement

	$.post('final', getTicket(), function(data)
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

	console.log(getTicket());

	$('#chosingPanel').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'), getTicket());
});

$(function()
{
	$('#btn').click(function()
	{
		if(getUrlParameter('ordre') < maxOrdre)
		{
			History.pushState(null, null, 'reserv?code='+getUrlParameter('code')+'&ordre='+(parseInt(getUrlParameter('ordre'))+1));

			$('#chosingPanel').empty();

			$('#chosingPanel').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'), getTicket());
		}
		else
		{
			book();
		}
	});
});