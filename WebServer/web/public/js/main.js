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

	var datas = {};

	datas[0] = getUrlParameter('code');
	datas[1] = getTicket();

	$.post('final', datas, function(data)
		{
			switch(data[0])
			{
				case '0':
					window.location.href = "final/booked";
					break; 
				case '1':
					window.location.href = "final/failed?e=1";
					break;
				case '2':
					window.location.href = "final/failed?e=2&d="+data[1];
					break;
				case '3':
					window.location.href = "final/failed?e=3";
					break;
			}
		});


}

$('document').ready(function()
{
	$.get('maxOrdre', function(data) 
		{
	  		maxOrdre = data;

		}, 'text');

	$('#column_left').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'), getTicket());
});

function ajouter_bouffe(id, name)
{
	document.getElementById("ticket").innerHTML = document.getElementById("ticket").innerHTML + "<div class='foodListItem' id='fLI-"+id+"'>" + name + "</div>";

	goNext();
}

function goNext()
{
	if(getUrlParameter('ordre') < maxOrdre)
	{
		History.pushState(null, null, 'reserv?code='+getUrlParameter('code')+'&ordre='+(parseInt(getUrlParameter('ordre'))+1));

		$('#column_left').empty();

		$('#column_left').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'), getTicket());
	}
	else
	{
		book();
	}

	$(window).bind('statechange',function(){
		$('#column_left').load('menu?code='+getUrlParameter('code')+'&ordre='+getUrlParameter('ordre'), getTicket());
	});
}