var cfg = require("./configFile");
var url = require('url');
var fs = require("fs");
var express = require("express");
var app = express();
var path = require("path");
var cfg = require("./configFile");
var mysql = require('mysql');
var generator = require('./pageGenerator.js');
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

var bookingQueue = [];
var processingQueue = false;

if(coCfg != nu5)
{

	connection = mysql.createConnection({
		host     : coCfg[4],
		user     : coCfg[1],
		password : coCfg[2],
		database : coCfg[0],
		port     : coCfg[3],
		multipleStatements: true
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
		generator.generateReservPage(url.parse(req.url).query, function(page)
		{
			res.send(page);
		});
	}
	else 
	{
		res.status(500).send('Something went wrong ! (E02)');	
	}
	
});

app.post('/menu', function (req, res) 
{

	console.log('Menu asked');

	if(clientStatus)
	{
		generator.generateMenuPage(url.parse(req.url).query, req.body, function(page)
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

		checkReservation(req.body, function(result)
		{
			if(result)
			{

				console.log('Result positive');

				var datas = [req.body, 
							 function(data)
							 {	
							 	res.send(data);
							 }
							];

				bookingQueue.push(datas);

				console.log(datas);

				if(!processingQueue)
				{
					processingQueue = true;

					processQueue();
				}
			}
			else
			{
				console.log('seding a 1');
				res.send('1');
			}
		});
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

function checkReservation(data, callback)
{
	connection.query("select * from food;", function (error, results, fields) 
    {
        if (error) error;

        connection.query("select * from excluded_food_types_join;", function (error2, results2, fields2) 
        {
            if (error) return error2;

            var confirmedFood = [];

            var foods = [];

            if(data[1][0] == undefined){ console.log('Data undefined'); callback(false); }

            for(af of data[1][0])
	        {
	            for(var i = 0, length1 = results.length; i < length1; i++)
	            {
	                if(af == results[i].id)
	                {
	                    foods.push(results[i]);

	                    break;
	                }
	            }
	        }

            for(f of foods)
            {
            	for(cf of confirmedFood)
            	{
            		if(cf.type_id == f.type_id)
            		{
            			console.log('Same type id ('+f.type_id+')');
            			callback(false);
            		}
            		else
            		{
            			var excludedTypes = [];

            			for(var i = 0, length1 = results2.length; i < length1; i++)
                        {
                            if(results2[i].master_food_types == f.type_id)
                            {
                                var add = true;

                                for(e of excludedTypes)
                                {
                                    if(e == results2[i].slave_food_types)
                                    {
                                        add = false;
                                        break;
                                    }
                                }

                                if(add)
                                {
                                    excludedTypes.push(results2[i].slave_food_types);
                                }
                            }
                        }

            			for(e of excludedTypes)
            			{
            				if(f.type_id == e)
            				{
            					console.log('Type excluded ('+f.type_id+')');

            					callback(false);
            				}
            			}
            		}
            	}

            	confirmedFood.push(f);
            }

            callback(true);
        });
    });

}

function book(data, callback)
{
	console.log('Process data: '+data);

	console.log('Cdoe: '+data[0][0]);

	connection.query("select * from badges where code_id="+data[0][0]+";", function (error, results, fields) 
    {

    	var reserv = true;
    	var motif = ['', []];

    	if(error)
    	{
    		console.log(error);
    		motif[0] = '3';
			data[1](motif);
			callback();
			return;
    	}

    	if(results[0].passed != 0)
    	{
    		motif[0] = '1';
    		data[1](motif);
    		callback();
    		return;
    	}
    	else
    	{
    		connection.query("select * from booking;", function (error2, results2, fields2) 
        	{
        		connection.query("select * from food;", function (error3, results3, fields3) 
        		{
        			if(error2 || error3)
			    	{
			    		console.log(error2);
			    		console.log(error3);
			    		motif[0] = '3';
						data[1](motif);
						callback();
						return;
			    	}

	        		var foodQuantity = {};

	        		for(var i = 0, length1 = results2.length; i < length1; i++)
	        		{
	        			if(foodQuantity[results2[i].food_id] == undefined)
	        			{
	        				foodQuantity[results2[i].food_id] == 1;
	        			}
	        			else
	        			{
	        				foodQuantity[results2[i].food_id] == foodQuantity[results2[i].food_id] + 1;
	        			}
	        		}

	        		console.log(foodQuantity);

	        		for(f of data[0][1][0])
	        		{
	        			for(var i = 0, length1 = results2.length; i < length1; i++)
	        			{
	        				if(results3[i].id == f)
	        				{
	        					if(foodQuantity[f] >= results3[i].quantity)
	        					{
	        						console.log(f);
	        						reserv = false;
	        						motif[0] = '2';
	        						motif[1].push([results3.name]);
	        						data[1](motif);
	        						callback();
	        						return;
	        					}
	        					else
	        					{
	        						break;
	        					}
	        				}
	        			}
	        		}

	        		var query = "";

	        		for(f of data[0][1][0])
	        		{
	        			query += "insert into booking (badge_id, food_id) values("+results[0].id+", "+f+");";
	        		}

	        		query += "update badges set passed=1 where id="+results[0].id+";";

	        		console.log(query);

	        		connection.query(query, function (error4, results4, fields4)
	        		{
	        			if(error4)
	        			{
	        				console.log(error4);
	        				motif[0] = '3';
	        				data[1](motif);
	        				callback();
	        				return;
	        			}
	        			else
	        			{
	        				motif[0] = '0';
	        				data[1](motif);
	        				callback();
	        				return;
	        			}
	        		});
	        	});
        	});
    	}
    });
}

function processQueue()
{
	processingQueue = true;


	console.log('Processing queue');

	while(bookingQueue.length > 0)
	{
		book(bookingQueue.shift(), function(){});
	}

	processingQueue = false;
}

http.listen(8080, function () {
	console.log('Server running ...');
})