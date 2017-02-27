(function() {
    var fs = require("fs");
    var cfg = require("./configFile");
    var mysql = require('mysql');

    var connection;

    function connect()
    {
        var coCfg = cfg.readDatabaseConfig();
        var nu = [null, null, null, null, null];

        if(coCfg != nu)
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
    }

    module.exports.generateFailedPage = function(parameters, callback) 
    {
        var error = parameters.split("&")[0].split("=")[1];

        switch(error)
        {
            case '1':
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Erreur</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                '</head>'+
                                '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                '<script src="../js/jquery.history.js"></script>'+
                                '<script src="../js/reset.js"></script>'+
                                '<body>'+
                                    '<article id="test">'+
                                        '<span style="position: absolute; top: 100px; left: 360px;">'+
                                            '<p>Une réservation a déjà été éffectuée avec votre badge.</p>'+
                                        '</span>'+
                                        '<button id="btn">Retour</button>'+
                                    '</article>'+ 
                                '</body>'+
                            '</html>';

                callback(page);

                break;
            case '2':
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Erreur</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                '</head>'+
                                '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                '<script src="../js/jquery.history.js"></script>'+
                                '<script src="../js/reset.js"></script>'+
                                '<body>'+
                                    '<article id="test">'+
                                        '<span style="position: absolute; top: 100px; left: 360px;">'+
                                            '<p>Nous sommes désolés, mais les produits suivants ne sont plus disponibles, veuillez recommencer votre réservation.</p>'+
                                            '<p></p>';
                
                var foods = parameters.split("&")[1].split("=")[1].split(',');

                for(f of foods)
                {
                    page += ('<p>' + f + '</p>');
                }

                                    page += '</span>'+
                                        '<button id="btn">Retour</button>'+
                                    '</article>'+ 
                                '</body>'+
                            '</html>';

                callback(page);
                
                break;
            case '3':
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Erreur</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                '</head>'+
                                '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                '<script src="../js/jquery.history.js"></script>'+
                                '<script src="../js/reset.js"></script>'+
                                '<body>'+
                                    '<article id="test">'+
                                        '<span style="position: absolute; top: 100px; left: 360px;">'+
                                            '<p>Une erreur s\'est produite lors de la réservation. Veuillez réessayer.</p>'+
                                            '<p>En cas de disfonctionnements répétés, veuillez contacter l\'administrateur.</p>'+
                                        '</span>'+
                                        '<button id="btn">Retour</button>'+
                                    '</article>'+ 
                                '</body>'+
                            '</html>';

                callback(page);

                break;
        }
    }

    module.exports.generateConfirmPage = function(parameters, callback) 
    {
        var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Erreur</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                '</head>'+
                                '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                '<script src="../js/jquery.history.js"></script>'+
                                '<script src="../js/reset.js"></script>'+
                                '<body>'+
                                    '<article id="test">'+
                                        '<span style="position: absolute; top: 100px; left: 360px;">'+
                                            '<p>Votre réservation a bien été enregistrée. Bonne journée !</p>'+
                                        '</span>'+
                                        '<button id="btn">Retour</button>'+
                                    '</article>'+ 
                                '</body>'+
                            '</html>';

        callback(page);
    }

    module.exports.generateReservPage = function(parameters, callback) 
    {
        connection.query("select * from badges where code_id='"+parameters.split("&")[0].split("=")[1]+"';", function (error, results, fields) 
        {
            if (error) return generateErrorPage();

            var page =  '<!DOCTYPE html>'+
                    '<html>'+
                        '<head>'+
                            '<meta charset="utf8"/>'+
                            '<title>SR2D</title>'+
                            '<link rel="stylesheet" href="css/menu.css">'+
                        '</head>'+
                        '<script src="js/socket.io.js"></script>'+
                        '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                        '<script src="js/socket.js"></script>'+
                        '<script src="js/jquery.history.js"></script>'+
                        '<script src="js/main.js"></script>'+
                        '<body>'+
                            '<article id="test">'+
                                    '<div id="ticket">'+
                                        '<div class="foodListItem" id ="food-11">'+
                                            '<p>9</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="chosingPanel">'+
                                    '</div>'+
                                    '<span style="position: absolute; top: 100px; left: 360px;">'+
                                        '<p>Bonjour '+results[0].first_name+' '+results[0].name+'</p>'+
                                    '</span>'+
                                    '<button id="btn">Valider</button>'+
                            '</article>'+ 
                        '</body>'+
                    '</html>';

            callback(page);

        });
    }

    module.exports.generateMenuPage = function(parameters, data, callback) 
    {
        connection.query("select * from badges where code_id='"+parameters.split("&")[0].split("=")[1]+"';", function (error, results, fields) 
        {
            if (error) return generateErrorPage();

            var ordre = parameters.split("&")[1].split("=")[1];

            var ordreCfg = cfg.readOrdreConfig();

            var currentOrder = ordreCfg[ordre];

            var menu = [];

            var cont = false;

            if(currentOrder != null)
            {
                connection.query("select * from food_types;", function (error2, results2, fields2)
                {

                    connection.query("select * from food;", function (error3, results3, fields3)
                    {

                        connection.query("select * from excluded_food_types_join;", function (error4, results4, fields4) 
                        {

                            connection.query("select * from booking;", function (error5, results5, fields5) 
                            {

                                if (error2) return error2;
                                if (error3) return error3;
                                if (error4) return error4;

                                var types = [];

                                var excludedTypes = [];

                                if(data[0] != undefined)
                                {
                                    for(af of data[0])
                                    {
                                        for(var i = 0, length1 = results3.length; i < length1; i++)
                                        {
                                            if(af == results3[i].id)
                                            {
                                                types.push(results3[i].type_id);

                                                break;
                                            }
                                        }
                                    }
                                }
                            
                                for(t of types)
                                {
                                    for(var i = 0, length1 = results4.length; i < length1; i++)
                                    {
                                        if(results4[i].master_food_types == t)
                                        {
                                            var add = true;

                                            for(e of excludedTypes)
                                            {
                                                if(e == results4[i].slave_food_types)
                                                {
                                                    add = false;
                                                    break;
                                                }
                                            }

                                            if(add)
                                            {
                                                excludedTypes.push(results4[i].slave_food_types);
                                            }
                                        }
                                    }

                                    excludedTypes.push(t);
                                }

                                var foodQuantity = {};

                                for(var i = 0, length1 = results5.length; i < length1; i++)
                                {
                                    if(foodQuantity[results5[i].food_id] == undefined)
                                    {
                                        foodQuantity[results5[i].food_id] = 1;
                                    }
                                    else
                                    {
                                        foodQuantity[results5[i].food_id] = foodQuantity[results5[i].food_id] + 1;
                                    }
                                }

                                for(cO in currentOrder.types)
                                {
                                    for(var i = 0, length1 = results2.length; i < length1; i++)
                                    {
                                        if(cO == results2[i].id)
                                        {
                                            var include = true;

                                            for(e of excludedTypes)
                                            {
                                                if(e == cO)
                                                {
                                                    include = false;
                                                    break;
                                                }
                                            }

                                            if(include)
                                            {
                                                var foodType = [results2[i].id, results2[i].name];

                                                var food = [];

                                                for(var i2 = 0, length3 = results3.length; i2 < length3; i2++)
                                                {

                                                    if(results3[i2].type_id == results2[i].id)
                                                    {
                                                        if(foodQuantity[results3[i2].id] < results3[i2].quantity || foodQuantity[results3[i2].id] == undefined)
                                                        {
                                                            food.push([results3[i2].id, results3[i2].name]);
                                                        }
                                                    }
                                                }

                                                foodType.push(food);

                                                menu.push(foodType);
                                            }                                    
                                        }
                                    }

                                }

                                if(menu != [])
                                {
                                    var page = '';

                                    for(m of menu)
                                    {
                                        page += ('<div class="foodType" id="fT-'+m[0]+' name='+m[1]+'"><div class="foodsContainer" >');
                                        //style="visibility: hidden;"

                                            for(f of m[2])
                                            {
                                                page += ('<div class="food" id="f-'+f[0]+'">'+f[1]+'</div>');
                                            }

                                        page += '</div></div>';
                                    }
                                         
                                    callback(page);
                                }
                                else
                                {
                                    return generateErrorPage();
                                }
                            });
                        });
                    });
                });            
            }
            else
            {
                return generateErrorPage();
            }
        });
    }

    connect();

}());