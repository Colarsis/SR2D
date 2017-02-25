var cfg = require("./configFile");
var url = require('url');
var fs = require("fs");
var express = require("express");
var app = express();
var path = require("path");
var cfg = require("./configFile");
var mysql = require('mysql');
var reserv = require('./reserv.js');
var menu = require('./menu.js');
var http = require("http").Server(app);
var io = require("socket.io")(http);
var net = require('net');
var async = require('async');
var bodyP = require('body-parser');

app.use(bodyP.json());
app.use(bodyP.urlencoded(
{
	extended: true
})); 

var clientStatus = false;

var coCfg = cfg.readDatabaseConfig();
var nu5 = ["", "", "", "", ""];

if(coCfg != nu5)
{

	connection = mysql.createConnection({
		host     : coCfg[4],
		user     : coCfg[1],
		password : coCfg[2],
		database : coCfg[0],
		port     : coCfg[3]
	});

	connection.connect();
}
else
{
	throw new Error('E03');
}

var nu2 = ["", "", "", ""];
var syncCfg = cfg.readSynchroConfig();

if(syncCfg == nu2)
{
	throw new Error('E04');
}

//**********  Node to update server communication  ***********//

var client = net.createConnection(syncCfg[1], syncCfg[0], function()
{
	console.log('Connected to the US');
});

client.on('error', function(ex) 
{
	console.log("US connection error handled :");
	console.log(ex);
	console.log("ERROR END");
});

client.on('data', function(data)
{
	var realData = data.toString('utf8');

	realData = realData.split(';')

	if(realData[0].split('=')[0] == "rt" && checkMessageValiditie(realData))
	{
		switch (realData[0].split('=')[1]) 
		{
			case "update":
				sendUpdate();
				client.write("at=update;m=updated;");
				break;
			case "ping":
				client.write("at=ping;m=pong;");
				break;
		}
	}
	else if(realData[0].split('=')[0] == "at" && checkMessageValiditie(realData))
	{
		switch (realData[0].split('=')[1]) 
		{
			case "update":
				break;
			case "connect":
				if(realData[1].split('=')[1] == "right_infos")
				{
					console.log('Credentials OK');
					clientStatus = true;
				}
				else
				{
					throw new Error('E05');
				}
				break;
		}
	}
});

client.on('connect', function()
{
	connectSync();
});

client.on('timeout', function()
{
	client.close();
});

client.on('close', function(er)
{
	clientStatus = false;
	console.log('US connection closed! Server not able to work anymore. Please restart');
});

function checkMessageValiditie(message)
{
    var messageType = message[0].split('=');

    if (messageType[0] == "rt")
    {
        switch (messageType[1])
        {
            case "connect":
                if (message[1].split('=')[0] == "un" && message[2].split('=')[0] == "pw")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case "ping":
                return true;
            case "update":
                return true;
            default:
                return false;
        }
    }
    else if (messageType[0] == "at")
    {
        return true;
    }
    else
    {
        return false;
    }
}

function connectSync()
{
	client.write("rt=connect;un="+syncCfg[2]+";pw="+syncCfg[3]+";");
}

//**********  Node to update server communication end  ***********//

//**********  Express routes  **********//

app.use('/css', express.static(__dirname + '/public/css'));
app.use('/media', express.static(__dirname + '/public/media'));
app.use('/js', express.static(__dirname + '/public/js'));

app.get('/', function (req, res) 
{
	if(clientStatus)
	{
		res.sendFile(__dirname + '/public/html/acceuil.html');
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
});

app.get('/reserv', function (req, res) 
{

	console.log('Reserv asked');

	if(clientStatus)
	{
		reserv.generatePage(url.parse(req.url).query, function(page)
		{
			res.send(page);
		});
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.get('/menu', function (req, res) 
{

	console.log('Menu asked');

	if(clientStatus)
	{
		menu.generatePage(url.parse(req.url).query, function(page)
		{
			res.send(page);
		});
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.post('/final', function (req, res) 
{

	console.log('Final asked');

	if(clientStatus)
	{
		console.log(req.body);
		res.send('0');
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.get('/final/booked', function (req, res) 
{

	console.log('Booked asked');

	if(clientStatus)
	{
		res.send('passed');
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.get('/final/failed', function (req, res) 
{

	console.log('Failed asked');

	if(clientStatus)
	{
		res.send('failed');
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.get('/maxOrdre', function (req, res) 
{

	console.log('MaxOrdre asked');

	if(clientStatus)
	{
		var ordreCfg = cfg.readOrdreConfig();

		res.send((Object.keys(ordreCfg).length-1).toString());
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

//**********  Express routes end  **********//

//**********  SocketIO  **********//

io.on('connect', function(socket)
{
	socket.on('update', function()
	{
		client.write("rt=update;m=new_infos;");
	});

	socket.on('booking', function(message)
	{
		console.log(message);
	});


});

function sendUpdate()
{
	async.waterfall([
		function(callback){
			var fTypes = [];

			connection.query("select * from food_types;", function (error, results, fields) 
    		{
		    	for(var j = 0, length2 = results.length; j < length2; j++)
		    	{
		    		fType = [];

		    		fType.push(results[j].id);
		    		fType.push(results[j].name);

		    		fTypes.push(fType);
		    	}

		    	callback(null, fTypes);
		    });

		    
	    },
	    function(fTs, callback)
	    {
	    	var foods = [];

	    	connection.query("select * from food;", function (error2, results2, fields2) 
			{
				for(var i = 0, length1 = results2.length; i < length1; i++)
				{
					food = [];

					food.push(results2[i].id);
					food.push(results2[i].name);
					food.push(results2[i].quantity);
					food.push(results2[i].type_id);

					foods.push(food);		
				}



				callback(null, fTs, foods);
			});		
	    },
	    function(fTs, fds, callback)
	    {
	    	var answer = [];

	    	for(fT of fTs)
			{

				var foods = [];

				for(f of fds)
				{
					if(fT[0] == f[3])
					{
						foods.push(f);
					}
				}

				fT.push(foods);

				answer.push(fT);
			}

			io.emit('update', answer);

			callback(null, "Update emitted");
	    }
	], function(err, resu)
	{
	});	
}

//**********  SocketIO end  **********//

http.listen(8080, function () {
	console.log('Server running ...');
})