$(document).ready(function()
	{
        $("#codeBox").focus();
    });

$(document).keypress(function(e) 
	{
		console.log(e.which);

	    if(e.which == 13) 
	    {
	        var data = $('#codeBox').val();

	        var code = "";

	        for(i=0; i < data.length; i++)
	        {
	        	switch(data[i])
	        	{
	        		case '&':
	        			code += "1";
	        			break;
	        		case 'é':
	        			code += "2";
	        			break;
	        		case '"':
	        			code += "3";
	        			break;
	        		case '\'':
	        			code += "4";
	        			break;
	        		case '(':
	        			code += "5";
	        			break;
	        		case '-':
	        			code += "6";
	        			break;
	        		case 'è':
	        			code += "7";
	        			break;
	        		case '_':
	        			code += "8";
	        			break;
	        		case 'ç':
	        			code += "9";
	        			break;
	        		case 'à':
	        			code += "0";
	        			break;
	        	}
	        }

	        document.location.href = "/reserv?code=" + code + "&ordre=0";
	    }
	});