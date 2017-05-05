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
                                    '<title>Succès</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="../css/accueil.css" />'+
                                    '<script src="../js/reset.js"></script>'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Une réservation a déjà</p>'+
                                        '<p>été effectuée avec votre badge</p>'+
                                    '</div>'+
                                '</body>'+
                            '</html>';

                callback(page);

                break;
            case '2':
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Succès</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="../css/accueil.css" />'+
                                    '<script src="../js/reset.js"></script>'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Nous sommes désolés, mais les produits suivants ne sont plus disponibles, veuillez recommencer votre réservation.</p>'+
                                        '<p></p>';
                
                var foods = parameters.split("&")[1].split("=")[1].split(',');

                for(f of foods)
                {
                    page += ('<p>' + f + '</p>');
                }

                                    page += '</div>'+
                                            '</body>'+
                                        '</html>';

                callback(page);
                
                break;
            case '3':
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf8"/>'+
                                    '<title>Succès</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="../css/accueil.css" />'+
                                    '<script src="../js/reset.js"></script>'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Une erreur s\'est produite lors de la réservation. Veuillez réessayer.</p>'+
                                        '<p>En cas de disfonctionnements répétés, veuillez contacter l\'administrateur.</p>'+
                                    '</div>'+
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
                                    '<title>Succès</title>'+
                                    '<link rel="stylesheet" href="../css/menu.css">'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="../css/accueil.css" />'+
                                    '<script src="../js/reset.js"></script>'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Votre commande a bien été enregistrée</p>'+
                                        '<p>Bonne journée</p>'+
                                    '</div>'+
                                '</body>'+
                            '</html>';

        callback(page);
    }

    module.exports.generateReservPage = function(parameters, service, callback) 
    {
        if(parameters.split("&")[1].split("=")[1] == '0')
        {
            if(service)
            {
                connection.query("select * from badges where code_id='"+parameters.split("&")[0].split("=")[1]+"';", function (error, results, fields) 
                {
                    if (error) return generateErrorPage();

                    if(results[0] != undefined)
                    {
                        var page =  '<!DOCTYPE html>'+
                                    '<html>'+
                                        '<head>'+
                                            '<meta charset="utf8"/>'+
                                            '<title>SR2D</title>'+
                                            '<link rel="stylesheet" href="css/menu.css">'+
                                            '<link rel="stylesheet" href="css/accueil.css">'+
                                        '</head>'+
                                        '<script src="js/socket.io.js"></script>'+
                                        '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                        '<script src="js/socket.js"></script>'+
                                        '<script src="js/jquery.history.js"></script>'+
                                        '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                        '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                        '<body>'+
                                            '<div id="header">'+
                                                '<div id="headerTitle">'+
                                                    '<p>SR2D</p>'+
                                                '</div>'+
                                                '<div class="progress">'+
                                                    '<div id="progressbar" class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%;">50%'+
                                                    '</div>'+
                                                '</div>'+
                                            '</div>'+
                                            '<div id="container">'+
                                                '<div id="topbar"><p>Veuillez choisir:</p></div>'+
                                                    '<div id="main">'+
                                                        '<div id="column_left">'+       
                                                        '</div>'+
                                                        '<div id="column_right">'+
                                                            'Commande :'+
                                                            '<div id="ticket">'+
                                                            '</div>'+
                                                        '</div>'+
                                                    '</div>'+
                                                '</div>'+
                                            '</div>'+
                                        '</body>'+
                                        '<script src="js/main.js"></script>'+
                                    '</html>';

                        callback(page);
                    }
                });
            }
            else
            {
                var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf-8" />'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="accueil.css" />'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Bonjour,</p>'+
                                        '<p>les réservations ne sont pas ouvertes !</p>'+
                                    '</div>'+
                                    '<footer>'+
                                        '<span style="position: absolute; top: 550px; left: 1140px;">'+
                                        '<a href= "P2.html" class="btn btn-primary btn-lg active" href="#" role="button">Valider</a>'+
                                        '</span>'+
                                    '</footer>'+
                                '</body>'+
                            '</html>';

                    callback(page);
            }
        }
        else
        {
            var page =  '<!DOCTYPE html>'+
                            '<html>'+
                                '<head>'+
                                    '<meta charset="utf-8" />'+
                                    '<link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>'+
                                    '<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>'+
                                    '<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">'+
                                    '<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>'+
                                    '<link rel="stylesheet" href="accueil.css" />'+
                                    '<title>SR2D</title>'+
                                '</head>'+
                                '<body>'+
                                    '<div id="header">'+
                                        '<div id="headerTitle">'+
                                            '<p>SR2D</p>'+
                                        '</div>'+
                                    '</div>'+
                                    '<div id="mainText">'+
                                        '<p>Bonjour,</p>'+
                                        '<p>ce code n\'est pas attribué !</p>'+
                                    '</div>'+
                                '</body>'+
                            '</html>';

            callback(page);
        }       
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
                                    var page = '<ul class="nav nav-tabs">';

                                    for(m of menu)
                                    {
                                        page += '<li role="presentation" class="">'+
                                                    '<a href="#fT-'+m[0]+'" aria-controls="'+m[1]+'" role="tab" data-toggle="tab">'+m[1]+'</a></li>'+
                                                '</li>';
                                    }

                                    page += '<script type="text/javascript">'+
                                                    '$("#myTabs a").click(function (e) {'+
                                                        'e.preventDefault();'+
                                                        '$(this).tab("show");'+
                                                    '});'+
                                                '</script>'+
                                            '</ul>'+
                                            '<div class="tab-content">';

                                    for(m of menu)
                                    {
                                        page += ('<div role="tabpanel" class="tab-pane" id="fT-'+m[0]+'">');

                                            for(f of m[2])
                                            {
                                                page += '<span id="f-'+f[0]+'" onClick="ajouter_bouffe(\''+f[0]+'\', \''+f[1]+'\')">'+
                                                            '<a href="#" class="btn btn-primary btn-lg btn-block disabled" role="button"><img src="image.jpg" alt="" />'+f[1]+'</a>'+
                                                        '</span>';
                                            }

                                        page += '</div>';
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